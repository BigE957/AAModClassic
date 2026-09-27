using AAModClassic.UI.Dialogue;
using log4net;
using MonoMod.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Dialogues
{
    internal enum DialogueFileKind
    {
        WorldText,
        DialogueTree
    }

    internal record DialogueEntry<T>(Mod ProviderMod, string FilePath, string DialogueKey, T Data);
    
    internal record DialogueRawEntry(Mod ProviderMod, string FilePath, string DialogueKey, DialogueFileKind Kind, DialogueTextData WorldTextData, DialogueTree TreeData);

    internal partial class DialogueLoader : ModSystem
    {
        private const string DialogueFilePrefix = "Dialogue.";

        private static readonly Dictionary<string, DialogueEntry<DialogueTextData>> _WorldTextLookup = [];
        private static readonly Dictionary<string, DialogueEntry<DialogueTree>> _DialogueTreeLookup = [];
        private static readonly Dictionary<Mod, MainThreadedFileSystemWatcher> _Watchers = [];

        public override void Load()
        {
            _WorldTextLookup.Clear();
            _DialogueTreeLookup.Clear();

            var method = typeof(LocalizationLoader).GetMethod("ExtractLocalizationFiles", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            if (method != null)
            {
                MonoModHooks.Modify(method, ExtractDialogueFilesPatch);
            }
        }

        public override void PostSetupContent()
        {
            foreach (var mod in ModLoader.Mods)
            {
                var path = mod.SourceFolder;
                if (!Directory.Exists(path))
                    continue;

                var watcher = new MainThreadedFileSystemWatcher()
                {
                    Path = path,
                    Filter = "*.json",
                    FileNameFilter = DialogueFileRegex(),
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
                    IncludeSubdirectories = true
                };
                watcher.Changed += (arg) =>
                {
                    HandleFileUpdate(mod, arg.FullPath);
                };
                watcher.Renamed += (arg) =>
                {
                    HandleFileUpdate(mod, arg.FullPath);
                };
                watcher.EnableRaisingEvents = true;
                _Watchers[mod] = watcher;
            }
        }

        public override void Unload()
        {
            _WorldTextLookup.Clear();
            _DialogueTreeLookup.Clear();

            foreach (var watcher in _Watchers.Values)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }

            _Watchers.Clear();
        }

        private static void ExtractDialogueFilesPatch(ILContext il)
        {
            var cursor = new ILCursor(il);

            static void ILFailure(ILog logger, string name, string reason)
            {
                logger.Warn($"IL edit \"{name}\" failed! {reason}");
            }

            int pathLdloc = -1;
            int modLdloc = -1;
            if (!cursor.TryGotoNext(MoveType.After,
                i => i.MatchLdloc(out modLdloc), // Mod mod
                i => i.MatchLdloc(out pathLdloc), // string path
                i => i.MatchCallOrCallvirt(out _), // GameCulture ActiveCulture
                i => i.MatchCallOrCallvirt(typeof(LocalizationLoader), "UpdateLocalizationFilesForMod")))
            {
                ILFailure(AAMod.instance.Logger, "Force Extract Dialogue Files", $"Unable to locate UpdateLocalizationFilesForMod call");
                return;
            }

            if (modLdloc == -1)
            {
                ILFailure(AAMod.instance.Logger, "Force Extract Dialogue Files", $"Unable to locate ldloc index for mod");
                return;
            }

            if (pathLdloc == -1)
            {
                ILFailure(AAMod.instance.Logger, "Force Extract Dialogue Files", $"Unable to locate ldloc index for path");
                return;
            }

            cursor.EmitLdloc(modLdloc);
            cursor.EmitLdloc(pathLdloc);
            cursor.EmitDelegate((Mod mod, string basePath) =>
            {
                foreach (var entry in GetDialogueFilesForMod(mod, GameCulture.DefaultCulture, skipDeserializeData: true))
                {
                    try
                    {
                        var destFilePath = Path.Combine(basePath, entry.FilePath);
                        var destDir = Path.GetDirectoryName(destFilePath);
                        if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);

                        using var stream = mod.GetFileStream(entry.FilePath);
                        using var fileStream = File.OpenWrite(destFilePath);
                        using var writer = new StreamWriter(fileStream, Encoding.UTF8);
                        using var reader = new StreamReader(stream, Encoding.UTF8);
                        writer.Write(reader.ReadToEnd());
                    }
                    catch (Exception e)
                    {
                        AAMod.instance.Logger.Error($"Error while exporting dialogue entry ({mod.Name}::{entry.FilePath}): {e}");
                    }
                }
            });
        }

        public static bool TryGetTextData(string dialogueKey, out DialogueTextData data)
        {
            if (_WorldTextLookup.TryGetValue(dialogueKey, out var entry))
            {
                data = entry.Data;
                return true;
            }

            data = null;
            return false;
        }

        public static bool TryGetDialogueTree(string dialogueKey, out DialogueTree data)
        {
            if (_DialogueTreeLookup.TryGetValue(dialogueKey, out var entry))
            {
                data = entry.Data;
                return true;
            }

            data = null;
            return false;
        }

        public override void OnLocalizationsLoaded()
        {
            var defaultEntries = GetDialogueFilesForAllMods(GameCulture.DefaultCulture).ToList();

            PopulateDefaultLookup(_WorldTextLookup, defaultEntries.Where(e => e.Kind == DialogueFileKind.WorldText), e => e.WorldTextData, d => d.Revision);
            PopulateDefaultLookup(_DialogueTreeLookup, defaultEntries.Where(e => e.Kind == DialogueFileKind.DialogueTree), e => e.TreeData, d => d.Revision);

            var activeCulture = LanguageManager.Instance.ActiveCulture;
            if (activeCulture == GameCulture.DefaultCulture)
                return;

            var activeEntries = GetDialogueFilesForAllMods(activeCulture).ToList();

            MergeLocalizedEntries(_WorldTextLookup, activeEntries.Where(e => e.Kind == DialogueFileKind.WorldText), e => e.WorldTextData, d => d.Revision);
            MergeLocalizedEntries(_DialogueTreeLookup, activeEntries.Where(e => e.Kind == DialogueFileKind.DialogueTree), e => e.TreeData, d => d.Revision);
        }

        private static void PopulateDefaultLookup<T>(Dictionary<string, DialogueEntry<T>> lookup, IEnumerable<DialogueRawEntry> entries, Func<DialogueRawEntry, T> selectData, Func<T, int> revisionOf)
        {
            lookup.Clear();

            foreach (var raw in entries)
            {
                var data = selectData(raw);

                if (lookup.TryGetValue(raw.DialogueKey, out var oldEntry) && revisionOf(data) != revisionOf(oldEntry.Data))
                {
                    AAMod.instance.Logger.Warn($"Dialogue Localization was detected but revision mismatches. This will not be applied! : '{raw.ProviderMod.Name}::{raw.FilePath}'");
                    continue;
                }

                lookup[raw.DialogueKey] = new DialogueEntry<T>(raw.ProviderMod, raw.FilePath, raw.DialogueKey, data);
            }
        }

        private static void MergeLocalizedEntries<T>(Dictionary<string, DialogueEntry<T>> lookup, IEnumerable<DialogueRawEntry> entries, Func<DialogueRawEntry, T> selectData, Func<T, int> revisionOf)
        {
            foreach (var raw in entries)
            {
                var mod = raw.ProviderMod;

                if (!lookup.TryGetValue(raw.DialogueKey, out var oldEntry))
                {
                    AAMod.instance.Logger.Warn($"Dialogue Localization was detected but original Dialogue file does not exist. This will not be applied! : '{mod.Name}::{raw.FilePath}'");
                    continue;
                }

                if (oldEntry.ProviderMod == mod && oldEntry.FilePath == raw.FilePath)
                    continue;

                var data = selectData(raw);

                if (revisionOf(oldEntry.Data) != revisionOf(data))
                {
                    AAMod.instance.Logger.Warn($"Dialogue Localization was detected but revision mismatches. This will not be applied! : '{mod.Name}::{raw.FilePath}'");
                    continue;
                }

                lookup[raw.DialogueKey] = new DialogueEntry<T>(mod, raw.FilePath, raw.DialogueKey, data);
            }
        }

        private static void HandleFileUpdate(Mod mod, string filePath)
        {
            if (!TryGetDialogueFileInfo(filePath, out _, out _, out var dialogueKey))
                return;

            bool hasWorldText = _WorldTextLookup.TryGetValue(dialogueKey, out var worldEntry) && worldEntry.ProviderMod == mod;
            bool hasTree = _DialogueTreeLookup.TryGetValue(dialogueKey, out var treeEntry) && treeEntry.ProviderMod == mod;

            if (!hasWorldText && !hasTree)
                return;

            try
            {
                using var stream = File.OpenRead(filePath);
                if (!TryReadDialogueFile(stream, out var kind, out var worldData, out var treeData))
                {
                    AAMod.instance.Logger.Error($"Unrecognized dialogue file schema during hot reload, expected a 'Pages' or 'Dialogues' root property: '{filePath}'");
                    return;
                }

                string hotreloadedMessage;
                if (kind == DialogueFileKind.WorldText && hasWorldText)
                {
                    _WorldTextLookup[dialogueKey] = worldEntry with { Data = worldData };
                    hotreloadedMessage = $"Dialogue entry has been hot reloaded: '{dialogueKey}', from source: '{filePath}'";
                }
                else if (kind == DialogueFileKind.DialogueTree && hasTree)
                {
                    _DialogueTreeLookup[dialogueKey] = treeEntry with { Data = treeData };
                    hotreloadedMessage = $"Dialogue Tree entry has been hot reloaded: '{dialogueKey}', from source: '{filePath}'";
                }
                else
                {
                    AAMod.instance.Logger.Warn($"Dialogue file's schema kind changed on disk; skipping hot reload for '{filePath}'. Restart to pick up the change.");
                    return;
                }

                AAMod.instance.Logger.Info(hotreloadedMessage);
                if (!Main.gameMenu)
                    Main.NewText(hotreloadedMessage);
            }
            catch (Exception e)
            {
                AAMod.instance.Logger.Error($"Error while hot reloading dialogue entry ({filePath}): {e}");
            }
        }

        private static readonly PropertyInfo ModFileProperty = typeof(Mod).GetProperty("File", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        private static PropertyInfo _fileEntryNameProperty;

        private static IEnumerable<string> GetModFileNames(Mod mod)
        {
            if (ModFileProperty == null)
                yield break;

            if (ModFileProperty.GetValue(mod) is not IEnumerable tmodFile)
                yield break;

            foreach (var entry in tmodFile)
            {
                _fileEntryNameProperty ??= entry.GetType().GetProperty("Name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (_fileEntryNameProperty?.GetValue(entry) is string name)
                    yield return name;
            }
        }

        private static IEnumerable<DialogueRawEntry> GetDialogueFilesForAllMods(GameCulture targetCulture, bool skipDeserializeData = false)
        {
            return ModLoader.Mods.SelectMany(mod => GetDialogueFilesForMod(mod, targetCulture, skipDeserializeData));
        }

        private static IEnumerable<DialogueRawEntry> GetDialogueFilesForMod(Mod mod, GameCulture targetCulture, bool skipDeserializeData = false)
        {
            if (mod == null)
                yield break;

            foreach (var fileName in GetModFileNames(mod))
            {
                if (!TryGetDialogueFileInfo(fileName, out var culture, out var prefix, out var dialogueKey))
                    continue;

                if (culture != targetCulture)
                    continue;

                if (skipDeserializeData)
                {
                    yield return new DialogueRawEntry(mod, fileName, dialogueKey, default, null, null);
                    continue;
                }

                DialogueFileKind kind;
                DialogueTextData worldText;
                DialogueTree tree;

                try
                {
                    using var stream = mod.GetFileStream(fileName);
                    if (!TryReadDialogueFile(stream, out kind, out worldText, out tree))
                    {
                        AAMod.instance.Logger.Error($"Unrecognized dialogue file schema ({mod.Name}::{fileName}): expected a top-level 'Pages' (WorldText) or 'Dialogues' (DialogueTree) property.");
                        continue;
                    }
                }
                catch (Exception e)
                {
                    AAMod.instance.Logger.Error($"Error while reading dialogue entry ({mod.Name}::{fileName}): {e}");
                    continue;
                }

                yield return new DialogueRawEntry(mod, fileName, dialogueKey, kind, worldText, tree);
            }
        }

        private static bool TryReadDialogueFile(Stream stream, out DialogueFileKind kind, out DialogueTextData worldText, out DialogueTree tree)
        {
            using var doc = JsonDocument.Parse(stream);
            var root = doc.RootElement;

            if (root.TryGetProperty("Dialogues", out _))
            {
                kind = DialogueFileKind.DialogueTree;
                tree = root.Deserialize<DialogueTree>();
                worldText = null;
                return true;
            }

            if (root.TryGetProperty("Pages", out _))
            {
                kind = DialogueFileKind.WorldText;
                worldText = root.Deserialize<DialogueTextData>();
                tree = null;
                return true;
            }

            kind = default;
            worldText = null;
            tree = null;
            return false;
        }

        private static bool TryGetDialogueFileInfo(string filePath, out GameCulture culture, out string prefix, out string dialogueKey)
        {
            if (!Path.GetExtension(filePath).Equals(".json", StringComparison.InvariantCultureIgnoreCase))
                goto EXIT_INVALID;

            if (!LocalizationLoader.TryGetCultureAndPrefixFromPath(filePath, out culture, out prefix))
                goto EXIT_INVALID;

            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (fileName.StartsWith($"{prefix}_{DialogueFilePrefix}", StringComparison.InvariantCultureIgnoreCase))
            {
                dialogueKey = fileName[$"{prefix}_{DialogueFilePrefix}".Length..];
                return true;
            }
            else if (fileName.StartsWith(DialogueFilePrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                dialogueKey = fileName[DialogueFilePrefix.Length..];
                return true;
            }

        EXIT_INVALID:
            culture = null;
            prefix = null;
            dialogueKey = null;
            return false;
        }

        [GeneratedRegex(@"Dialogue\..+?\.jsonc?$", RegexOptions.IgnoreCase)]
        private static partial Regex DialogueFileRegex();
    }

    public static class WorldTextLoader
    {
        public static bool TryGet(string dialogueKey, out DialogueTextData data) => DialogueLoader.TryGetTextData(dialogueKey, out data);
    }

    public static class DialogueTreeLoader
    {
        public static bool TryGet(string dialogueKey, out DialogueTree data) => DialogueLoader.TryGetDialogueTree(dialogueKey, out data);
    }

    internal sealed class MainThreadedFileSystemWatcher : IDisposable
    {
        public string Path
        {
            get => _FSW.Path;
            set => _FSW.Path = value;
        }

        public string Filter
        {
            get => _FSW.Filter;
            set => _FSW.Filter = value;
        }

        public Collection<string> Filters
        {
            get => _FSW.Filters;
        }

        public NotifyFilters NotifyFilter
        {
            get => _FSW.NotifyFilter;
            set => _FSW.NotifyFilter = value;
        }

        public bool IncludeSubdirectories
        {
            get => _FSW.IncludeSubdirectories;
            set => _FSW.IncludeSubdirectories = value;
        }

        public bool EnableRaisingEvents
        {
            get => _FSW.EnableRaisingEvents;
            set => _FSW.EnableRaisingEvents = value;
        }

        public Regex FileNameFilter { get; set; } = null;

        public event Action<FileSystemEventArgs> Changed;
        public event Action<RenamedEventArgs> Renamed;

        private Dictionary<string, FileSystemEventArgs> _ChangedQueue = [];
        private Dictionary<string, RenamedEventArgs> _RenamedQueue = [];

        private FileSystemWatcher _FSW;
        private bool _HasQueueInFrame = false;
        private bool _Disposed;

        public MainThreadedFileSystemWatcher()
        {
            _FSW = new();
            _FSW.Changed += (o, arg) =>
            {
                if (FileNameFilter != null && !FileNameFilter.IsMatch(System.IO.Path.GetFileName(arg.Name)))
                    return;

                lock (_ChangedQueue)
                {
                    _ChangedQueue[arg.FullPath] = arg;
                    _HasQueueInFrame = true;
                }
            };

            _FSW.Renamed += (o, arg) =>
            {
                if (FileNameFilter != null && !FileNameFilter.IsMatch(System.IO.Path.GetFileName(arg.Name)))
                    return;

                lock (_RenamedQueue)
                {
                    _RenamedQueue[arg.FullPath] = arg;
                    _HasQueueInFrame = true;
                }
            };

            MainThreadedFileSystemWatcherSystem.Register(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    _FSW?.Dispose();
                }

                _FSW = null;
                _Disposed = true;
                _ChangedQueue?.Clear();
                _RenamedQueue?.Clear();
                _ChangedQueue = null;
                _RenamedQueue = null;
                _HasQueueInFrame = false;
                MainThreadedFileSystemWatcherSystem.Unregister(this);
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private sealed class MainThreadedFileSystemWatcherSystem : ILoadable
        {
            private static MainThreadedFileSystemWatcher[] _Watchers = [];
            private static HashSet<MainThreadedFileSystemWatcher> _WatchersList = [];

            public static void Register(MainThreadedFileSystemWatcher watcher)
            {
                _WatchersList.Add(watcher);
                _Watchers = [.. _WatchersList];
            }

            public static void Unregister(MainThreadedFileSystemWatcher watcher)
            {
                _WatchersList.Remove(watcher);
                _Watchers = [.. _WatchersList];
            }

            void ILoadable.Load(Mod mod)
            {
                Main.OnTickForThirdPartySoftwareOnly += Tick;
            }

            void ILoadable.Unload()
            {
                Main.OnTickForThirdPartySoftwareOnly -= Tick;
                _WatchersList?.Clear();
                _Watchers = [];
            }

            private void Tick()
            {
                foreach (var watcher in _Watchers)
                {
                    if (!watcher._HasQueueInFrame)
                        continue;

                    HandleQueuedEvents(watcher);
                }
            }

            private static void HandleQueuedEvents(MainThreadedFileSystemWatcher watcher)
            {
                lock (watcher._ChangedQueue)
                {
                    foreach (var changed in watcher._ChangedQueue.Values)
                    {
                        watcher.Changed?.Invoke(changed);
                    }
                    watcher._ChangedQueue.Clear();
                }

                lock (watcher._RenamedQueue)
                {
                    foreach (var renamed in watcher._RenamedQueue.Values)
                    {
                        watcher.Renamed?.Invoke(renamed);
                    }
                    watcher._RenamedQueue.Clear();
                }

                watcher._HasQueueInFrame = false;
            }
        }
    }
}
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
    internal record DialogueTextDataEntry(
        Mod ProviderMod,
        string FilePath,
        string DialogueKey,
        DialogueTextData Data
        );

    internal partial class DialogueLoader : ModSystem
    {
        private const string DialogueFilePrefix = "Dialogue.";

        private static readonly Dictionary<string, DialogueTextDataEntry> _DialogueLookup = [];
        private static readonly Dictionary<Mod, MainThreadedFileSystemWatcher> _Watchers = [];

        public override void Load()
        {
            _DialogueLookup.Clear();

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
            _DialogueLookup.Clear();

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
                foreach (var entry in GetDialogueTextEntries(mod, GameCulture.DefaultCulture, skipDeserializeData: true))
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
                        AAMod.instance.Logger.Error($"Error while exporting DialogueTextData entry ({mod.Name}::{entry.FilePath}): {e}");
                    }
                }
            });
        }

        public static bool TryGetDialogue(string dialogueKey, out DialogueTextData data)
        {
            if (_DialogueLookup.TryGetValue(dialogueKey, out var entry))
            {
                data = entry.Data;
                return true;
            }

            data = null;
            return false;
        }

        public override void OnLocalizationsLoaded()
        {
            _DialogueLookup.Clear();

            // Mods should be sorted by dependency order.
            foreach (var entry in GetDialogueTextEntiresForAllMods(GameCulture.DefaultCulture))
            {
                if (_DialogueLookup.TryGetValue(entry.DialogueKey, out var oldEntry))
                {
                    if (entry.Data.Revision != oldEntry.Data.Revision)
                    {
                        AAMod.instance.Logger.Warn($"Dialogue Localization was detected but revision mismatches. This will not be applied! : '{entry.ProviderMod.Name}::{entry.FilePath}'");
                        continue;
                    }
                }

                _DialogueLookup[entry.DialogueKey] = entry;
            }

            var activeCulture = LanguageManager.Instance.ActiveCulture;
            if (activeCulture == GameCulture.DefaultCulture)
                return;

            foreach (var entry in GetDialogueTextEntiresForAllMods(activeCulture))
            {
                var mod = entry.ProviderMod;

                if (!_DialogueLookup.TryGetValue(entry.DialogueKey, out var oldEntry))
                {
                    AAMod.instance.Logger.Warn($"Dialogue Localization was detected but original Dialogue file does not exist. This will not be applied! : '{mod.Name}::{entry.FilePath}'");
                    continue;
                }

                // Skip if entry is from same file.
                if (oldEntry.ProviderMod == entry.ProviderMod && oldEntry.FilePath == entry.FilePath)
                {
                    continue;
                }

                if (oldEntry.Data.Revision != entry.Data.Revision)
                {
                    AAMod.instance.Logger.Warn($"Dialogue Localization was detected but revision mismatches. This will not be applied! : '{mod.Name}::{entry.FilePath}'");
                    continue;
                }

                _DialogueLookup[entry.DialogueKey] = entry;
            }
        }

        private static void HandleFileUpdate(Mod mod, string filePath)
        {
            if (!TryGetDialogueFileInfo(filePath, out _, out _, out var dialogueKey))
                return;

            if (!_DialogueLookup.TryGetValue(dialogueKey, out var existingEntry))
                return;

            if (existingEntry.ProviderMod != mod)
                return;

            try
            {
                using var stream = new StreamReader(File.OpenRead(filePath), Encoding.UTF8);
                _DialogueLookup[dialogueKey] = existingEntry with
                {
                    Data = JsonSerializer.Deserialize<DialogueTextData>(stream.BaseStream)
                };

                var hotreloadedMessage = $"Dialogue entry has been hot reloaded: '{dialogueKey}', from source: '{filePath}'";
                AAMod.instance.Logger.Info(hotreloadedMessage);
                if (!Main.gameMenu) Main.NewText(hotreloadedMessage);
            }
            catch (Exception e)
            {
                AAMod.instance.Logger.Error($"Error while hot reloading DialogueTextData entry ({filePath}): {e}");
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

        private static IEnumerable<DialogueTextDataEntry> GetDialogueTextEntiresForAllMods(GameCulture targetCulture, bool skipDeserializeData = false)
        {
            return ModLoader.Mods.SelectMany(mod => GetDialogueTextEntries(mod, targetCulture, skipDeserializeData));
        }

        private static IEnumerable<DialogueTextDataEntry> GetDialogueTextEntries(Mod mod, GameCulture targetCulture, bool skipDeserializeData = false)
        {
            if (mod == null)
                yield break;

            foreach (var fileName in GetModFileNames(mod))
            {
                if (!TryGetDialogueFileInfo(fileName, out var culture, out var prefix, out var dialogueKey))
                    continue;

                if (culture != targetCulture)
                    continue;

                DialogueTextData data = null;
                if (!skipDeserializeData)
                {
                    try
                    {
                        using var stream = new StreamReader(mod.GetFileStream(fileName), Encoding.UTF8);
                        data = JsonSerializer.Deserialize<DialogueTextData>(stream.BaseStream);
                    }
                    catch (Exception e)
                    {
                        AAMod.instance.Logger.Error($"Error while reading DialogueTextData entry ({mod.Name}::{fileName}): {e}");
                    }
                }

                if (data != null || skipDeserializeData)
                {
                    yield return new DialogueTextDataEntry(mod, fileName, dialogueKey, data);
                }
            }
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

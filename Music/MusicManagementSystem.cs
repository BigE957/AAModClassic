using System.Collections.Generic;
using System.Linq;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Music
{
    public class MusicManagementSystem : ModSystem
    {
        public static readonly Dictionary<string, short> MusicSlots = [];

        public override void OnModLoad()
        {
            foreach(string file in Mod.GetFileNames().Where(s => s.Contains("Music/")))
            {
                string path = file.Remove(file.IndexOf('.'));
                string name = path;
                for (int i = name.Length - 1; i >= 0; i--)
                    if (name[i] == '/')
                    {
                        name = name.Remove(0, i + 1);
                        break;
                    }

                MusicSlots.Add(name, (short)MusicLoader.GetMusicSlot(Mod, path));
            }
        }

        public static bool ReplaceTrack(string key, short musicSlot)
        {
            if (!MusicSlots.ContainsKey(key))
                return false;

            MusicSlots[key] = musicSlot;
            return true;
        }

        public override void PostSetupContent()
        {
            foreach(var pair in MusicSlots)
            {
                short slot = pair.Value;
                string name = pair.Key.Replace("_", "") + "Box";

                if (!Mod.TryFind<ModItem>(name, out ModItem item))
                {
                    Mod.Logger.Info($"Failed To add music box for {name}: No Music Box Item for that song could be found.");
                    continue;
                }

                if (!Mod.TryFind<ModTile>(name + "_Tile", out ModTile tile))
                {
                    Mod.Logger.Warn($"Failed To add music box for {name}: No Music Box Tile for that song could be found.");
                    continue;
                }

                MusicLoader.AddMusicBox(Mod, slot, item.Type, tile.Type);

                if (!ModLoader.TryGetMod("MusicDisplay", out Mod display))
                    return;

                string displayPath = "Mods.AAModClassic.CrossMod.MusicDisplay.";

                LocalizedText modName = Language.GetOrRegister(displayPath + "ModName");
                LocalizedText author = Language.GetOrRegister(displayPath + pair.Key.Replace("_", "") + ".Author");
                LocalizedText displayName = Language.GetOrRegister(displayPath + pair.Key.Replace("_", "") + ".DisplayName");
                display.Call("AddMusic", slot, displayName, author, modName);
            }
        }
    }
}

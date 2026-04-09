using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace AAModClassic.Music
{
    public class MusicManagementSystem : ModSystem
    {
        public static readonly Dictionary<string, int> MusicSlots = [];

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

                MusicSlots.Add(name, MusicLoader.GetMusicSlot(Mod, path));
            }
        }

        public static bool ReplaceTrack(string key, int musicSlot)
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
                int slot = pair.Value;
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
            }
        }
    }
}

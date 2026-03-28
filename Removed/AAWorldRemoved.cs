using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Removed.Items.BossSummons;
using AAModClassic.Removed.Tiles;
using AAModClassic.Removed.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace AAModClassic.Removed
{
    public class AAWorldRemoved : ModSystem
    {
        public static bool doRemovedContent; // has no function but u can see where removed content is placed elsewhere

        private Vector2 shipPos = new Vector2(0, 0);
        private int shipSide = 0;

        public static bool downedSoC;
        public static bool downedIZ;

        public static bool Anticheat = true;

        #region stupid bullshit
        public override void PreWorldGen()
        {
            downedSoC = false;
            downedIZ = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            var downedRemoved = new List<string>();
            if (downedSoC) downedRemoved.Add("SoC");
            if (downedIZ) downedRemoved.Add("IZ");

            tag.Add("downedRemoved", downedRemoved);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            var downedRemoved = tag.GetList<string>("downedRemoved");
            downedSoC = downedRemoved.Contains("SoC");
            downedIZ = downedRemoved.Contains("IZ");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedSoC;
            flags[1] = downedIZ;
            //flags[2] = downedIZ;
            //flags[3] = downedIZ;
            //flags[4] = downedIZ;
            //flags[5] = downedIZ;
            //flags[6] = downedIZ;
            //flags[7] = downedIZ;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedSoC = flags[0];
            downedIZ = flags[1];
            //downedIZ = flags[2];
            //downedIZ = flags[3];
            //downedIZ = flags[4];
            //downedIZ = flags[5];
            //downedIZ = flags[6];
            //downedIZ = flags[7];
        }
        #endregion

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            int shiniesIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            int chaosBiomeIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            /*
            tasks.Insert(shiniesIndex2 + 2, new PassLegacy("Parthenan", delegate (GenerationProgress progress, GameConfiguration config)
            {
                ParthenanIsland(progress);
            }));

            tasks.Insert(shiniesIndex2 + 3, new PassLegacy("Mush", delegate (GenerationProgress progress, GameConfiguration config)
            {
                Mush(progress);
            }));
            */
            tasks.Insert(shiniesIndex2, new PassLegacy("Ship", delegate (GenerationProgress progress, GameConfiguration config)
            {
                Ship(progress);
            }));
        }

        private void Ship(GenerationProgress progress)
        {
            shipSide = ((Main.dungeonX > Main.maxTilesX / 2) ? (-1) : (1));
            shipPos.X = (shipSide == 1 ? (Main.maxTilesX - 90) : 90);
            progress.Message = "Sinking the ship";
            SunkenShip();
        }

        public void SunkenShip()
        {
            Point origin = new Point((int)shipPos.X, (int)GenVars.worldSurfaceLow - 200);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            BOTE biome = new BOTE();
            biome.Place(origin, GenVars.structures);
        }

        public override void PostWorldGen()
        {
            int[] itemsToPlaceInSunkenChest = new int[] { ModContent.ItemType<CursedCompass>() };
            int itemsToPlaceInSunkenChestsChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].TileType == ModContent.TileType<SunkenChest>()) // if glass chest
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            itemsToPlaceInSunkenChestsChoice = Main.rand.Next(itemsToPlaceInSunkenChest.Length);
                            chest.item[0].SetDefaults(itemsToPlaceInSunkenChest[itemsToPlaceInSunkenChestsChoice]);
                            break;
                        }
                    }
                }
            }
        }
    }
}

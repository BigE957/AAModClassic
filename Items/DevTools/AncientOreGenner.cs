using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic.Items.DevTools
{
    public class AncientOreGenner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Ore Genner");
            // Tooltip.SetDefault(@"Generates AA worldgen ore");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            GenOres();
            
            return true;
        }
        private void GenOres()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next((int)GenVars.rockLayerLow, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].TileType == TileID.Stone)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)Mod.Find<ModTile>("IncineriteOre").Type);
                }
            }

            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, x);
                int tilesY = WorldGen.genRand.Next(0, y);
                if (Main.tile[tilesX, tilesY].TileType == TileID.Mud)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("EverleafRoot").Type);
                }
            }

            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next((int)GenVars.rockLayerLow, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].TileType == TileID.Mud)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)Mod.Find<ModTile>("AbyssiumOre").Type);
                }
            }
            
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next(0, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].TileType == TileID.IceBlock)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)Mod.Find<ModTile>("RelicOre").Type);
                }
            }

            int amount = (int)(Main.maxTilesX * 0.4f * 0.2f);
            for (int k = 0; k < amount; k++)
            {
                int i = WorldGen.genRand.Next(0, Main.maxTilesX);
                int j = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
                while (Main.tile[i, j].TileType != TileID.Stone)
                {
                    i = WorldGen.genRand.Next(0, Main.maxTilesX);
                    j = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
                }
                WorldGen.TileRunner(i, j, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), Mod.Find<ModTile>("PrismOre").Type);
            }
        }
    }
}

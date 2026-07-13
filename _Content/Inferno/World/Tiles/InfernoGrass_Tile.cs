using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Unreleased.Content.Inferno.World.Tiles;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class InfernoGrass_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileBlendAll[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = ModContent.DustType<Dusts.RazeleafDust>();
            AddMapEntry(new Color(255, 153, 51));
            RegisterItemDrop(ItemID.DirtBlock);
        }

        public override void RandomUpdate(int i, int j)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && !Framing.GetTileSafely(i, j - 1).HasTile)
            {
                if (WorldGen.IsFitToPlaceFlowerIn(i, j, TileID.Plants))
                {
                    if (Main.tile[i, j - 1].WallType >= 0 && WallID.Sets.AllowsPlantsToGrow[Main.tile[i, j - 1].WallType] && Main.tile[i, j].WallType >= 0 && Main.tile[i, j].WallType < WallLoader.WallCount && WallID.Sets.AllowsPlantsToGrow[Main.tile[i, j].WallType])
                    {
                        if (WorldGen.genRand.NextBool(50) || WorldGen.genRand.NextBool(40)) 
                        {
                            int style = 23; // mushroom
                            if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), false, style))
                                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), style, 0, -1, -1);
                        }
                        else if (Main.rand.NextBool(35) || (Main.tile[i, j].WallType >= WallID.GrassUnsafe && Main.tile[i, j].WallType <= WallID.HallowedGrassUnsafe))
                        {
                            int style = Main.rand.Next(16) + 5; // flowers
                            if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), false, style))
                                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), style, 0, -1, -1);
                        }
                        else
                        {
                            int style = Main.rand.Next(6); // grass
                            if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), false, style))
                                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), style, 0, -1, -1);
                        }
                    }
                }
            }
            else if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) &&!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(40))
            {
                int style = Main.rand.Next(23);
                if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), false, style))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), style, 0, -1, -1);
            }

            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(1500))
            {
                int style = 23; // mushroom
                if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), false, style))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), style, 0, -1, -1);

            }
        }
    }
}
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Unreleased.Content.Mire.World.Tiles;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic._Unreleased.Content.Mire.World.Tiles;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class MireGrass_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.JungleSpecial[Type] = true;
            DustType = ModContent.DustType<Dusts.AbyssiumDust>();
            AddMapEntry(new Color(0, 50, 140));
            RegisterItemDrop(ItemID.MudBlock);
        }

        public override void RandomUpdate(int i, int j)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && !Framing.GetTileSafely(i, j - 1).HasTile)
            {
                Tile tile = Main.tile[i, j];
                Tile tileAbove = Main.tile[i, j - 1];
                Tile tileBwlow = Main.tile[i, j + 1];

                // tall grass
                /*
                if (tileAbove.TileType == ModContent.TileType<MireFoliage_Tile>() && WorldGen.genRand.Next(3) == 0)
                {
                    if (tileAbove.TileFrameX < 144) // 144 is the spore
                    {
                        if (Main.rand.Next(4) == 0)
                        {
                            tileAbove.TileFrameX = (short)(162 + WorldGen.genRand.Next(8) * 18);
                        }
                        tileAbove.TileType = TileID.JunglePlants2;
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendTileSquare(-1, i, j);
                        }
                    }
                }
                */

                int style2 = 23;
                if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), true, style2))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style2, 0, -1, -1);

                // short grass
                if (WorldGen.genRand.Next(7) == 0)
                {
                    PlaceMireFoliageLikeJungleGrass(i, j);
                    if (tile.HasTile)
                    {
                        tileAbove.CopyPaintAndCoating(tile);
                    }
                    if (Main.netMode == NetmodeID.Server && tileAbove.HasTile)
                    {
                        NetMessage.SendTileSquare(-1, i, j);
                    }
                }
                /*
                // trees
                else if (WorldGen.genRand.Next(500) == 0 && (!Main.tile[i, j].HasTile || Main.tile[i, j].TileType == TileID.JunglePlants || Main.tile[i, j].TileType == TileID.JunglePlants2 || Main.tile[i, j].TileType == TileID.JungleThorns))
                {
                    if (WorldGen.GrowTree(i, j) && WorldGen.PlayerLOS(i, j))
                    {
                        WorldGen.TreeGrowFXCheck(i, j - 1);
                    }
                }
                // big plants
                else if (WorldGen.genRand.Next(25) == 0 && Main.tile[i, j].LiquidType == LiquidID.Water)
                {
                    WorldGen.PlaceJunglePlant(i, j, 233, WorldGen.genRand.Next(8), 0);
                    if (Main.tile[i, j].TileType == TileID.PlantDetritus)
                    {
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendTileSquare(-1, i, j, 4);
                        }
                        else
                        {
                            WorldGen.PlaceJunglePlant(i, j, 233, WorldGen.genRand.Next(12), 1);
                            if (Main.tile[i, j].TileType == TileID.PlantDetritus && Main.netMode == 2)
                            {
                                NetMessage.SendTileSquare(-1, i, j, 3);
                            }
                        }
                    }
                }

                bool flag2 = false;
                if (Main.netMode == 2 && flag2)
                {
                    NetMessage.SendTileSquare(-1, i, j, 3);
                }
                */
            }
            else if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) && !Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(40))
            {
                int style = Main.rand.Next(23);
                if (style == 9)
                    style = 7;
                if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), false, style))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
            }

            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(1500))
                {
                    int style = 23; // mushroom
                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), true, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }

                if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(10000))
                {
                    int style = 9; // black orchid
                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), true, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }
            }
        }

        public static void PlaceMireFoliageLikeJungleGrass(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            Tile tileBelow = Main.tile[i, j + 1];
            Tile tileAbove = Main.tile[i, j - 1];

            bool flag = (double)j > Main.rockLayer;
            if (!Framing.GetTileSafely(i, j - 1).HasTile)
            {
                /*
                 * mire thorns.................................
                if (Main.rand.Next(16) == 0 && (double)j > Main.worldSurface)
                {
                    tile.HasTile = true;
                    tile.type = 69;
                    SquareTileFrame(i, j);

                }
                else
                */
                if (!Main.dayTime && (Main.rand.NextBool(50) || WorldGen.genRand.NextBool(40))) // mushroom. yes this is the vanilla logic
                {
                    int style = 24;
                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), true, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }
                else if (Main.rand.NextBool(60) && flag) // spore
                {
                    int style = 8;
                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), true, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }
                else if (Main.rand.NextBool(230) && flag) // natures gift,but now its the thing
                {
                    int style = 9;
                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), true, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }
                else if (Main.rand.NextBool(15)) // jungle rose and vanity flowers
                {
                    int style = 0;

                    if (Main.rand.NextBool(3)) // jungle rose, replaced by nothing
                        style = (short)(Main.rand.Next(2) + 6);
                    else // vanity flowers
                        style = (short)(Main.rand.Next(13) + 10);

                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), true, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }
                else // grass
                {
                    int style = Main.rand.Next(6);
                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), false, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }
            }
        }

        public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
        {
            sightColor = Color.BlueViolet;
            return true;
        }
    }
}
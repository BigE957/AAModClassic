using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.WorldBuilding;
using static Mono.CompilerServices.SymbolWriter.CodeBlockEntry;

namespace AAModClassic.Utilities
{
    public class WorldGenUtils
    {
        public static Tile GetTileSafely(Vector2 position)
        {
            return GetTileSafely((int)(position.X / 16f), (int)(position.Y / 16f));
        }

        public static Tile GetTileSafely(int x, int y)
        {
            if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
                return new Tile();
            return Framing.GetTileSafely(x, y);
        }

        /*
         * Iterates downwards and returns the first Y position that has a tile in it.
         * startY : The y to begin iteration at.
         * solid : True if the tile must be solid.
         */
        public static int GetFirstTileFloor(int x, int startY, bool solid = true, bool checkWater = false, bool noSolidTop = false)
        {
            if (!WorldGen.InWorld(x, startY)) return startY;
            for (int y = startY; y < Main.maxTilesY - 10; y++)
            {
                Tile tile = Framing.GetTileSafely(x, y);
                if (checkWater && tile.LiquidAmount >= 255)
                    return y;
                if (tile is { HasUnactuatedTile: true } && (!solid || Main.tileSolid[tile.TileType]) && (!noSolidTop || !Main.tileSolidTop[tile.TileType]))
                    return y;
            }
            return Main.maxTilesY - 10;
        }

        /**
         * Returns the current world size.
         * 1 == small, 2 == medium, 3 == large.
         */
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }

            if (Main.maxTilesX == 6400) { return 2; }

            if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }

        /**
         *  Completely kills a chest at X, Y and removes all items within it.
         *  (note this does not remove the tile itself)
         */
        public static bool KillChestAndItems(int x, int y)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (Main.chest[i] != null && Main.chest[i].x == x && Main.chest[i].y == y)
                {
                    Main.chest[i] = null;
                    return true;
                }
            }
            return false;
        }

        /**
         *  Generates a single tile of liquid.
         *  isLava == true if you want lava instead of water.
         *  updateFlow == true if you want the flow to update after placement. (almost definitely yes)
         *  liquidHeight is the height given to the liquid. (0 - 255)
         */
        public static void GenerateLiquid(int x, int y, int liquidType, bool updateFlow = true, int liquidHeight = 255, bool sync = true)
        {
            Tile Mtile = Main.tile[x, y];

            if (!WorldGen.InWorld(x, y)) return;
            liquidHeight = (int)MathHelper.Clamp(liquidHeight, 0, 255);
            Main.tile[x, y].LiquidAmount = (byte)liquidHeight;
            if (liquidType == 0) { Mtile.LiquidType = LiquidID.Water; }
            else
                if (liquidType == 1) { Mtile.LiquidType = LiquidID.Lava; }
                else
                    if (liquidType == 2) { Mtile.LiquidType = LiquidID.Honey; }
                    else
                        if (liquidType == 3) { Mtile.LiquidType = LiquidID.Shimmer; }
            if (updateFlow) { Liquid.AddWater(x, y); }
            if (sync && Main.netMode != NetmodeID.SinglePlayer) { NetMessage.SendTileSquare(-1, x, y, 1); }
        }

        /*
         *  Generates a single tile and wall at the given coordinates. (if the tile is > 1 x 1 it assumes the passed in coordinate is the top left)
         *  tile : type of tile to place. -1 means don't do anything tile related, -2 is used in conjunction with active == false to make air.
         *  wall : type of wall to place. -1 means don't do anything wall related. -2 is used to remove the wall already there.
         *  tileStyle : the style of the given tile. 
         *  active : If false, will make the tile 'air' and show the wall only.
         *  removeLiquid : If true, it will remove liquids in the generating area.
		 *  slope : if -2, keep the current slope. if -1, make it a halfbrick, otherwise make it the slope given.
		 *  tileFrame: if true and tile is a 1x1 block, will frame it and its neighbours
		 *  silent : If true, will not display dust nor sound.
         *  sync : If true, will sync the client and server.
         */
        public static void GenerateTile(int x, int y, int tile, int wall, int tileStyle = 0, bool active = true, bool removeLiquid = true, int slope = -2, bool tileFrame = true, bool silent = false, bool sync = true)
        {
            try
            {
                if (!WorldGen.InWorld(x, y))
                    return;

                Tile Mtile = Framing.GetTileSafely(x, y);
                TileObjectData data = tile <= -1 ? null : TileObjectData.GetTileData(tile, tileStyle);
                int width = data == null ? 1 : data.Width;
                int height = data == null ? 1 : data.Height;
                byte oldSlope = (byte)Main.tile[x, y].Slope;
                bool oldHalfBrick = Main.tile[x, y].IsHalfBlock;

                if (tile != -1)
                {
                    WorldGen.destroyObject = true;

                    if (width > 1 || height > 1)
                    {
                        Vector2 topLeft = FindTopLeft(x, y);
                        int tlX = (int)topLeft.X;
                        int tlY = (int)topLeft.Y;

                        for (int x1 = 0; x1 < width; x1++)
                        {
                            for (int y1 = 0; y1 < height; y1++)
                            {
                                int x2 = tlX + x1;
                                int y2 = tlY + y1;

                                if (x1 == 0 && y1 == 0 && Main.tile[x2, y2].TileType == TileID.Containers)
                                    KillChestAndItems(x2, y2);

                                Main.tile[x2, y2].TileType = TileID.Dirt;
                                if (!silent)
                                    WorldGen.KillTile(x2, y2, false, false, true);

                                if (removeLiquid)
                                    GenerateLiquid(x2, y2, 0, true, 0, false);
                            }
                        }

                        for (int x1 = 0; x1 < width; x1++)
                        {
                            for (int y1 = 0; y1 < height; y1++)
                            {
                                WorldGen.SquareTileFrame(tlX + x1, tlY + y1);
                                WorldGen.SquareWallFrame(tlX + x1, tlY + y1);
                            }
                        }
                    }

                    WorldGen.destroyObject = false;

                    if (active)
                    {
                        if (width <= 1 && height <= 1 && !Main.tileFrameImportant[tile])
                        {
                            Main.tile[x, y].TileType = (ushort)tile;
                            Mtile.HasTile = true;

                            if (slope == -2 && oldHalfBrick)
                                Mtile.IsHalfBlock = true;
                            else if (slope == -1)
                                Mtile.IsHalfBlock = true;
                            else
                                Mtile.Slope = (SlopeType)(slope == -2 ? oldSlope : (byte)slope);

                            if (removeLiquid)
                                GenerateLiquid(x, y, 0, true, 0, false);

                            if (WorldGen.InWorld(x, y))
                                WorldGen.SquareTileFrame(x, y);
                        }
                        else
                        {
                            WorldGen.destroyObject = true;
                            for (int x1 = 0; x1 < width; x1++)
                                for (int y1 = 0; y1 < height; y1++)
                                    Framing.GetTileSafely(x + x1, y + y1).ClearTile();
                            WorldGen.destroyObject = false;

                            if (TileID.Sets.Platforms[tile])
                            {
                                Mtile.HasTile = true;
                                Mtile.TileType = (ushort)tile;
                                Mtile.Slope = SlopeType.Solid;
                                Mtile.IsHalfBlock = false;

                                WorldGen.SquareTileFrame(x, y);
                                if (tile >= TileID.Count && Mtile.TileFrameY != 0)
                                {
                                    Mtile.TileFrameX = 5 * 18;
                                    Mtile.TileFrameY = 0;
                                }
                            }
                            else
                            {
                                WorldGen.PlaceTile(x, y, tile, true, true, -1, tileStyle);
                                for (int x1 = 0; x1 < width; x1++)
                                    for (int y1 = 0; y1 < height; y1++)
                                        WorldGen.SquareTileFrame(x + x1, y + y1);
                            }
                        }
                    }
                    else
                    {
                        Mtile.ClearTile();
                    }
                }

                if (wall != -1)
                {
                    if (wall == -2) wall = 0;
                    Main.tile[x, y].WallType = WallID.None;
                    WorldGen.PlaceWall(x, y, wall, true);
                }

                if (sync && Main.netMode != NetmodeID.SinglePlayer)
                {
                    int size = Math.Max(width, height);
                    NetMessage.SendTileSquare(-1, x + size / 2, y + size / 2, size + 1);
                }
            }
            catch (Exception e)
            {
                BaseUtility.LogFancy("AAModClassic~ TILEGEN ERROR:", e);
            }
        }

        private static Vector2 FindTopLeft(int x, int y)
        {
            Tile tile = Framing.GetTileSafely(x, y); if (tile == null) return new Vector2(x, y);
            TileObjectData data = TileObjectData.GetTileData(tile.TileType, 0);
            if (data == null) return new Vector2(x, y);
            x -= tile.TileFrameX / 18 % data.Width;
            y -= tile.TileFrameY / 18 % data.Height;
            return new Vector2(x, y);
        }
        public static void SmoothTiles(int topX, int topY, int bottomX, int bottomY)
        {
            Main.tileSolid[137] = false;
            for (int x = topX; x < bottomX; x++)
            {
                for (int y = topY; y < bottomY; y++)
                {
                    if (Main.tile[x, y].TileType != TileID.Spikes && Main.tile[x, y].TileType != TileID.Traps && Main.tile[x, y].TileType != TileID.WoodenSpikes && Main.tile[x, y].TileType != TileID.LivingWood && Main.tile[x, y].TileType != TileID.SandstoneBrick && Main.tile[x, y].TileType != TileID.SandStoneSlab)
                    {
                        if (!Main.tile[x, y - 1].HasTile)
                        {
                            if (WorldGen.SolidTile(x, y))
                            {
                                if (!Main.tile[x - 1, y].IsHalfBlock && !Main.tile[x + 1, y].IsHalfBlock && Main.tile[x - 1, y].Slope == 0 && Main.tile[x + 1, y].Slope == 0)
                                {
                                    if (WorldGen.SolidTile(x, y + 1))
                                    {
                                        if (!WorldGen.SolidTile(x - 1, y) && !Main.tile[x - 1, y + 1].IsHalfBlock && WorldGen.SolidTile(x - 1, y + 1) && WorldGen.SolidTile(x + 1, y) && !Main.tile[x + 1, y - 1].HasTile)
                                        {
                                            if (WorldGen.genRand.NextBool(2))
                                            {
                                                WorldGen.SlopeTile(x, y, 2);
                                            }
                                            else
                                            {
                                                WorldGen.PoundTile(x, y);
                                            }
                                        }
                                        else if (!WorldGen.SolidTile(x + 1, y) && !Main.tile[x + 1, y + 1].IsHalfBlock && WorldGen.SolidTile(x + 1, y + 1) && WorldGen.SolidTile(x - 1, y) && !Main.tile[x - 1, y - 1].HasTile)
                                        {
                                            if (WorldGen.genRand.NextBool(2))
                                            {
                                                WorldGen.SlopeTile(x, y, 1);
                                            }
                                            else
                                            {
                                                WorldGen.PoundTile(x, y);
                                            }
                                        }
                                        else if (WorldGen.SolidTile(x + 1, y + 1) && WorldGen.SolidTile(x - 1, y + 1) && !Main.tile[x + 1, y].HasTile && !Main.tile[x - 1, y].HasTile)
                                        {
                                            WorldGen.PoundTile(x, y);
                                        }
                                        if (WorldGen.SolidTile(x, y))
                                        {
                                            if (WorldGen.SolidTile(x - 1, y) && WorldGen.SolidTile(x + 1, y + 2) && !Main.tile[x + 1, y].HasTile && !Main.tile[x + 1, y + 1].HasTile && !Main.tile[x - 1, y - 1].HasTile)
                                            {
                                                WorldGen.KillTile(x, y);
                                            }
                                            else if (WorldGen.SolidTile(x + 1, y) && WorldGen.SolidTile(x - 1, y + 2) && !Main.tile[x - 1, y].HasTile && !Main.tile[x - 1, y + 1].HasTile && !Main.tile[x + 1, y - 1].HasTile)
                                            {
                                                WorldGen.KillTile(x, y);
                                            }
                                            else if (!Main.tile[x - 1, y + 1].HasTile && !Main.tile[x - 1, y].HasTile && WorldGen.SolidTile(x + 1, y) && WorldGen.SolidTile(x, y + 2))
                                            {
                                                if (WorldGen.genRand.NextBool(5)) WorldGen.KillTile(x, y);
                                                else if (WorldGen.genRand.NextBool(5)) WorldGen.PoundTile(x, y);
                                                else WorldGen.SlopeTile(x, y, 2);
                                            }
                                            else if (!Main.tile[x + 1, y + 1].HasTile && !Main.tile[x + 1, y].HasTile && WorldGen.SolidTile(x - 1, y) && WorldGen.SolidTile(x, y + 2))
                                            {
                                                if (WorldGen.genRand.NextBool(5))
                                                {
                                                    WorldGen.KillTile(x, y);
                                                }
                                                else if (WorldGen.genRand.NextBool(5))
                                                {
                                                    WorldGen.PoundTile(x, y);
                                                }
                                                else
                                                {
                                                    WorldGen.SlopeTile(x, y, 1);
                                                }
                                            }
                                        }
                                    }
                                    if (WorldGen.SolidTile(x, y) && !Main.tile[x - 1, y].HasTile && !Main.tile[x + 1, y].HasTile)
                                    {
                                        WorldGen.KillTile(x, y);
                                    }
                                }
                            }
                            else if (!Main.tile[x, y].HasTile && Main.tile[x, y + 1].TileType != TileID.SandstoneBrick && Main.tile[x, y + 1].TileType != TileID.SandStoneSlab)
                            {
                                if (Main.tile[x + 1, y].TileType != TileID.MushroomBlock && Main.tile[x + 1, y].TileType != TileID.Spikes && Main.tile[x + 1, y].TileType != TileID.WoodenSpikes && WorldGen.SolidTile(x - 1, y + 1) && WorldGen.SolidTile(x + 1, y) && !Main.tile[x - 1, y].HasTile && !Main.tile[x + 1, y - 1].HasTile)
                                {
                                    WorldGen.PlaceTile(x, y, Main.tile[x, y + 1].TileType);
                                    if (WorldGen.genRand.NextBool(2))
                                    {
                                        WorldGen.SlopeTile(x, y, 2);
                                    }
                                    else
                                    {
                                        WorldGen.PoundTile(x, y);
                                    }
                                }
                                if (Main.tile[x - 1, y].TileType != TileID.MushroomBlock && Main.tile[x - 1, y].TileType != TileID.Spikes && Main.tile[x - 1, y].TileType != TileID.WoodenSpikes && WorldGen.SolidTile(x + 1, y + 1) && WorldGen.SolidTile(x - 1, y) && !Main.tile[x + 1, y].HasTile && !Main.tile[x - 1, y - 1].HasTile)
                                {
                                    WorldGen.PlaceTile(x, y, Main.tile[x, y + 1].TileType);
                                    if (WorldGen.genRand.NextBool(2))
                                    {
                                        WorldGen.SlopeTile(x, y, 1);
                                    }
                                    else
                                    {
                                        WorldGen.PoundTile(x, y);
                                    }
                                }
                            }
                        }
                        else if (!Main.tile[x, y + 1].HasTile && WorldGen.genRand.NextBool(2) && WorldGen.SolidTile(x, y) && !Main.tile[x - 1, y].IsHalfBlock && !Main.tile[x + 1, y].IsHalfBlock && Main.tile[x - 1, y].Slope == 0 && Main.tile[x + 1, y].Slope == 0 && WorldGen.SolidTile(x, y - 1))
                        {
                            if (WorldGen.SolidTile(x - 1, y) && !WorldGen.SolidTile(x + 1, y) && WorldGen.SolidTile(x - 1, y - 1))
                            {
                                WorldGen.SlopeTile(x, y, 3);
                            }
                            else if (WorldGen.SolidTile(x + 1, y) && !WorldGen.SolidTile(x - 1, y) && WorldGen.SolidTile(x + 1, y - 1))
                            {
                                WorldGen.SlopeTile(x, y, 4);
                            }
                        }
                    }
                }
            }
            for (int x = topX; x < bottomX; x++)
            {
                for (int y = topY; y < bottomY; y++)
                {
                    if (WorldGen.genRand.NextBool(2) && !Main.tile[x, y - 1].HasTile && Main.tile[x, y].TileType != TileID.Traps && Main.tile[x, y].TileType != TileID.Spikes && Main.tile[x, y].TileType != TileID.WoodenSpikes && Main.tile[x, y].TileType != TileID.LivingWood && Main.tile[x, y].TileType != TileID.SandstoneBrick && Main.tile[x, y].TileType != TileID.SandStoneSlab && Main.tile[x, y].TileType != TileID.ObsidianBrick && Main.tile[x, y].TileType != TileID.HellstoneBrick && WorldGen.SolidTile(x, y) && Main.tile[x - 1, y].TileType != TileID.Traps && Main.tile[x + 1, y].TileType != TileID.Traps)
                    {
                        if (WorldGen.SolidTile(x, y + 1) && WorldGen.SolidTile(x + 1, y) && !Main.tile[x - 1, y].HasTile)
                        {
                            WorldGen.SlopeTile(x, y, 2);
                        }
                        if (WorldGen.SolidTile(x, y + 1) && WorldGen.SolidTile(x - 1, y) && !Main.tile[x + 1, y].HasTile)
                        {
                            WorldGen.SlopeTile(x, y, 1);
                        }
                    }
                    if (Main.tile[x, y].Slope == SlopeType.SlopeDownLeft && !WorldGen.SolidTile(x - 1, y))
                    {
                        WorldGen.SlopeTile(x, y);
                        WorldGen.PoundTile(x, y);
                    }
                    if (Main.tile[x, y].Slope == SlopeType.SlopeDownRight && !WorldGen.SolidTile(x + 1, y))
                    {
                        WorldGen.SlopeTile(x, y);
                        WorldGen.PoundTile(x, y);
                    }
                }
            }
            Main.tileSolid[137] = true;
        }

        public static void AddProtectedStructure(Rectangle area, int padding = 0)
        {
            // Always add to the vanilla protected structures list.
            GenVars.structures.AddProtectedStructure(area, padding);

            Rectangle paddedArea = new Rectangle(area.X, area.Y, area.Width, area.Height);
            paddedArea.Inflate(padding, padding);

            // If Fargo's Mutant Mod is loaded, add to their Indestructible Rectangle list, which prevents structures from being trashed by Fargo's terrain tools.
            if (ModLoader.TryGetMod("Fargowiltas", out Mod fargos))
            {
                paddedArea.X *= 16;
                paddedArea.Y *= 16;
                paddedArea.Width *= 16;
                paddedArea.Height *= 16;
                fargos.Call("AddIndestructibleRectangle", paddedArea);
            }
        }

        //Gen Actions
        public class SetModTile : GenAction
        {
            public ushort type;
            public short frameX = -1;
            public short frameY = -1;
            public bool doFraming;
            public bool doNeighborFraming;
            public Func<int, int, Tile, bool> canReplace;

            public SetModTile(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true)
            {
                this.type = type;
                doFraming = setSelfFrames;
                doNeighborFraming = setNeighborFrames;
            }

            public SetModTile ExtraParams(Func<int, int, Tile, bool> canReplace, int frameX = -1, int frameY = -1)
            {
                this.canReplace = canReplace;
                this.frameX = (short)frameX;
                this.frameY = (short)frameY;
                return this;
            }

            public override bool Apply(Point origin, int x, int y, params object[] args)
            {
                if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
                    return false;
                if (canReplace == null || canReplace != null && canReplace(x, y, _tiles[x, y]))
                {
                    _tiles[x, y].ResetToType(type);
                    if (frameX > -1)
                        _tiles[x, y].TileFrameX = frameX;
                    if (frameY > -1)
                        _tiles[x, y].TileFrameY = frameY;
                    if (doFraming)
                    {
                        WorldUtils.TileFrame(x, y, doNeighborFraming);
                    }
                }
                return UnitApply(origin, x, y, args);
            }
        }

        public class PlaceModWall : GenAction
        {
            public ushort type;
            public bool neighbors;
            public Func<int, int, Tile, bool> canReplace;

            public PlaceModWall(int type, bool neighbors = true)
            {
                this.type = (ushort)type;
                this.neighbors = neighbors;
            }

            public PlaceModWall ExtraParams(Func<int, int, Tile, bool> canReplace)
            {
                this.canReplace = canReplace;
                return this;
            }

            public override bool Apply(Point origin, int x, int y, params object[] args)
            {
                if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY) return false;
                if (canReplace == null || canReplace != null && canReplace(x, y, _tiles[x, y]))
                {
                    _tiles[x, y].WallType = type;
                    WorldGen.SquareWallFrame(x, y);
                    if (neighbors)
                    {
                        WorldGen.SquareWallFrame(x + 1, y);
                        WorldGen.SquareWallFrame(x - 1, y);
                        WorldGen.SquareWallFrame(x, y - 1);
                        WorldGen.SquareWallFrame(x, y + 1);
                    }
                }
                return UnitApply(origin, x, y, args);
            }
        }

        public class RadialDitherTopMiddle : GenAction
        {
            private int _width, _height;
            private float _innerRadius, _outerRadius;

            public RadialDitherTopMiddle(int width, int height, float innerRadius, float outerRadius)
            {
                _width = width;
                _height = height;
                _innerRadius = innerRadius;
                _outerRadius = outerRadius;
            }

            public override bool Apply(Point origin, int x, int y, params object[] args)
            {
                Vector2 value = new((float)origin.X + _width / 2, origin.Y);
                Vector2 value2 = new(x, y);
                float num = Vector2.Distance(value2, value);
                float num2 = Math.Max(0f, Math.Min(1f, (num - _innerRadius) / (_outerRadius - _innerRadius)));
                if (_random.NextDouble() > num2)
                {
                    return UnitApply(origin, x, y, args);
                }
                return Fail();
            }
        }

        public class RadialDitherCenter : GenAction
        {
            private int _width, _height;
            private float _innerRadius, _outerRadius;

            public RadialDitherCenter(int width, int height, float innerRadius, float outerRadius)
            {
                _width = width;
                _height = height;
                _innerRadius = innerRadius;
                _outerRadius = outerRadius;
            }

            public override bool Apply(Point origin, int x, int y, params object[] args)
            {
                Vector2 value = new((float)origin.X + _width / 2, (float)origin.Y + _height / 2);
                Vector2 value2 = new(x, y);
                float num = Vector2.Distance(value2, value);
                float num2 = Math.Max(0f, Math.Min(1f, (num - _innerRadius) / (_outerRadius - _innerRadius)));
                if (_random.NextDouble() > num2)
                {
                    return UnitApply(origin, x, y, args);
                }
                return Fail();
            }
        }

        public class InWorld : GenAction
        {
            public InWorld()
            {
            }

            public override bool Apply(Point origin, int x, int y, params object[] args)
            {
                if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
                    return Fail();
                return UnitApply(origin, x, y, args);
            }
        }

        public class ConvertTile : GenAction
        {
            int conversionType = -1;
            public ConvertTile(int type)
            {
                conversionType = type;
            }

            public override bool Apply(Point origin, int x, int y, params object[] args)
            {
                if (conversionType == -1)
                    return Fail();

                if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
                    return Fail();

                WorldGen.Convert(x, y, conversionType, 1, true, true);

                return UnitApply(origin, x, y, args);
            }
        }
    }
}

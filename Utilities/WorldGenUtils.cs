using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic.Utilities
{
    public class WorldGenUtils
    {
        public static readonly bool[] AllTilesAllowed = Enumerable.Repeat(true, TileLoader.TileCount).ToArray();

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
        public static int GetWorldSize() => Main.maxTilesX >= 8400 ? 3 : Main.maxTilesX >= 6400 ? 2 : 1;

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

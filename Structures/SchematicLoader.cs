using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Structures
{
    public sealed class ResolvedSchematic
    {
        public SchematicData Data { get; }

        public int[] TileIds { get; }

        public int[] WallIds { get; }

        public Dictionary<string, int> ItemIds { get; } = [];

        public List<string> Warnings { get; } = [];

        public int Width => Data.Width;
        public int Height => Data.Height;

        internal ResolvedSchematic(SchematicData data)
        {
            Data = data;
            TileIds = new int[data.Tiles.Count];
            WallIds = new int[data.Walls.Count];
            Array.Fill(TileIds, -1);
            Array.Fill(WallIds, -1);
        }
    }

    public static class SchematicLoader
    {
        public static SchematicData ReadFile(string path)
        {
            using FileStream stream = File.OpenRead(path);
            return SchematicIO.Read(stream);
        }

        public static SchematicData ReadFromMod(Mod mod, string path)
        {
            using Stream stream = mod.GetFileStream(path);
            return SchematicIO.Read(stream);
        }

        public static ResolvedSchematic Resolve(SchematicData data)
        {
            var resolved = new ResolvedSchematic(data);

            for (int i = 1; i < data.Tiles.Count; i++)
            {
                if (TryResolveTile(data.Tiles[i], out int type))
                    resolved.TileIds[i] = type;
                else
                    resolved.Warnings.Add($"Tile '{data.Tiles[i]}' could not be found; cells using it are left empty.");
            }

            for (int i = 1; i < data.Walls.Count; i++)
            {
                if (TryResolveWall(data.Walls[i], out int type))
                    resolved.WallIds[i] = type;
                else
                    resolved.Warnings.Add($"Wall '{data.Walls[i]}' could not be found; cells using it get no wall.");
            }

            EnsureChestData(data, resolved);

            foreach (SchematicChest chest in data.Chests)
            {
                foreach (SchematicChestItem item in chest.Items)
                {
                    if (resolved.ItemIds.ContainsKey(item.Item))
                        continue;

                    if (TryResolveItem(item.Item, out int type))
                        resolved.ItemIds[item.Item] = type;
                    else
                        resolved.Warnings.Add($"Item '{item.Item}' could not be found; it is skipped in chests.");
                }
            }

            return resolved;
        }

        private static void EnsureChestData(SchematicData data, ResolvedSchematic resolved)
        {
            HashSet<Point> existingChests = [];

            foreach (SchematicChest chest in data.Chests)
                existingChests.Add(new Point(chest.X, chest.Y));

            for (int x = 0; x < data.Width; x++)
            {
                for (int y = 0; y < data.Height; y++)
                {
                    int index = data.CellIndex(x, y);

                    if (!SchematicCell.Has(data.Flags[index], SchematicCell.HasTile))
                        continue;

                    int tileIndex = data.TileIndex[index];

                    if (tileIndex <= 0 || tileIndex >= resolved.TileIds.Length)
                        continue;

                    int tileType = resolved.TileIds[tileIndex];

                    if (tileType < 0 || !TileID.Sets.IsAContainer[tileType])
                        continue;

                    TileObjectData objectData = TileObjectData.GetTileData(
                        tileType,
                        data.FrameX[index],
                        data.FrameY[index]);

                    if (objectData == null)
                        continue;

                    int sheet = 16 + objectData.CoordinatePadding;

                    int originX = x - data.FrameX[index] / sheet % objectData.Width;
                    int originY = y - data.FrameY[index] / sheet % objectData.Height;

                    if ((uint)originX >= (uint)data.Width ||
                        (uint)originY >= (uint)data.Height)
                        continue;

                    Point origin = new(originX, originY);

                    if (!existingChests.Add(origin))
                        continue;

                    data.Chests.Add(new SchematicChest
                    {
                        X = originX,
                        Y = originY
                    });
                }
            }
        }

        private const string VanillaPrefix = "Terraria/";

        public static bool TryResolveTile(string name, out int type)
        {
            type = -1;
            if (name.StartsWith(VanillaPrefix, StringComparison.Ordinal))
                return TileID.Search.TryGetId(name.Substring(VanillaPrefix.Length), out type);

            if (ModContent.TryFind(name, out ModTile modTile))
            {
                type = modTile.Type;
                return true;
            }
            return false;
        }

        public static bool TryResolveWall(string name, out int type)
        {
            type = -1;
            if (name.StartsWith(VanillaPrefix, StringComparison.Ordinal))
                return WallID.Search.TryGetId(name.Substring(VanillaPrefix.Length), out type);

            if (ModContent.TryFind(name, out ModWall modWall))
            {
                type = modWall.Type;
                return true;
            }
            return false;
        }

        public static bool TryResolveItem(string name, out int type)
        {
            type = -1;
            if (name.StartsWith(VanillaPrefix, StringComparison.Ordinal))
                return ItemID.Search.TryGetId(name.Substring(VanillaPrefix.Length), out type);

            if (ModContent.TryFind(name, out ModItem modItem))
            {
                type = modItem.Type;
                return true;
            }
            return false;
        }
    }
}
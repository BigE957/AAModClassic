using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Utilities
{
    public static class TreeExtensions
    {
        public static Vector2 GetRandomTreePosition(this ModTree tree, int x, int y)
        {
            var size = tree.GetTreeSize(x, y);
            var halfSize = size / 2f;
            var offset = new Vector2(Main.rand.NextFloat(-halfSize.X, halfSize.X), -Main.rand.NextFloat(size.Y * 0.1f, size.Y * 0.8f));
            return offset;
        }

        public static Vector2 GetTreeSize(this ModTree tree, int x, int y)
        {
            int _ = 0;
            int width = 0;
            int height = 0;
            tree.SetTreeFoliageSettings(x, y, Main.tile[x, y], _, ref _, _, ref width, ref height);
            return new Vector2(width, height);
        }
    }
}

using Microsoft.Xna.Framework;
using Terraria;

namespace AAMod
{
    public class ColorUtils
    {
        public static Color COLOR_GLOWPULSE => Color.White * (Main.mouseTextColor / 255f);
    }

    public static class ItemUtils
    {
        public static void DropLoot(this Entity ent, int type, int stack = 1)
        {
            Item.NewItem(Item.GetSource_NaturalSpawn(), ent.Hitbox, type, stack);
        }

        public static void DropLoot(this Entity ent, int type, float chance)
        {
            if (Main.rand.NextDouble() < chance)
            {
                Item.NewItem(Item.GetSource_NaturalSpawn(), ent.Hitbox, type);
            }
        }

        public static void DropLoot(this Entity ent, int type, int min, int max)
        {
            Item.NewItem(Item.GetSource_NaturalSpawn(), ent.Hitbox, type, Main.rand.Next(min, max));
        }
    }
}

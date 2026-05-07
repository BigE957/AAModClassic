using Terraria;

namespace AAModClassic.Utilities
{
    public static class ItemUtils
    {
        public static void DropLoot(this Entity ent, int type, int stack = 1)
        {
            Item.NewItem(ent.GetSource_Loot(), ent.Hitbox, type, stack);
        }

        public static void DropLoot(this Entity ent, int type, float chance)
        {
            if (Main.rand.NextDouble() < chance)
            {
                Item.NewItem(ent.GetSource_Loot(), ent.Hitbox, type);
            }
        }

        public static void DropLoot(this Entity ent, int type, int min, int max)
        {
            Item.NewItem(ent.GetSource_Loot(), ent.Hitbox, type, Main.rand.Next(min, max));
        }
    }
}

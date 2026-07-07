using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle GetFrame(int itemID, int whoAmI, Texture2D texture = null)
        {
            texture ??= TextureAssets.Item[itemID].Value;
            return Main.itemAnimations[itemID] == null
                ? texture.Frame()
                : Main.itemAnimations[itemID].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle GetFrame(this Item item, int whoAmI, Texture2D texture = null)
        {
            return GetFrame(item.type, whoAmI, texture);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle GetFrame(int itemID, Texture2D texture = null)
        {
            texture ??= TextureAssets.Item[itemID].Value;
            return Main.itemAnimations[itemID] == null
                ? texture.Frame()
                : Main.itemAnimations[itemID].GetFrame(texture);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle GetFrame(this Item item, Texture2D texture = null)
        {
            return GetFrame(item.type, texture);
        }
    }
}

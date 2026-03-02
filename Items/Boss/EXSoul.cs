using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Boss
{
    public class EXSoul : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("EX Soul");
            // Tooltip.SetDefault("Essence of ancient, arcane magic");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 4));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            Item.width = refItem.width;
            Item.height = refItem.height;
            Item.maxStack = 999;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true; Item.expertOnly = true;
            
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Main.DiscoColor; //GConstants.COLOR_RARITYN1;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Main.DiscoColor.ToVector3() * 0.55f * Main.essScale);
        }
    }
}
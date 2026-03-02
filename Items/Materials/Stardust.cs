using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Materials
{
    public class Stardust : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            // DisplayName.SetDefault("Radiant Photon");
            // Tooltip.SetDefault("A shard of the heavenly cosmos");
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Purple;
            Item.value = 10000;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Glow;
        }
    }
}

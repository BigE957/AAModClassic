using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Materials
{
    public class ChaosSoul : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Soul");
            // Tooltip.SetDefault("Solid discord, symbolizing unrest and Anarchy itself");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 7));
            ItemID.Sets.ItemNoGravity[Item.type] = true;

        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.expert = true; 
            Item.expertOnly = true;
            Item.alpha = 25;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
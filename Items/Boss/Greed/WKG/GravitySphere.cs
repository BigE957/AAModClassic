using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class GravitySphere : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gravity Sphere");
            // Tooltip.SetDefault("A stone model of the planet, complete with an orbitting moon!");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 8));
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = 999;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
        }
    }
}
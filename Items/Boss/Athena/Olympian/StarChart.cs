using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAModClassic.Items.Boss.Athena.Olympian
{
    public class StarChart : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Star Chart");
            // Tooltip.SetDefault("A map of the solar system up to this planet.");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 8));
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = 9999;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.55f * Main.essScale);
        }
    }
}
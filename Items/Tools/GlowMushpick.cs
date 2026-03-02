using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Tools
{
    public class GlowMushpick : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 10;
            Item.useAnimation = 19;
            Item.pick = 55;
            Item.useStyle = 1;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing Mushpick");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GlowingMushroom, 5);
            recipe.AddIngredient(null, "GlowingMushiumBar", 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}

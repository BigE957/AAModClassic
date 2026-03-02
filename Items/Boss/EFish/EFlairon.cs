using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.EFish
{
    public class EFlairon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Emperor Flairon");
            // Tooltip.SetDefault("Lets loose an armada of homing bubbles");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Flairon);
            Item.damage = 350;
            Item.rare = 11;
            Item.shoot = Mod.Find<ModProjectile>("EFlairon").Type;
        }



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Flairon);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}
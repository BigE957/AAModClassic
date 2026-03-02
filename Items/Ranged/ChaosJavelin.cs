using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class ChaosJavelin : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Javelin");
            // Tooltip.SetDefault("Explodes on contact");
        }

        public override void SetDefaults()
        {
            Item.shoot = Mod.Find<ModProjectile>("ChaosJavelin").Type;
            Item.shootSpeed = 15f;
            Item.damage = 105;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.width = 30;
            Item.height = 30;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 8;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "PrimevalJavelin");
            recipe.AddIngredient(null, "ChaosCrystal");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}

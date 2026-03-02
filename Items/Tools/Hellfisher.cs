using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Hellfisher : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Hellfisher");
		}

		public override void SetDefaults()
		{
            Item.CloneDefaults(ItemID.HotlineFishingHook);
            Item.shoot = ModContent.ProjectileType<Hellfisher_Bob>();
		}

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "IncineriteBar", 12);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
        }
	}
}
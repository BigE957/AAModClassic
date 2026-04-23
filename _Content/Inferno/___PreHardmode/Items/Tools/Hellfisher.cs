using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tools
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
            Item.shoot = ModContent.ProjectileType<Hellfisher_Bobber>();
		}

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 12);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
        }
	}
}
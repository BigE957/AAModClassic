using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Tools
{
    public class DragonDigger : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Dragon Digger");
            // Tooltip.SetDefault("Dragons don't really dig, but this'll do");
		}

		public override void SetDefaults()
		{
			Item.damage = 14;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 22;
			Item.useAnimation = 22;
			Item.pick = 75;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = 3600;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "IncineriteBar", 12);
                recipe.AddIngredient(null, "BroodScale", 6);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
        }
	}
}
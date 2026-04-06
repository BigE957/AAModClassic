using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Tools
{
    //ported from my tAPI mod because I don't want to make more artwork
    public class HydraTuneller : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Hydra Tuneller");
            // Tooltip.SetDefault("Okay, this is getting rediculous. Hydras don't use drills.");
		}

		public override void SetDefaults()
		{
			Item.damage = 6;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 50;
			Item.height = 18;
			Item.useTime = 10;
			Item.useAnimation = 15;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.pick = 65;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = Item.sellPrice(0, 0, 30, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<HydraTuneller_Proj>();
			Item.shootSpeed = 40f;
		}

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "AbyssiumBar", 12);
                recipe.AddIngredient(null, "HydraHide", 6);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
        }
    }
}
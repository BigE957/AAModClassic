using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Tools
{
    public class PerfectShadowDrill : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Perfect Shadow Drill");
            // Tooltip.SetDefault("Now that's more like it.");
		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 50;
			Item.height = 18;
			Item.useTime = 6;
			Item.useAnimation = 15;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.pick = 205;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 10);
            Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.PShadowDrill>();
			Item.shootSpeed = 40f;
		}

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "ShadowDrill");
            recipe.AddIngredient(Mod, "HeroShards");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
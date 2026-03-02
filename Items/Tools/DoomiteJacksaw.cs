using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class DoomiteJacksaw : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Doomite Jacksaw");
            // Tooltip.SetDefault("Engineered for ultimate tree and wall breaking action!");
		}

		public override void SetDefaults()
		{
			Item.damage = 18;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 50;
			Item.height = 18;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useAnimation = 15;
			Item.useTime = 12;
			Item.hammer = 70;
			Item.axe = 30;
			Item.useStyle = 5;
			Item.knockBack = 0;
			Item.value = 15000;
			Item.rare = 4;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("DoomiteJacksaw").Type;
			Item.shootSpeed = 40f;
		}

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "Doomite", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ScourgeOfShadows : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scourge of the Shadows");
            // Tooltip.SetDefault("Bounce off tiles up to 3 times\nBreaks after hitting an enemy\nSprays little eaters while travelling and on enemy hit\nScourge of the Corruptor EX");
        }

        public override void SetDefaults()
		{
            Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.shoot = Mod.Find<ModProjectile>("ScourgeOfShadowsP").Type;
			Item.damage = 130;
			Item.width = 18;
			Item.height = 20;
			Item.UseSound = SoundID.Item39;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 50, 0, 0);
			Item.knockBack = 5f;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.rare = ItemRarityID.Purple;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ScourgeoftheCorruptor);
            recipe.AddIngredient(null, "EXSoul");
		    recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
		}
    }
}

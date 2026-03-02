using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class AmphibianLongswordEXS : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Amphibious Greatblade");
            // Tooltip.SetDefault(@"Amphibious Longsword EX");
        }
		public override void SetDefaults()
		{
			Item.damage = 350;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 64;
			Item.height = 64;
            Item.useTime = 30;
			Item.useAnimation = 30;
            Item.useStyle = 1;
			Item.knockBack = 7;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = 9;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("AmphibiousProjectileEXS").Type;
            Item.shootSpeed = 18f;
            Item.expert = true; Item.expertOnly = true;
		}
        
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Wet, 1000);
        }
        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "AmphibianLongswordEX");
                recipe.AddIngredient(null, "ShinyCharm");
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "AmphibianLongswordS");
                recipe.AddIngredient(null, "EXSoul");
                recipe.AddTile(null, "QuantumFusionAccelerator");
                recipe.Register();
            }
        }
    }
}

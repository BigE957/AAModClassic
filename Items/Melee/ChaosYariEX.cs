using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Melee
{
    public class ChaosYariEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Perfect Chaos Yari");
            // Tooltip.SetDefault("Chaos Yari EX");
        }

        public override void SetDefaults()
        {
            Item.damage = 180;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(5, 0, 0, 0);
            Item.rare = 11;
            Item.expert = true; Item.expertOnly = true;
            Item.shootSpeed = 12f;
            Item.shoot = Mod.Find<ModProjectile>("ChaosYariEX").Type;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "ChaosYari", 1);
            recipe.AddIngredient(Mod, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.Register();
		}
    }
}
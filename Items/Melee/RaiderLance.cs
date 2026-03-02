using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using AAModClassic;

namespace AAModClassic.Items.Melee
{
    public class RaiderLance : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Raider Lance");		
		}

        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 1;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.knockBack = 2.3f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = 10800;
            Item.rare = ItemRarityID.Orange;
            Item.shootSpeed = 7f;
            Item.shoot = Mod.Find<ModProjectile>("RaiderLance").Type;  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "VikingPolearm", 1);
            recipe.AddIngredient(Mod, "HydrasSpear", 1);
            recipe.AddIngredient(Mod, "SaltwaterSpear", 1);
            recipe.AddIngredient(Mod, "Executioner", 1);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
    }
}
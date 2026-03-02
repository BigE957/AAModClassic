using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace AAModClassic.Items.Melee
{
    public class AsgardianLance : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Asgardian Lance");		
		}

        public override void SetDefaults()
        {
            Item.damage = 80;
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
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.shootSpeed = 10f;
            Item.shoot = Mod.Find<ModProjectile>("AsgardianLance").Type;  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<Projectiles.AsgardianLanceShot>(), damage, knockBack, Main.myPlayer);
            return true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "RaiderLance", 1);
            recipe.AddIngredient(Mod, "HeroShards", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
    }
}
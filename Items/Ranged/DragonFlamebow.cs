using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class DragonFlamebow : BaseAAItem
    {

        public override void SetDefaults()
        {

            Item.damage = 14;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 60;
            Item.scale *= .8f;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<Projectiles.DragonArrow>();
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 2;
            Item.value = 1000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 25f;

        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Flamebow");
            // Tooltip.SetDefault("Transforms arrows into Dragon Arrows");
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<Projectiles.DragonArrow>(), damage, knockBack, player.whoAmI, 0f, 0f); //This is spawning a projectile of type FrostburnArrow using the original stats
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "IncineriteBar", 8);
			recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}

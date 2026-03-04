using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class DragonsMaw : BaseAAItem
    {

        public override void SetDefaults()
        {

            Item.damage = 30;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 60;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.Shuriken;
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 25f;
            Item.value = Item.sellPrice(0, 1, 0, 0);

        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragons Maw");
            // Tooltip.SetDefault("Transforms arrows into Dragon Arrows");
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			float rotation = MathHelper.ToRadians(5);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < 2; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i));
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("DragonLaser").Type, damage, knockback, player.whoAmI);
			}
            Projectile.NewProjectile(source, position, velocity, Mod.Find<ModProjectile>("DragonArrow").Type, damage, knockback, player.whoAmI, 0f, 0f); //This is spawning a projectile of type FrostburnArrow using the original stats
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DragonSpirit", 25);
			recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}

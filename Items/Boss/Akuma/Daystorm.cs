using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Projectiles.Akuma;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Akuma
{
    public class Daystorm : BaseAAItem
    { 

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Daystorm");
            // Tooltip.SetDefault(@"Incinerate your enemies in a storm of scorching fiery mayhem");
        }       

        public override void SetDefaults()
		{
			Item.damage = 225;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 9;
			Item.width = 100;
			Item.height = 100;
			Item.useTime = 7;
			Item.useAnimation = 7;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; 
			Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 7, 0, 0);
            Item.rare = ItemRarityID.Yellow;
			Item.UseSound = new SoundStyle("AAModClassic/Sounds/Dayshot");
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 30;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        int shoot = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));
            //    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("Dayser"), damage, knockback, player.whoAmI);
            //}
            if (shoot++ > 6) shoot = 0;

            for (int i = 0; i < 4; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15)) * .5f;
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Daystormbullet>(), damage, knockback, player.whoAmI);
            }

            if (Main.rand.NextBool(3))
            {
                //Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
                //Projectile.NewProjectile(position, velocity, mod.ProjectileType("Dayser"), damage * 2, knockback, player.whoAmI);
                //shoot = 0;
                for (int i = 0; i < Main.rand.Next(2); i++)
                {
                    Vector2 perturbedSpeed2 = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, ModContent.ProjectileType<DaystormbulletA>(), (int)(damage * 1.5f), knockback, player.whoAmI);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ItemID.LaserMachinegun);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}

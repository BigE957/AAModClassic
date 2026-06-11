using Terraria;
using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos.__Hardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class PerfectChaosBustershot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Perfect Chaos Bustershot");
            /* Tooltip.SetDefault(@"Fires a piercing dualblast as well as a spread of 10 bullets
Chaos Bustershot EX"); */
        }

        public override void SetDefaults()
        {

            Item.damage = 300;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 20;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(5, 0, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item14;
            Item.shootSpeed = 12f;
            Item.expert = true; Item.expertOnly = true;
            Item.autoReuse = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, -2);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float spread = 20f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
		    double offsetAngle;
		    for (int i = 0; i < 10; i++)
		    {
		    	offsetAngle = startAngle + deltaAngle * i;
		    	Projectile.NewProjectile(source, position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), type, damage, knockback, Main.myPlayer);
            }
            for (int m = 0; m < 2; m++)
            {
                Projectile.NewProjectile(source, position, velocity, m == 0 ? ModContent.ProjectileType<PerfectChaosBustershot_ChaosShot2>() : ModContent.ProjectileType<PerfectChaosBustershot_ChaosShot3>(), damage, knockback, player.whoAmI, 0, 1);
            }

            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<PerfectChaosBustershot_ChaosShot1>(), damage, knockback, player.whoAmI, 0, 1);
            return false;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ChaosBustershot>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}

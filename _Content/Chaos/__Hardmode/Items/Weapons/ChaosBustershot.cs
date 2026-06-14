using Terraria;
using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAModClassic._Content._EX._PostMoonlord.Items.Weapons;
using AAModClassic._Content.Mire.__Hardmode.Items.Weapons;
using AAModClassic._Content.Chaos.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Weapons
{
    public class ChaosBustershot : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Bustershot");
            // Tooltip.SetDefault("Fires a piercing dualblast as well as a spread of 6 bullets");
        }

        public override void SetDefaults()
        {

            Item.damage = 37;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 20;
            Item.useTime = 38;
            Item.useAnimation = 38;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item14;
            Item.shootSpeed = 20f;
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
		    for (int i = 0; i < 6; i++)
		    {
		    	offsetAngle = startAngle + deltaAngle * i;
		    	Projectile.NewProjectile(source, position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), type, damage, knockback, Main.myPlayer);
            }
            for (int m = 0; m < 2; m++)
            {
                Projectile.NewProjectile(source, position, velocity, m == 0 ? ModContent.ProjectileType<PerfectChaosBustershot_ChaosShot2>() : ModContent.ProjectileType<PerfectChaosBustershot_ChaosShot3>(), damage, knockback, player.whoAmI);
            }
            return false;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AbyssalShadowshot>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChaosPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}

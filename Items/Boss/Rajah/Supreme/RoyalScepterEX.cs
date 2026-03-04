using Microsoft.Xna.Framework;
using Terraria;
using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Projectiles.Rajah.Supreme;
using AAModClassic;

namespace AAModClassic.Items.Boss.Rajah.Supreme
{
    public class RoyalScepterEX : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.mana = 5;
            Item.damage = 260;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 9f;
            Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<CarrotEX>();
            Item.width = 58;
            Item.height = 57;
            Item.UseSound = SoundID.Item39;
            Item.useAnimation = 30;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Yellow;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.DamageType = DamageClass.Magic;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Royal Staff");
            /* Tooltip.SetDefault(@"Shoots a spread of five golden carrots
Royal Scepter EX"); */
            Item.staff[Item.type] = true;
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float spread = 45f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
		    double deltaAngle = spread / 10f;
		    double offsetAngle;
		    for (int i = 0; i < 5; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	int proj = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), type, damage, knockback, Main.myPlayer);
                Main.projectile[proj].DamageType = DamageClass.Magic;
            }
		    return false;
		}
    }
}

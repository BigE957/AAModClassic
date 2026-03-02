using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Djinn
{
    public class SandstormCrossbow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sandstorm Crossbow");
            // Tooltip.SetDefault("Replaces arrows with desert bolts");
        }

	    public override void SetDefaults()
	    {
	        Item.damage = 28;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 40;
	        Item.height = 26;
	        Item.useTime = 19;
	        Item.reuseDelay = 0;
	        Item.useAnimation = 19;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
	        Item.value = 50000;
	        Item.rare = ItemRarityID.Orange;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 8f;
	        Item.useAmmo = 40;
	    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int Shoot = Main.rand.Next(2);
            switch (Shoot)
            {
                case 0:
                    Shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Djinn.DesertBolt1>();
                    break;
                default:
                    Shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Djinn.DesertBolt2>();
                    break;
            }
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, Shoot, damage, knockBack, player.whoAmI, 0f, 0f);
        
            return false;
        }
	}
}
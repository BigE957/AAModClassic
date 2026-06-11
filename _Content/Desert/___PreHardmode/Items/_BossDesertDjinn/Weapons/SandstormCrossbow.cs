using System;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Weapons
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
                    Shoot = ModContent.ProjectileType<SandstormCrossbow_DesertBoltBlue>();
                    break;
                default:
                    Shoot = ModContent.ProjectileType<SandstormCrossbow_DesertBoltRed>();
                    break;
            }
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, Shoot, damage, knockback, player.whoAmI, 0f, 0f);
        
            return false;
        }
	}
}
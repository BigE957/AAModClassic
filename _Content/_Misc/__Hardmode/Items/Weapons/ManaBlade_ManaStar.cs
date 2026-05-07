using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Weapons
{
    public class ManaBlade_ManaStar : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.aiStyle = ProjAIStyleID.Beam;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.timeLeft = 1200;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mana Star");
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		
		int HomeOnTarget()
		{
			const bool homingCanAimAtWetEnemies = true;
			const float homingMaximumRangeInPixels = 1000;

			int selectedTarget = -1;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC n = Main.npc[i];
				if(n.CanBeChasedBy(Projectile) && (!n.wet || homingCanAimAtWetEnemies))
				{
					float distance = Projectile.Distance(n.Center);
					if(distance <= homingMaximumRangeInPixels &&
						(
						selectedTarget == -1 ||  //there is no selected target
						Projectile.Distance(Main.npc[selectedTarget].Center) > distance) 
						)
					{
						selectedTarget = i;
					}
				}
			}
			return selectedTarget;
		}
		
		public override void AI()
		{
			if (Main.rand.NextBool(2))
			{
				int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.MagicDust>(), 0f, 0f, 200, default, 0.8f);
				Main.dust[dustnumber].velocity *= 0.3f;
			}
			
			const int aislotHomingCooldown = 0;
			const int homingDelay = 30;
			const float desiredFlySpeedInPixelsPerFrame = 10; 
			const float amountOfFramesToLerpBy = 5; 

			Projectile.ai[aislotHomingCooldown]++;
			if(Projectile.ai[aislotHomingCooldown] > homingDelay)
			{
				Projectile.ai[aislotHomingCooldown] = homingDelay; 

				int foundTarget = HomeOnTarget();
				if(foundTarget != -1)
				{
					NPC n = Main.npc[foundTarget];
					Vector2 desiredVelocity = Projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
					Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
				}
			}
		}
	}
}
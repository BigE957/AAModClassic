using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeitySkull;
using AAModClassic.Items.Boss.Grips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ClawBaton_Holdout : ModProjectile
	{
        public override string Texture => ModContent.GetInstance<ClawBaton>().Texture;

		public override void SetDefaults()
		{
			Projectile.width = 72;
			Projectile.height = 60;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ownerHitCheck = true;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 26;
			AIType = ProjectileID.Bullet;
		}
		
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];	
			
			Projectile.ai[0]++;
			
			if (player.dead)
			{
				Projectile.Kill();
				return;
			}
			
			if (player.direction > 0)
			{
				Projectile.rotation += 0.35f;
				Projectile.spriteDirection = 1;
			}
			else
			{
				Projectile.rotation -= 0.35f;
				Projectile.spriteDirection = -1;
			}
			
			Projectile.position.X = player.Center.X - (Projectile.width / 2f);
			Projectile.position.Y = player.Center.Y - (Projectile.height / 2f);
			
			if (Projectile.timeLeft < 8)
			{
				Projectile.alpha = 100;
			}
			if (Projectile.timeLeft < 6)
			{
				Projectile.alpha = 140;
			}
			if (Projectile.timeLeft < 4)
			{
				Projectile.alpha = 180;
			}
			if (Projectile.timeLeft < 2)
			{
				Projectile.alpha = 220;
			}
		}
	}
}
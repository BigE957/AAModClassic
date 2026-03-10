using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class OceanicArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Oceanic Arrow");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.FrostburnArrow);
			Projectile.width = 14;
			Projectile.height = 18;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			AIType = ProjectileID.FrostburnArrow;
            Projectile.arrow = true;
        }

		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.TargetDamageMultiplier *= 2;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 1;
			Projectile.Kill();
		}
		
		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item112, Projectile.position);
			for (int h = 0; h < 4; h++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.3f;
				vel = vel.RotatedBy(rand);
				vel *= 4f;
				int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, vel.X, vel.Y, ProjectileID.FlaironBubble, Projectile.damage/4, 0, Main.myPlayer);
				Main.projectile[proj].DamageType = DamageClass.Ranged;
			}
		}
	}
}
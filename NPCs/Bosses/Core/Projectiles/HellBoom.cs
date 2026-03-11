using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Core.Projectiles;

public class HellBoom : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//((ModProjectile)this).DisplayName.SetDefault("Hellfire Blast");
		Main.projFrames[Projectile.type] = 7;
	}

	public override void SetDefaults()
	{
		Projectile.width = 98;
		Projectile.height = 98;
		Projectile.penetrate = -1;
		Projectile.hostile = true;
		Projectile.friendly = false;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		if (++Projectile.frameCounter >= 6)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 7)
			{
				Projectile.Kill();
			}
		}
		Projectile.velocity.X *= 0f;
		Projectile.velocity.Y *= 0f;
	}

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.OnFire, 300);
	}

	public override void OnKill(int timeLeft)
	{
		Projectile.timeLeft = 0;
	}
}

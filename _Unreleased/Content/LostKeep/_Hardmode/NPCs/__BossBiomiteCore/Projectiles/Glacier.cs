using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep._Hardmode.NPCs.__BossBiomiteCore.Projectiles;

public class Glacier : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//((ModProjectile)this).DisplayName.SetDefault("Glacier");
		Main.projFrames[Projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		Projectile.width = 104;
		Projectile.height = 78;
		Projectile.penetrate = -1;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		if (++Projectile.frameCounter >= 5)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 4)
			{
				Projectile.frame = 3;
			}
		}
		Projectile.velocity.X *= 0f;
		Projectile.velocity.Y *= 0f;
		if (Projectile.timeLeft < 60)
		{
			Projectile.alpha += 5;
		}
	}
}

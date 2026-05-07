using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.__Hardmode.NPCs._BossBiomiteCore.Projectiles;

public class GlacierChunk : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(261);
		Projectile.penetrate = 1;
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.hostile = true;
		Projectile.timeLeft = 300;
		Projectile.penetrate = -1;
	}

	public override void SetStaticDefaults()
	{
		//((ModProjectile)this).DisplayName.SetDefault("Glacier Bomb");
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.Kill();
		return true;
	}

	public override void OnKill(int a)
	{
		int num = ModContent.ProjectileType<Glacier>();
		int num2 = 48;
		
		SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
		Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center + new Vector2(0f, (float)(-num2)), Vector2.Zero, num, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
	}
}

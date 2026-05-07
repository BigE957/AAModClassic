using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.__Hardmode.NPCs._BossBiomiteCore.Projectiles;

public class RockFall : ModProjectile
{
	public override string Texture => "AAModClassic/BlankTex";

	public override void SetDefaults()
	{
		Projectile.width = 250;
		Projectile.height = 2;
		Projectile.hostile = true;
		Projectile.penetrate = 1;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = true;
	}

	public override void AI()
	{
		Projectile.ai[0] += 1f;
		if (Main.npc[(int)Projectile.ai[0]].ai[1] != 2f)
		{
			((Entity)Projectile).active = false;
		}
	}
}

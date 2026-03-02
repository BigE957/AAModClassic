using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Tools
{
    //ported from my tAPI mod because I don't want to make artwork
    public class ShadowDrill : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = ProjAIStyleID.Drill;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true; //so you can't hit enemies through walls
			Projectile.DamageType = DamageClass.Melee;
		}
	}
}
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Tools
{
    //ported from my tAPI mod because I don't want to make artwork
    public class DoomiteJacksaw : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 22;
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
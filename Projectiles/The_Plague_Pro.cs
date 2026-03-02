using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class The_Plague_Pro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 300f;
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 13f;
		}

		public override void SetDefaults()
		{
			Projectile.extraUpdates = 0;
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = ProjAIStyleID.Yoyo;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.scale = 1f;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.OnFire, 300);
			target.AddBuff(BuffID.Confused, 300);
			target.AddBuff(BuffID.Bleeding, 300);
			target.AddBuff(BuffID.BrokenArmor, 300);
			target.AddBuff(BuffID.Frostburn, 300);
			target.AddBuff(BuffID.Chilled, 300);
			target.AddBuff(BuffID.WitheredWeapon, 300);
		}
	}
}

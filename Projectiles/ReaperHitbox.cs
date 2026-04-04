using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ReaperHitbox : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reaper Hitbox");
		}

		public override void SetDefaults()
		{
			Projectile.width = 120;
			Projectile.height = 120;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 30;
			Projectile.tileCollide = false;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Projectile.Center = player.Center;
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers)
		{
			Player player = Main.player[Projectile.owner]; 
			if (player.HasBuff(ModContent.BuffType<ReaperImmune2>()))
			{
				modifiers.TargetDamageMultiplier *= 15;
			}
			else if (player.HasBuff(ModContent.BuffType<ReaperImmune>()))
			{
				modifiers.TargetDamageMultiplier *= 10;
			}
		}
	}
}

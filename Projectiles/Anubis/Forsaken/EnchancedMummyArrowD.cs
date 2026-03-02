using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Anubis.Forsaken
{
    public class EnchancedMummyArrowD : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Enchanced Mummy Arrow");
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.ignoreWater = false;
			Projectile.tileCollide = true;
            Projectile.arrow = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 0;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			target.buffImmune[Mod.Find<ModBuff>("Forsaken").Type] = false;
			target.AddBuff(Mod.Find<ModBuff>("Forsaken").Type, 10);
		}

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 32, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
            }
        }
    }
}

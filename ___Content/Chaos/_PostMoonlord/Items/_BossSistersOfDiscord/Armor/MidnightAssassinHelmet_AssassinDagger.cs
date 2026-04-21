using AAModClassic.___Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    public class MidnightAssassinHelmet_AssassinDagger : ModProjectile
	{
        public override string Texture => ModContent.GetInstance<AbyssalKunai>().Texture;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Assassin Dagger");
        }

        public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.ThrowingKnife);
			Projectile.width = 14;
			Projectile.height = 34;
			Projectile.friendly = true;
            Projectile.hostile = false;
			Projectile.timeLeft = 1200;
			Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
			AIType = ProjectileID.ShadowFlameKnife;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = height = 10;
			return true;
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<MidnightAssassinHelmet_AssassinHurtBuff>(), 1000);
        }

        public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Dusts.CthulhuAuraDust>(), Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f);
			}
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		}
	}
}
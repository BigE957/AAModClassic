using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class HarukaKunaiF : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.ThrowingKnife);
			Projectile.width = 14;
			Projectile.height = 34;
			Projectile.friendly = true;
            Projectile.hostile = false;
			Projectile.timeLeft = 1200;
			Projectile.penetrate = 1;
            Projectile.thrown = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
            Projectile.DamageType = DamageClass.Ranged;
			AIType = ProjectileID.ShadowFlameKnife;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			// For going through platforms and such, javelins use a tad smaller size
			width = height = 10; // notice we set the width to the height, the height to 10. so both are 10
			return true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Kunai");
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.HydraToxin>(), 180);
            Projectile.netUpdate = true;
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
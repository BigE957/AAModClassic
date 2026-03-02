using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Assassin
{
    public class AssassinDagger : ModProjectile
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
			width = height = 10;
			return true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Assassin Dagger");
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("AssassinHurt").Type, 1000);
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
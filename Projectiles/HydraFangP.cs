using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class HydraFangP : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.ThrowingKnife);
			Projectile.width = 28;
			Projectile.height = 34;
			Projectile.friendly = true;
			Projectile.timeLeft = 600;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 3;
			Projectile.friendly = true;
			AIType = ProjectileID.ThrowingKnife;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = height = 10;
			return true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hydra Fang");
		}

		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 3; k++)
			{
				int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.MagicMirror, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f);
			}
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			
			if (Main.rand.NextBool(2))
			{
				Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<HydraFang>());
			};
		}
		private const int alphaReduction = 25;
        private const float maxTicks = 35f;
	}
}
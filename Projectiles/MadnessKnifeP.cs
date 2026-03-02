using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
	public class MadnessKnifeP : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.ThrowingKnife);
			Projectile.width = 14;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.timeLeft = 600;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 2;
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
			// DisplayName.SetDefault("Madness Knife");
		}

		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 200, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f);
			}
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			
			if (Main.rand.NextBool(2))
			{
				Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("MadnessKnife").Type);
			};
		}
		private const int alphaReduction = 25;
        private const float maxTicks = 35f;
	}
}
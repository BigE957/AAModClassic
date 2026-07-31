using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.Weapons
{
    public class FrogLob_ToadGunk : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Toad Gunk");
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 32;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 600;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Ranged;
			Projectile.extraUpdates = 1;
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.penetrate = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 60;
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void PostAI()
        {
            Lighting.AddLight(Projectile.Center, Color.DodgerBlue.R / 255f, Color.DodgerBlue.G / 255f, Color.DodgerBlue.B / 255f);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact, Projectile.Center);
        }
    }
}

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TrueBlazingDawnShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LightBeam);
            Projectile.penetrate = 4;  
            Projectile.width = 42;
            Projectile.height = 42;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 900;
        }
		
		public override void AI()
		{
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                Vector2 position = Projectile.position;
                dust = Main.dust[Dust.NewDust(position, 30, 30, DustID.WaterCandle, 0f, 0f, 60, new Color(0, 242, 255), 1.381579f)];
                dust.noGravity = true;
                dust.fadeIn = 1.421053f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dawn Ray");
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 500);
        }
    }
}
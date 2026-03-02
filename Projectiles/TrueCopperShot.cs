using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TrueCopperShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LightBeam);
            Projectile.penetrate = 10;  
            Projectile.width = 30;
            Projectile.height = 30;
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
                dust = Main.dust[Dust.NewDust(position, 30, 30, 177, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.381579f)];
                dust.noGravity = true;
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                Vector2 position = Projectile.position;
                dust = Main.dust[Dust.NewDust(position, 30, 30, 177, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.381579f)];
                dust.noGravity = true;
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                Vector2 position = Projectile.position;
                dust = Main.dust[Dust.NewDust(position, 30, 30, 177, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.381579f)];
                dust.noGravity = true;
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                Vector2 position = Projectile.position;
                dust = Main.dust[Dust.NewDust(position, 30, 30, 177, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.381579f)];
                dust.noGravity = true;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 500);
        }
    }
}
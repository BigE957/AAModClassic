using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.FeudalFungus
{
    internal class Mushshot : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spore Blast");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 60;
            Projectile.timeLeft = 180;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Glow;
        }

        public override void AI()
        {
            
            for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.ShroomDust>(), 0f, 0f, 0);

                Main.dust[num190].scale *= 1.3f;
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int dust = 0; dust <= 3; dust++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ShroomDust>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}
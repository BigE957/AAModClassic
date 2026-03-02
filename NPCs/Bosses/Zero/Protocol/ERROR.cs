using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Zero.Protocol
{
    public class ERROR : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("ERR0R");
            Main.projFrames[Projectile.type] = 4;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -11;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.5f / 255f, (255 - Projectile.alpha) * 0.05f / 255f, (255 - Projectile.alpha) * 0.05f / 255f);
            Projectile.velocity *= 0.98f;
            Projectile.alpha += 2;
            if (Projectile.alpha > 255)
            {
                Projectile.Kill();
            }

            int dustId = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height + 10, ModContent.DustType<Dusts.VoidDust>(), Projectile.velocity.X * 0.2f,
					Projectile.velocity.Y * 0.2f, 100);
				Main.dust[dustId].noGravity = true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 4) 
                    Projectile.frame = 0; 
            }
            return true;
        }
    }
}
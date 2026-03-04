using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Greed
{
    public class Nebula : ModProjectile
	{
        public override string Texture => "AAMod/BlankTex";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = false; 
			Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 240;
			Projectile.alpha = 20;
			Projectile.ignoreWater = true;
            Projectile.tileCollide = true;          
		}

        public override void AI()
        {
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        Texture2D t;
        Color c;

        public void Setstuff()
        {
            if (Projectile.ai[0] == 0)
            {
                t = Mod.GetTexture("NPCs/Bosses/Greed/NebulaA");
                c = Color.HotPink;
            }
            else if (Projectile.ai[0] == 1)
            {
                t = Mod.GetTexture("NPCs/Bosses/Greed/NebulaD");
                c = Color.Blue;
            }
            else
            {
                t = Mod.GetTexture("NPCs/Bosses/Greed/NebulaH");
                c = Color.Red;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Setstuff();
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, t.Width, t.Height / 4, 0, 0);
            BaseDrawing.DrawTexture(Main.spriteBatch, t, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 4, frame, ColorUtils.COLOR_GLOWPULSE, false);
            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.Kill();
        }

        public override void OnKill(int timeleft)
        {
            Setstuff();
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<AbyssDust>(), 0f, 0f, 100, c, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<AbyssDust>(), 0f, 0f, 100, c, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }
    }
}

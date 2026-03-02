using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Bosses.Equinox
{
    public class DaybringerOrb : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Daybringer Orb");
            Main.projFrames[Projectile.type] = 4;
		}

        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.hostile = true;
            Projectile.scale = 1f;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
			Projectile.extraUpdates = 2;
			Projectile.timeLeft = 1800;
        }	
        public override void AI()
        {
            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;

            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
            
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), .98f, .96f, .67f);
            NPC npc = Main.npc[(int)Projectile.ai[1]];
            Player target = Main.player[npc.target];

            if(Projectile.timeLeft <= 0)
            {
                Projectile.Kill();
            }

            if(Projectile.ai[0] == 0)
            {
                Projectile.velocity *= 0.985f;
            }

            if(Projectile.velocity.Length() < .01f && (Projectile.localAI[0] ++ > 40))
            {
                Projectile.ai[0] = 1f;
                Projectile.velocity = Projectile.DirectionTo(target.Center + target.velocity * ((Projectile.Center - target.Center).Length() / 6f)) * 6f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SpawnDust();
            Projectile.active = false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(250, 244, 171, 200);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if(Projectile.ai[0] == 1f)
            {
                Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
                for (int k = 0; k < 3; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((3 - k) / 3f);
                    Rectangle frame = BaseDrawing.GetFrame(1, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 0);
                    BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, drawPos, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, color, true);
                }
            }
            return base.PreDraw(spriteBatch, lightColor);
        }

        public void SpawnDust()
        {
            Vector2 position = Projectile.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num86].color = new Color(250, 244, 171);
            }
            for (int num87 = 0; num87 < 7; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.Torch, 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                Main.dust[num88].color = new Color(250, 244, 171);
                num88 = Dust.NewDust(position, num84, height3, DustID.Torch, 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = new Color(250, 244, 171);
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * 8f;
            }
        }
    }
}
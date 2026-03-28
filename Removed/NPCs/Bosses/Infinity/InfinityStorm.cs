using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Removed.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Removed.NPCs.Bosses.Infinity
{
    public class InfinityStorm : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infinity Storm");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
		{

            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Projectile.localAI[1] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item121, Projectile.position);
                Projectile.localAI[1] = 1f;
            }
            if (Projectile.ai[0] < 180f)
            {
                Projectile.alpha -= 5;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
            }
            else
            {
                Projectile.alpha += 5;
                if (Projectile.alpha > 255)
                {
                    Projectile.alpha = 255;
                    Projectile.Kill();
                    return;
                }
            }
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] % 30f == 0f && Projectile.ai[0] < 180f && Main.netMode != 1)
            {
                int[] array4 = new int[5];
                Vector2[] array5 = new Vector2[5];
                int num838 = 0;
                float num839 = 2000f;
                for (int num840 = 0; num840 < 255; num840++)
                {
                    if (Main.player[num840].active && !Main.player[num840].dead)
                    {
                        Vector2 center9 = Main.player[num840].Center;
                        float num841 = Vector2.Distance(center9, Projectile.Center);
                        if (num841 < num839 && Collision.CanHit(Projectile.Center, 1, 1, center9, 1, 1))
                        {
                            array4[num838] = num840;
                            array5[num838] = center9;
                            if (++num838 >= array5.Length)
                            {
                                break;
                            }
                        }
                    }
                }
                for (int num842 = 0; num842 < num838; num842++)
                {
                    Vector2 vector82 = array5[num842] - Projectile.Center;
                    float ai = (float)Main.rand.Next(100);
                    Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 7f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector83.X, vector83.Y, ModContent.ProjectileType<InfinityBolt>(), Projectile.damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                }
            }
            Lighting.AddLight(Projectile.Center, 0.4f, 0.85f, 0.9f);
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
            if (Projectile.alpha < 150 && Projectile.ai[0] < 180f)
            {
                for (int num843 = 0; num843 < 1; num843++)
                {
                    float num844 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num844 < -0.5f)
                    {
                        num844 = -0.5f;
                    }
                    if (num844 > 0.5f)
                    {
                        num844 = 0.5f;
                    }
                    Vector2 value47 = new Vector2((float)(-(float)Projectile.width) * 0.2f * Projectile.scale, 0f).RotatedBy((double)(num844 * 6.28318548f), default(Vector2)).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                    int num845 = Dust.NewDust(Projectile.Center - Vector2.One * 5f, 10, 10, ModContent.DustType<VoidDustRemoved>(), -Projectile.velocity.X / 3f, -Projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num845].position = Projectile.Center + value47;
                    Main.dust[num845].velocity = Vector2.Normalize(Main.dust[num845].position - Projectile.Center) * 2f;
                    Main.dust[num845].noGravity = true;
                }
                for (int num846 = 0; num846 < 1; num846++)
                {
                    float num847 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num847 < -0.5f)
                    {
                        num847 = -0.5f;
                    }
                    if (num847 > 0.5f)
                    {
                        num847 = 0.5f;
                    }
                    Vector2 value48 = new Vector2((float)(-(float)Projectile.width) * 0.6f * Projectile.scale, 0f).RotatedBy((double)(num847 * 6.28318548f), default(Vector2)).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                    int num848 = Dust.NewDust(Projectile.Center - Vector2.One * 5f, 10, 10, ModContent.DustType<VoidDustRemoved>(), -Projectile.velocity.X / 3f, -Projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num848].velocity = Vector2.Zero;
                    Main.dust[num848].position = Projectile.Center + value48;
                    Main.dust[num848].noGravity = true;
                }
                return;
            }
        }
        public static int frameWidth = 100, frameHeight = 100;
        public int frameTimer = 0;
        public int frameCount = 3;
        public bool invertFrame = false;
        public Rectangle frame;
        public static Texture2D tex = null;
        public static Texture2D glowTex = null;
        public bool checkedMinPos = false;

        public override void PostAI()
        {
            Projectile.rotation = Projectile.velocity.X * 0.1f;
            frameTimer--;
            if (frameTimer <= 0)
            {
                frameTimer = 2;
                if (invertFrame) { frameCount--; if (frameCount < 0) { frameCount = 1; invertFrame = false; } }
                else
                { frameCount++; if (frameCount > 2) { frameCount = 1; invertFrame = true; } }
            }
            frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (tex == null)
            {
                tex = TextureAssets.Projectile[Projectile.type].Value;
                glowTex =  Mod.GetTexture("Removed/NPCs/Bosses/Infinity/InfinityStorm_Glow");
            }
            Color lightColour = BaseDrawing.GetLightColor(Projectile.Center);
            for (int m = Projectile.oldPos.Length - 1; m > 0; m--) { Projectile.oldPos[m] = Projectile.oldPos[m - 1]; }
            Projectile.oldPos[0] = Projectile.position;
            BaseDrawing.DrawTexture(Main.spriteBatch, tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.spriteDirection, 3, frame, lightColour);
            BaseDrawing.DrawTexture(Main.spriteBatch, glowTex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.spriteDirection, 3, frame, AAColor.Oblivion);

            return false;
        }
    }
}

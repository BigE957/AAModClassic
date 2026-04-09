using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.NPCs.Bosses.Akuma.Awakened
{
    [AutoloadBossHead]
    public class AsheA : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ashe Akuma");
            Main.projFrames[Projectile.type] = 12;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.timeLeft = 600;
            Projectile.aiStyle = -1;
            CooldownSlot = 1;
            Projectile.hide = true;
        }

        public override void AI()
        {
            Projectile.hide = false;

            Frames(); 
            
            if (Projectile.velocity.X < 0)
            {
                Projectile.direction = -1;
                Projectile.spriteDirection = -1;
            }
            else
            {
                Projectile.direction = 1;
                Projectile.spriteDirection = 1;
            }

            Player player = Main.player[(int)Projectile.ai[0]];

            Projectile.Center = player.Center;
            Projectile.position.Y -= 400;
            Projectile.position.X += 400 * (float)Math.Sin(2 * Math.PI / 180 * Projectile.ai[1]++);

            if (++Projectile.localAI[0] == 52)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.UnitY * 4, ModContent.ProjectileType<AkumaRock>(), Projectile.damage, 0, Main.myPlayer);
            }

            if (Projectile.localAI[1] == 0)
            {
                Projectile.localAI[1] = 1; 
                int pieCut = 20;
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                for (int m = 0; m < pieCut; m++)
                {
                    int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, Color.White, 1.6f);
                    Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                    Main.dust[dustID].noLight = false;
                    Main.dust[dustID].noGravity = true;
                }
                for (int m = 0; m < pieCut; m++)
                {
                    int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, Color.White, 2f);
                    Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                    Main.dust[dustID].noLight = false;
                    Main.dust[dustID].noGravity = true;
                }
            }

            if (!NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
            {
                Projectile.Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            Vector2 position = Projectile.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Granite, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = Projectile.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += Projectile.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = Projectile.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += Projectile.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {

            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 12, 0, 0);

            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.spriteDirection, 12, frame, lightColor, true);
            return false;
        }

        public void Frames()
        {
            if (Projectile.localAI[0] > 40)
            {
                if (Projectile.localAI[0] < 43)
                {
                    Projectile.frame = 4;
                }
                else if (Projectile.localAI[0] < 46)
                {
                    Projectile.frame = 5;
                }
                else if (Projectile.localAI[0] < 49)
                {
                    Projectile.frame = 6;
                }
                else if (Projectile.localAI[0] < 52)
                {
                    Projectile.frame = 7;
                }
                else if (Projectile.localAI[0] < 55)
                {
                    Projectile.frame = 8;
                }
                else if (Projectile.localAI[0] < 58)
                {
                    Projectile.frame = 9;
                }
                else if (Projectile.localAI[0] < 61)
                {
                    Projectile.frame = 10;
                }
                else if (Projectile.localAI[0] < 64)
                {
                    Projectile.frame = 11;
                }
                else
                {
                    Projectile.localAI[0] = 0;
                }
            }
            else
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
        }
    }
}



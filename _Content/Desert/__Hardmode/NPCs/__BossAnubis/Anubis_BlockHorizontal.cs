using AAModClassic.Base.BaseMod.Base;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.NPCs.__BossAnubis
{
    public class Anubis_BlockHorizontal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 208;
            Projectile.height = 64;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
        }

        public float[] internalAI = new float[1];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadSingle();
            }
        }

        public override void AI()
        {
            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;
            if (Projectile.frame < 5)
            {
                if (Projectile.frameCounter++ > 3)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                }
            }
            else
            {
                Projectile.frame = 5;
            }

            float maxVelocity = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) ? 14 : 12;
            float accel = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) ? 0.14f : 0.05f;
            int delay = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) ? 45 : 90;

            if (internalAI[0]++ > delay)
            {
                if (Projectile.ai[0] == 0)
                {
                    if (Projectile.velocity.Y < maxVelocity)
                    {
                        Projectile.velocity.Y += accel;
                    }
                }
                else if (Projectile.ai[0] == 1)
                {
                    if (Projectile.velocity.Y > -maxVelocity)
                    {
                        Projectile.velocity.Y -= accel;
                    }
                    Projectile.direction = Projectile.spriteDirection = -1;
                }

                Projectile clearCheck = Main.projectile[(int)Projectile.ai[1]];
                if (Collision.CheckAABBvAABBCollision(Projectile.position, Projectile.Size, clearCheck.position, clearCheck.Size))
                {
                    for (int m = 0; m < 40; m++)
                    {
                        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sand, 0f, 0f, 100, default, 1.6f);
                    }
                    clearCheck.Kill();
                    Projectile.Kill();
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int m = 0; m < 40; m++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sand, 0f, 0f, 100, default, 1.6f);
            }
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 6, 0, 0);

            BaseDrawing.DrawAfterimage(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, 2f, 1f, Math.Abs((int)Projectile.velocity.Y), true, 0f, 0f, lightColor, frame, 6);

            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 6, frame, lightColor, true);
            return false;
        }
    }
}
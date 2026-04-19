using Microsoft.Xna.Framework;
using System.IO;
using Terraria;

using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Projectiles.Akuma
{
    public class Sunstorm_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sunstorm");

            Main.projFrames[Projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 5;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12;
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
            if (Projectile.ai[1] != -1f && Projectile.position.Y > Projectile.ai[1])
            {
                Projectile.tileCollide = true;
            }
            if (Projectile.position.HasNaNs())
            {
                Projectile.Kill();
                return;
            }
            bool flag5 = WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.position.X / 16, (int)Projectile.position.Y / 16));
            Dust dust19 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
            dust19.position = Projectile.Center;
            dust19.velocity = Vector2.Zero;
            dust19.noGravity = true;
            Dust dust18 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
            dust18.position = Projectile.Center;
            dust18.velocity = Vector2.Zero;
            dust18.noGravity = true;
            if (flag5)
            {
                dust19.noLight = true;
                dust18.noLight = true;
            }
            if (Projectile.ai[1] == -1f)
            {
                Projectile.ai[0] += 1f;
                Projectile.velocity = Vector2.Zero;
                Projectile.tileCollide = false;
                Projectile.penetrate = -1;
                Projectile.position = Projectile.Center;
                Projectile.width = Projectile.height = 140;
                Projectile.Center = Projectile.position;
                Projectile.alpha -= 10;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
                if (++Projectile.frameCounter >= Projectile.MaxUpdates * 3)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                }
                if (Projectile.frame == 6)
                {
                    Projectile.Kill();
                }
                return;
            }
            Projectile.alpha = 255;
            internalAI[0]++;
            if (internalAI[0] > 20)
            {
                internalAI[0] = 20; 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = Projectile.DirectionTo(n.Center) * 30;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / 30);
                }
            }
            if (Projectile.numUpdates == 0)
            {
                int num185 = -1;
                float num186 = 60f;
                for (int num187 = 0; num187 < 200; num187++)
                {
                    NPC nPC2 = Main.npc[num187];
                    if (nPC2.CanBeChasedBy(this, false))
                    {
                        float num188 = Projectile.Distance(nPC2.Center);
                        if (num188 < num186 && Collision.CanHitLine(Projectile.Center, 0, 0, nPC2.Center, 0, 0))
                        {
                            num186 = num188;
                            num185 = num187;
                        }
                    }
                }
                if (num185 != -1)
                {
                    Projectile.ai[0] = 0f;
                    Projectile.ai[1] = -1f;
                    Projectile.netUpdate = true;
                    return;
                }
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(Projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = Projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            Projectile.Distance(Main.npc[selectedTarget].Center) > distance) 
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] = 0f;
            Projectile.ai[1] = -1f;
            Projectile.netUpdate = true;
        }

        public override void OnKill(int timeLeft)
        {
            bool flag = WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.position.X / 16, (int)Projectile.position.Y / 16));

            for (int num58 = 0; num58 < 4; num58++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, default, 1.5f);
            }
            for (int num59 = 0; num59 < 4; num59++)
            {
                int num60 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 2.5f);
                Main.dust[num60].noGravity = true;
                Main.dust[num60].velocity *= 3f;
                if (flag)
                {
                    Main.dust[num60].noLight = true;
                }
                num60 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num60].velocity *= 2f;
                Main.dust[num60].noGravity = true;
                if (flag)
                {
                    Main.dust[num60].noLight = true;
                }
            }
        }
    }
}
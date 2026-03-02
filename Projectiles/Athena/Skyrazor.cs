using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Athena
{
    public class Skyrazor : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 360;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            Projectile.localAI[1] += 1f;
            if (Projectile.localAI[1] > 10f && Main.rand.Next(3) == 0)
            {
                for (int num694 = 0; num694 < 3; num694++)
                {
                    Vector2 vector56 = Vector2.Normalize(Projectile.velocity) * new Vector2(Projectile.width, Projectile.height) / 2f;
                    vector56 = vector56.RotatedBy((num694 - (3 / 2 - 1)) * 3.1415926535897931 / 3f, default) + Projectile.Center;
                    Vector2 value24 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);
                    int num695 = Dust.NewDust(vector56 + value24, 0, 0, DustID.FishronWings, value24.X * 2f, value24.Y * 2f, DustID.Electric, default, 1.4f);
                    Main.dust[num695].noLight = true;
                    Main.dust[num695].velocity /= 4f;
                    Main.dust[num695].velocity -= Projectile.velocity;
                }
                Projectile.alpha -= 5;
                if (Projectile.alpha < 50)
                {
                    Projectile.alpha = 50;
                }
                Projectile.rotation += Projectile.velocity.X * 0.1f;
                Projectile.frame = (int)(Projectile.localAI[1] / 3f) % 3;
                Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, 0.1f, 0.4f, 0.6f);
            }
            int num696 = -1;
            Vector2 vector57 = Projectile.Center;
            float num697 = 500f;
            if (Projectile.localAI[0] > 0f)
            {
                Projectile.localAI[0] -= 1f;
            }
            if (Projectile.ai[0] == 0f && Projectile.localAI[0] == 0f)
            {
                for (int num698 = 0; num698 < 200; num698++)
                {
                    NPC nPC6 = Main.npc[num698];
                    if (nPC6.CanBeChasedBy(this, false) && (Projectile.ai[0] == 0f || Projectile.ai[0] == num698 + 1))
                    {
                        Vector2 center4 = nPC6.Center;
                        float num699 = Vector2.Distance(center4, vector57);
                        if (num699 < num697 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC6.position, nPC6.width, nPC6.height))
                        {
                            num697 = num699;
                            vector57 = center4;
                            num696 = num698;
                        }
                    }
                }
                if (num696 >= 0)
                {
                    Projectile.ai[0] = num696 + 1;
                    Projectile.netUpdate = true;
                }
            }
            if (Projectile.localAI[0] == 0f && Projectile.ai[0] == 0f)
            {
                Projectile.localAI[0] = 30f;
            }
            bool flag31 = false;
            if (Projectile.ai[0] != 0f)
            {
                int num700 = (int)(Projectile.ai[0] - 1f);
                if (Main.npc[num700].active && !Main.npc[num700].dontTakeDamage && Main.npc[num700].immune[Projectile.owner] == 0)
                {
                    float num701 = Main.npc[num700].position.X + Main.npc[num700].width / 2;
                    float num702 = Main.npc[num700].position.Y + Main.npc[num700].height / 2;
                    float num703 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num701) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num702);
                    if (num703 < 1000f)
                    {
                        flag31 = true;
                        vector57 = Main.npc[num700].Center;
                    }
                }
                else
                {
                    Projectile.ai[0] = 0f;
                    flag31 = false;
                    Projectile.netUpdate = true;
                }
            }
            if (flag31)
            {
                Vector2 v = vector57 - Projectile.Center;
                float num704 = Projectile.velocity.ToRotation();
                float num705 = v.ToRotation();
                double num706 = num705 - num704;
                if (num706 > 3.1415926535897931)
                {
                    num706 -= 6.2831853071795862;
                }
                if (num706 < -3.1415926535897931)
                {
                    num706 += 6.2831853071795862;
                }
                Projectile.velocity = Projectile.velocity.RotatedBy(num706 * 0.10000000149011612, default);
            }
            float num707 = Projectile.velocity.Length();
            Projectile.velocity.Normalize();
            Projectile.velocity *= num707 + 0.0025f; 
            
            for (int u = 0; u < Main.maxNPCs; u++)
            {
                NPC target = Main.npc[u];

                if (target.type != NPCID.TargetDummy && target.active && !target.boss && target.chaseable && target.chaseable && Vector2.Distance(Projectile.Center, target.Center) < 200)
                {
                    float num3 = 6f;
                    Vector2 vector = new Vector2(target.position.X + target.width / 2, target.position.Y + target.height / 2);
                    float num4 = Projectile.Bottom.X - vector.X;
                    float num5 = Projectile.Bottom.Y - vector.Y;
                    float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                    num6 = num3 / num6;
                    num4 *= num6;
                    num5 *= num6;
                    int num7 = 6;
                    target.velocity.X = (target.velocity.X * (num7 - 1) + num4) / num7;
                    target.velocity.Y = (target.velocity.Y * (num7 - 1) + num5) / num7;
                    target.velocity *= target.knockBackResist;
                }
            }
            return;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 6;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = oldVelocity.X * -1f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = oldVelocity.Y * -1f;
            }
            return false;
        }
    }
}
using AAModClassic.___Content._PLACEHOLDER.ore.projs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Akuma
{
    public class DaystormbulletA : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Daystormbullet");
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.scale = 2f;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 500;

        }

        public override void AI()
        {
            if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(-90f);
            }
            if(Projectile.alpha < 170)
            {
                for (int num165 = 0; num165 < 2; num165 ++)
                {
                    float x2 = Projectile.position.X + Projectile.width / 2 - Projectile.velocity.X / 2f * num165;
                    float y2 = Projectile.position.Y + Projectile.height / 2 - Projectile.velocity.Y / 2f * num165;
                    int num166 = Dust.NewDust(new Vector2(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2), Projectile.width, Projectile.height + 5, ModContent.DustType<Dusts.AkumaADust>(), Projectile.velocity.X * 0.2f,
                        Projectile.velocity.Y * 0.2f, 0, default, 2f);
                    Main.dust[num166].alpha = Projectile.alpha;
                    Main.dust[num166].position.X = x2;
                    Main.dust[num166].position.Y = y2;
                    Main.dust[num166].velocity *= 0f;
                    Main.dust[num166].noGravity = true;
                }
            }
            float num167 = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
            float num168 = Projectile.localAI[0];
            if (num168 == 0f)
            {
                Projectile.localAI[0] = num167;
                num168 = num167;
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 25;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            float num169 = Projectile.position.X;
            float num170 = Projectile.position.Y;
            float num171 = 300f;
            bool flag4 = false;
            int num172 = 0;
            if (Projectile.ai[1] == 0f)
            {
                int num;
                for (int num173 = 0; num173 < 200; num173 = num + 1)
                {
                    if (Main.npc[num173].CanBeChasedBy(Projectile, false) && (Projectile.ai[1] == 0f || Projectile.ai[1] == num173 + 1))
                    {
                        float num174 = Main.npc[num173].position.X + Main.npc[num173].width / 2;
                        float num175 = Main.npc[num173].position.Y + Main.npc[num173].height / 2;
                        float num176 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num174) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num175);
                        if (num176 < num171 && Collision.CanHit(new Vector2(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2), 1, 1, Main.npc[num173].position, Main.npc[num173].width, Main.npc[num173].height))
                        {
                            num171 = num176;
                            num169 = num174;
                            num170 = num175;
                            flag4 = true;
                            num172 = num173;
                        }
                    }
                    num = num173;
                }
                if (flag4)
                {
                    Projectile.ai[1] = num172 + 1;
                }
                flag4 = false;
            }
            if (Projectile.ai[1] > 0f)
            {
                int num177 = (int)(Projectile.ai[1] - 1f);
                if (Main.npc[num177].active && Main.npc[num177].CanBeChasedBy(Projectile, true) && !Main.npc[num177].dontTakeDamage)
                {
                    float num178 = Main.npc[num177].position.X + Main.npc[num177].width / 2;
                    float num179 = Main.npc[num177].position.Y + Main.npc[num177].height / 2;
                    float num180 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num178) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num179);
                    if (num180 < 1000f)
                    {
                        flag4 = true;
                        num169 = Main.npc[num177].position.X + Main.npc[num177].width / 2;
                        num170 = Main.npc[num177].position.Y + Main.npc[num177].height / 2;
                    }
                }
                else
                {
                    Projectile.ai[1] = 0f;
                }
            }
            if (!Projectile.friendly)
            {
                flag4 = false;
            }
            if (flag4)
            {
                float num181 = num168;
                Vector2 vector19 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                float num182 = num169 - vector19.X;
                float num183 = num170 - vector19.Y;
                float num184 = (float)Math.Sqrt(num182 * num182 + num183 * num183);
                num184 = num181 / num184;
                num182 *= num184;
                num183 *= num184;
                int num185 = 8;
                Projectile.velocity.X = (Projectile.velocity.X * (num185 - 1) + num182) / num185;
                Projectile.velocity.Y = (Projectile.velocity.Y * (num185 - 1) + num183) / num185;
            }
        }
        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.InfinityOverloadB>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.InfinityOverloadB>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188));
                Main.dust[num469].velocity *= 2f;
            }
            int id = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaybreakBlast>(), Projectile.damage, Projectile.knockBack * 3, Main.myPlayer, 0, 0);
            Main.projectile[id].DamageType = DamageClass.Magic;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 800);
        }
    }
}

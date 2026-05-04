using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRaiderUltima
{
    public class RaiderUltima_RaiderRocket : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }

        public bool PlayerHit = false;
        public bool TileHit = false;

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 30;
            NPC.damage = 60;
            NPC.defense = 16;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit42;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.value = 0f;
            NPC.noTileCollide = false;
            NPC.noGravity = true;
            NPC.npcSlots = 1.5f;
            NPC.canGhostHeal = false;
            NPC.aiStyle = -1;
        }

        public override void AI()
        {
            NPC.TargetClosest(false);
            NPC.rotation = NPC.velocity.ToRotation();
            if (Math.Sign(NPC.velocity.X) != 0)
            {
                NPC.spriteDirection = -Math.Sign(NPC.velocity.X);
            }
            if (NPC.rotation < -1.57079637f)
            {
                NPC.rotation += 3.14159274f;
            }
            if (NPC.rotation > 1.57079637f)
            {
                NPC.rotation -= 3.14159274f;
            }
            float num997 = 0.4f;
            float num998 = 10f;
            float scaleFactor3 = 200f;
            float num999 = 750f;
            float num1000 = 30f;
            float num1001 = 30f;
            float scaleFactor4 = 0.95f;
            int num1002 = 50;
            float scaleFactor5 = 14f;
            float num1003 = 30f;
            float num1004 = 100f;
            float num1005 = 20f;
            float num1006 = 0f;
            float num1007 = 7f;
            bool flag63 = true;
            num1006 *= num1005;
            if (Main.expertMode)
            {
                num997 *= Main.GameModeInfo.KnockbackToEnemiesMultiplier;
            }
            if (NPC.collideX || NPC.collideY)
            {
                TileHit = true;
                NPC.life = 0;
            }
            if (NPC.ai[0] != 3f)
            {
                int num1008 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num1008].noGravity = true;
                Main.dust[num1008].velocity = NPC.velocity / 5f;
                Vector2 vector125 = new Vector2(-10f, 10f);
                if (NPC.spriteDirection == 1)
                {
                    vector125.X *= -1f;
                }
                vector125 = vector125.RotatedBy((double)NPC.rotation, default(Vector2));
                Main.dust[num1008].position = NPC.Center + vector125;
            }
            if (NPC.ai[0] == 0f)
            {
                NPC.knockBackResist = num997;
                float scaleFactor6 = num998;
                Vector2 center4 = NPC.Center;
                Vector2 center5 = Main.player[NPC.target].Center;
                Vector2 vector126 = center5 - center4;
                Vector2 vector127 = vector126 - Vector2.UnitY * scaleFactor3;
                float num1013 = vector126.Length();
                vector126 = Vector2.Normalize(vector126) * scaleFactor6;
                vector127 = Vector2.Normalize(vector127) * scaleFactor6;
                bool flag64 = Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1);
                if (NPC.ai[3] >= 120f)
                {
                    flag64 = true;
                }
                float num1014 = 8f;
                flag64 = (flag64 && vector126.ToRotation() > 3.14159274f / num1014 && vector126.ToRotation() < 3.14159274f - 3.14159274f / num1014);
                if (num1013 > num999 || !flag64)
                {
                    NPC.velocity.X = (NPC.velocity.X * (num1000 - 1f) + vector127.X) / num1000;
                    NPC.velocity.Y = (NPC.velocity.Y * (num1000 - 1f) + vector127.Y) / num1000;
                    if (!flag64)
                    {
                        NPC.ai[3] += 1f;
                        if (NPC.ai[3] == 120f)
                        {
                            NPC.netUpdate = true;
                        }
                    }
                    else
                    {
                        NPC.ai[3] = 0f;
                    }
                }
                else
                {
                    NPC.ai[0] = 1f;
                    NPC.ai[2] = vector126.X;
                    NPC.ai[3] = vector126.Y;
                    NPC.netUpdate = true;
                }
            }
            else if (NPC.ai[0] == 1f)
            {
                NPC.knockBackResist = 0f;
                NPC.velocity *= scaleFactor4;
                NPC.ai[1] += 1f;
                if (NPC.ai[1] >= num1001)
                {
                    NPC.ai[0] = 2f;
                    NPC.ai[1] = 0f;
                    NPC.netUpdate = true;
                    Vector2 velocity = new Vector2(NPC.ai[2], NPC.ai[3]) + new Vector2((float)Main.rand.Next(-num1002, num1002 + 1), (float)Main.rand.Next(-num1002, num1002 + 1)) * 0.04f;
                    velocity.Normalize();
                    velocity *= scaleFactor5;
                    NPC.velocity = velocity;
                }
                if (Main.rand.Next(4) == 0)
                {
                    int num1015 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[num1015].noGravity = true;
                    Main.dust[num1015].velocity *= 2f;
                    Main.dust[num1015].velocity = Main.dust[num1015].velocity / 2f + Vector2.Normalize(Main.dust[num1015].position - NPC.Center);
                }
            }
            else if (NPC.ai[0] == 2f)
            {
                NPC.knockBackResist = 0f;
                float num1016 = num1003;
                NPC.ai[1] += 1f;
                bool flag65 = Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) > num1004 && NPC.Center.Y > Main.player[NPC.target].Center.Y;
                if ((NPC.ai[1] >= num1016 && flag65) || NPC.velocity.Length() < num1007)
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.velocity /= 2f;
                    NPC.netUpdate = true;
                }
                else
                {
                    Vector2 center6 = NPC.Center;
                    Vector2 center7 = Main.player[NPC.target].Center;
                    Vector2 vec2 = center7 - center6;
                    vec2.Normalize();
                    if (vec2.HasNaNs())
                    {
                        vec2 = new Vector2((float)NPC.direction, 0f);
                    }
                    NPC.velocity = (NPC.velocity * (num1005 - 1f) + vec2 * (NPC.velocity.Length() + num1006)) / num1005;
                }
                if (flag63 && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.ai[0] = 3f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                }
            }
            else if (NPC.ai[0] == 4f)
            {
                NPC.ai[1] -= 3f;
                if (NPC.ai[1] <= 0f)
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                    NPC.netUpdate = true;
                }
                NPC.velocity *= 0.95f;
            }
            if (flag63 && NPC.ai[0] != 3f && Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) < 64f)
            {
                NPC.ai[0] = 3f;
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
                NPC.ai[3] = 0f;
                NPC.netUpdate = true;
            }
            if (NPC.ai[0] == 3f)
            {
                NPC.position = NPC.Center;
                NPC.width = (NPC.height = 192);
                NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
                NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
                NPC.velocity = Vector2.Zero;
                NPC.damage = (int)(80f * Main.GameModeInfo.EnemyDamageMultiplier);
                NPC.alpha = 255;
                Lighting.AddLight((int)NPC.Center.X / 16, (int)NPC.Center.Y / 16, 0.2f, 0.7f, 1.1f);
                for (int num1017 = 0; num1017 < 10; num1017++)
                {
                    int num1018 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num1018].velocity *= 1.4f;
                    Main.dust[num1018].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
                }
                for (int num1019 = 0; num1019 < 40; num1019++)
                {
                    int num1020 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[num1020].noGravity = true;
                    Main.dust[num1020].velocity *= 2f;
                    Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
                    Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - NPC.Center);
                    if (Main.rand.Next(2) == 0)
                    {
                        num1020 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.9f);
                        Main.dust[num1020].noGravity = true;
                        Main.dust[num1020].velocity *= 1.2f;
                        Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
                        Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - NPC.Center);
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        num1020 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.7f);
                        Main.dust[num1020].velocity *= 1.2f;
                        Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
                        Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - NPC.Center);
                    }
                }
                NPC.ai[1] += 1f;
                if (NPC.ai[1] >= 3f)
                {
                    SoundEngine.PlaySound(SoundID.Item14, NPC.position);
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    return;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int num236 = 0;
            while ((double)num236 < hit.Damage / (double)(NPC.lifeMax * 50))
            {
                int num237 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.FulguriteDust>(), (float)(-1 * hit.HitDirection), -1f, 0, default(Color), 1f);
                Main.dust[num237].position = Vector2.Lerp(Main.dust[num237].position, NPC.Center, 0.25f);
                Main.dust[num237].scale = 0.5f;
                num236++;
            }
            PlayerHit = true;
            NPC.life = 0;

            if (NPC.life <= 0)
            {
                if (TileHit)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y + 20, NPC.velocity.X, NPC.velocity.Y, ModContent.ProjectileType<RaiderUltima_RocketStrike>(), NPC.damage / 4, 1, 255);
                }
                if (PlayerHit)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y + 20, NPC.velocity.X, NPC.velocity.Y, ModContent.ProjectileType<RaiderUltima_RaiderExplosion>(), NPC.damage / 4, 1, 255);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 16;
                if (NPC.frame.Y > (16 * 3))
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }
    }
}
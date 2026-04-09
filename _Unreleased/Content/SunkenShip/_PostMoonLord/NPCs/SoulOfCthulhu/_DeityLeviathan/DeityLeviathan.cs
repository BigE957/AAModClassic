using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using AAModClassic.Music;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityLeviathan
{
    [AutoloadBossHead]
    public class DeityLeviathan : ModNPC
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vile-Oct");
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            NPC.width = 150;
            NPC.height = 100;
            NPC.aiStyle = -1;
            NPC.damage = 130;
            NPC.defense = 110;
            NPC.lifeMax = 150000;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.npcSlots = 10f;
            NPC.HitSound = SoundID.NPCHit14;
            NPC.DeathSound = SoundID.NPCDeath20;
            NPC.boss = true;
            NPC.netAlways = true;
            NPC.timeLeft = NPC.activeTime * 30;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[31] = true;
            NPC.buffImmune[44] = true;
            Music = MusicManagementSystem.MusicSlots["SoC"];
            for (int m = 0; m < NPC.buffImmune.Length; m++) NPC.buffImmune[m] = true;
        }

        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (!AAConfigClient.Instance.DisableAnticheat)
            {
                if (modifiers.GetDamage(item.damage, true) > NPC.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    modifiers.TargetDamageMultiplier *= 0;
                }
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (!AAConfigClient.Instance.DisableAnticheat)
            {
                if (modifiers.GetDamage(projectile.damage, true) > NPC.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    modifiers.TargetDamageMultiplier *= 0;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                SoulOfCthulhu.ComeBack = true;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            int num = TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type];
            if (NPC.ai[0] == 0f || NPC.ai[0] == 5f)
            {
                int num112 = 5;
                if (NPC.ai[0] == 5f)
                {
                    num112 = 4;
                }
                NPC.frameCounter += 1.0;
                if (NPC.frameCounter > num112)
                {
                    NPC.frameCounter = 0.0;
                    NPC.frame.Y = NPC.frame.Y + num;
                }
                if (NPC.frame.Y >= num * 6)
                {
                    NPC.frame.Y = 0;
                }
            }
            if (NPC.ai[0] == 1f || NPC.ai[0] == 6f)
            {
                if (NPC.ai[2] < 10f)
                {
                    NPC.frame.Y = num * 6;
                }
                else
                {
                    NPC.frame.Y = num * 7;
                }
            }
            if (NPC.ai[0] == 2f || NPC.ai[0] == 7f)
            {
                if (NPC.ai[2] < 10f)
                {
                    NPC.frame.Y = num * 6;
                }
                else
                {
                    NPC.frame.Y = num * 7;
                }
            }
            if (NPC.ai[0] == 3f || NPC.ai[0] == 8f || NPC.ai[0] == -1f)
            {
                int num113 = 90;
                if (NPC.ai[2] < num113 - 30 || NPC.ai[2] > num113 - 10)
                {
                    NPC.frameCounter += 1.0;
                    if (NPC.frameCounter > 5.0)
                    {
                        NPC.frameCounter = 0.0;
                        NPC.frame.Y = NPC.frame.Y + num;
                    }
                    if (NPC.frame.Y >= num * 6)
                    {
                        NPC.frame.Y = 0;
                    }
                }
                else
                {
                    NPC.frame.Y = num * 6;
                    if (NPC.ai[2] > num113 - 20 && NPC.ai[2] < num113 - 15)
                    {
                        NPC.frame.Y = num * 7;
                    }
                }
            }
            if (NPC.ai[0] == 4f || NPC.ai[0] == 9f)
            {
                int num114 = 180;
                if (NPC.ai[2] < num114 - 60 || NPC.ai[2] > num114 - 20)
                {
                    NPC.frameCounter += 1.0;
                    if (NPC.frameCounter > 5.0)
                    {
                        NPC.frameCounter = 0.0;
                        NPC.frame.Y = NPC.frame.Y + num;
                    }
                    if (NPC.frame.Y >= num * 6)
                    {
                        NPC.frame.Y = 0;
                    }
                }
                else
                {
                    NPC.frame.Y = num * 6;
                    if (NPC.ai[2] > num114 - 50 && NPC.ai[2] < num114 - 25)
                    {
                        NPC.frame.Y = num * 7;
                    }
                }
            }
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = 0;   //boss drops
        }

        public override void AI()
        {
            bool expertMode = Main.expertMode;
            float expertDamage = expertMode ? 0.6f * Main.GameModeInfo.EnemyDamageMultiplier : 1f;
            bool Phase2Check = NPC.life <= NPC.lifeMax * 0.5;
            bool ExpertPhaseCheck = expertMode && NPC.life <= NPC.lifeMax * 0.15;
            bool Phase2Change = NPC.ai[0] > 4f;
            bool ExpertPhaseChange = NPC.ai[0] > 9f;
            bool isCharging = NPC.ai[3] < 10f;
            if (ExpertPhaseChange)
            {
                NPC.damage = (int)(NPC.defDamage * 1.1f * expertDamage);
                NPC.defense = 0;
            }
            else if (Phase2Change)
            {
                NPC.damage = (int)(NPC.defDamage * 1.2f * expertDamage);
                NPC.defense = (int)(NPC.defDefense * 0.8f);
            }
            else
            {
                NPC.damage = NPC.defDamage;
                NPC.defense = NPC.defDefense;
            }
            int aiChangeRate = expertMode ? 40 : 60;
            float npcVelocity = expertMode ? 0.55f : 0.45f;
            float scaleFactor = expertMode ? 8.5f : 7.5f;
            if (ExpertPhaseChange)
            {
                npcVelocity = 0.7f;
                scaleFactor = 12f;
                aiChangeRate = 30;
            }
            else if (Phase2Change && isCharging)
            {
                npcVelocity = expertMode ? 0.6f : 0.5f;
                scaleFactor = expertMode ? 10f : 8f;
                aiChangeRate = expertMode ? 40 : 20;
            }
            else if (isCharging && !Phase2Change && !ExpertPhaseChange)
            {
                aiChangeRate = 30;
            }
            int ChargeTime = expertMode ? 28 : 30;
            float ChargeSpeed = expertMode ? 17f : 16f;
            if (ExpertPhaseChange)
            {
                ChargeTime = 25;
                ChargeSpeed = 27f;
            }
            else if (isCharging && Phase2Change)
            {
                ChargeTime = expertMode ? 27 : 30;
                if (expertMode)
                {
                    ChargeSpeed = 21f;
                }
            }
            int num6 = 80;
            int num7 = 4;
            float num8 = 0.3f;
            float scaleFactor2 = 5f;
            int num9 = 90;
            int num10 = 180;
            int num11 = 180;
            int num12 = 30;
            int num13 = 120;
            int num14 = 4;
            float scaleFactor3 = 6f;
            float scaleFactor4 = 20f;
            float num15 = 6.28318548f / (num13 / 2);
            int num16 = 75;
            Vector2 vector = NPC.Center;
            Player player = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
            {
                NPC.TargetClosest(true);
                player = Main.player[NPC.target];
                NPC.netUpdate = true;
            }
            if (player.dead || Vector2.Distance(player.Center, vector) > 5600f)
            {
                NPC.velocity.Y = NPC.velocity.Y - 0.4f;
                if (NPC.timeLeft > 10)
                {
                    NPC.timeLeft = 10;
                }
                if (NPC.ai[0] > 4f)
                {
                    NPC.ai[0] = 5f;
                }
                else
                {
                    NPC.ai[0] = 0f;
                }
                NPC.ai[2] = 0f;
            }
            bool flag6 = player.position.Y < 800f || player.position.Y > Main.worldSurface * 16.0 || player.position.X > 6400f && player.position.X < Main.maxTilesX * 16 - 6400;
            if (flag6)
            {
                aiChangeRate = 20;
                NPC.damage = NPC.defDamage * 2;
                NPC.defense = NPC.defDefense * 2;
                NPC.ai[3] = 0f;
                ChargeSpeed += 6f;
            }
            if (NPC.localAI[0] == 0f)
            {
                NPC.localAI[0] = 1f;
                NPC.alpha = 255;
                NPC.rotation = 0f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[0] = -1f;
                    NPC.netUpdate = true;
                }
            }
            float num17 = (float)Math.Atan2((double)(player.Center.Y - vector.Y), (double)(player.Center.X - vector.X));
            if (NPC.spriteDirection == 1)
            {
                num17 += 3.14159274f;
            }
            if (num17 < 0f)
            {
                num17 += 6.28318548f;
            }
            if (num17 > 6.28318548f)
            {
                num17 -= 6.28318548f;
            }
            if (NPC.ai[0] == -1f)
            {
                num17 = 0f;
            }
            if (NPC.ai[0] == 3f)
            {
                num17 = 0f;
            }
            if (NPC.ai[0] == 4f)
            {
                num17 = 0f;
            }
            if (NPC.ai[0] == 8f)
            {
                num17 = 0f;
            }
            float num18 = 0.04f;
            if (NPC.ai[0] == 1f || NPC.ai[0] == 6f)
            {
                num18 = 0f;
            }
            if (NPC.ai[0] == 7f)
            {
                num18 = 0f;
            }
            if (NPC.ai[0] == 3f)
            {
                num18 = 0.01f;
            }
            if (NPC.ai[0] == 4f)
            {
                num18 = 0.01f;
            }
            if (NPC.ai[0] == 8f)
            {
                num18 = 0.01f;
            }
            if (NPC.rotation < num17)
            {
                if ((double)(num17 - NPC.rotation) > 3.1415926535897931)
                {
                    NPC.rotation -= num18;
                }
                else
                {
                    NPC.rotation += num18;
                }
            }
            if (NPC.rotation > num17)
            {
                if ((double)(NPC.rotation - num17) > 3.1415926535897931)
                {
                    NPC.rotation += num18;
                }
                else
                {
                    NPC.rotation -= num18;
                }
            }
            if (NPC.rotation > num17 - num18 && NPC.rotation < num17 + num18)
            {
                NPC.rotation = num17;
            }
            if (NPC.rotation < 0f)
            {
                NPC.rotation += 6.28318548f;
            }
            if (NPC.rotation > 6.28318548f)
            {
                NPC.rotation -= 6.28318548f;
            }
            if (NPC.rotation > num17 - num18 && NPC.rotation < num17 + num18)
            {
                NPC.rotation = num17;
            }
            if (NPC.ai[0] != -1f && NPC.ai[0] < 9f)
            {
                bool flag7 = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);
                if (flag7)
                {
                    NPC.alpha += 15;
                }
                else
                {
                    NPC.alpha -= 15;
                }
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
                if (NPC.alpha > 150)
                {
                    NPC.alpha = 150;
                }
            }
            if (NPC.ai[0] == -1f)
            {
                NPC.velocity *= 0.98f;
                int num19 = Math.Sign(player.Center.X - vector.X);
                if (num19 != 0)
                {
                    NPC.direction = num19;
                    NPC.spriteDirection = -NPC.direction;
                }
                if (NPC.ai[2] > 20f)
                {
                    NPC.velocity.Y = -2f;
                    NPC.alpha -= 5;
                    bool flag8 = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);
                    if (flag8)
                    {
                        NPC.alpha += 15;
                    }
                    if (NPC.alpha < 0)
                    {
                        NPC.alpha = 0;
                    }
                    if (NPC.alpha > 150)
                    {
                        NPC.alpha = 150;
                    }
                }
                if (NPC.ai[2] == num9 - 30)
                {
                    int num20 = 36;
                    for (int i = 0; i < num20; i++)
                    {
                        Vector2 vector2 = Vector2.Normalize(NPC.velocity) * new Vector2(NPC.width / 2f, NPC.height) * 0.75f * 0.5f;
                        vector2 = vector2.RotatedBy((double)((i - (num20 / 2 - 1)) * 6.28318548f / num20), default) + NPC.Center;
                        Vector2 value = vector2 - NPC.Center;
                        int num21 = Dust.NewDust(vector2 + value, 0, 0, ModContent.DustType<Dusts.CthulhuDust>(), value.X * 2f, value.Y * 2f, 100, default, 1.4f);
                        Main.dust[num21].noGravity = true;
                        Main.dust[num21].noLight = true;
                        Main.dust[num21].velocity = Vector2.Normalize(value) * 3f;
                    }
                    SoundEngine.PlaySound(SoundID.Zombie20, vector);
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= num16)
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 0f && !player.dead)
            {
                if (NPC.ai[1] == 0f)
                {
                    NPC.ai[1] = 300 * Math.Sign((vector - player.Center).X);
                }
                Vector2 value2 = player.Center + new Vector2(NPC.ai[1], -200f) - vector;
                Vector2 vector3 = Vector2.Normalize(value2 - NPC.velocity) * scaleFactor;
                if (NPC.velocity.X < vector3.X)
                {
                    NPC.velocity.X = NPC.velocity.X + npcVelocity;
                    if (NPC.velocity.X < 0f && vector3.X > 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + npcVelocity;
                    }
                }
                else if (NPC.velocity.X > vector3.X)
                {
                    NPC.velocity.X = NPC.velocity.X - npcVelocity;
                    if (NPC.velocity.X > 0f && vector3.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - npcVelocity;
                    }
                }
                if (NPC.velocity.Y < vector3.Y)
                {
                    NPC.velocity.Y = NPC.velocity.Y + npcVelocity;
                    if (NPC.velocity.Y < 0f && vector3.Y > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + npcVelocity;
                    }
                }
                else if (NPC.velocity.Y > vector3.Y)
                {
                    NPC.velocity.Y = NPC.velocity.Y - npcVelocity;
                    if (NPC.velocity.Y > 0f && vector3.Y < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - npcVelocity;
                    }
                }
                int num22 = Math.Sign(player.Center.X - vector.X);
                if (num22 != 0)
                {
                    if (NPC.ai[2] == 0f && num22 != NPC.direction)
                    {
                        NPC.rotation += 3.14159274f;
                    }
                    NPC.direction = num22;
                    if (NPC.spriteDirection != -NPC.direction)
                    {
                        NPC.rotation += 3.14159274f;
                    }
                    NPC.spriteDirection = -NPC.direction;
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= aiChangeRate)
                {
                    int num23 = 0;
                    switch ((int)NPC.ai[3])
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            num23 = 1;
                            break;
                        case 10:
                            NPC.ai[3] = 1f;
                            num23 = 2;
                            break;
                        case 11:
                            NPC.ai[3] = 0f;
                            num23 = 3;
                            break;
                    }
                    if (Phase2Check)
                    {
                        num23 = 4;
                    }
                    if (num23 == 1)
                    {
                        NPC.ai[0] = 1f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                        NPC.velocity = Vector2.Normalize(player.Center - vector) * ChargeSpeed;
                        NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);
                        if (num22 != 0)
                        {
                            NPC.direction = num22;
                            if (NPC.spriteDirection == 1)
                            {
                                NPC.rotation += 3.14159274f;
                            }
                            NPC.spriteDirection = -NPC.direction;
                        }
                    }
                    else if (num23 == 2)
                    {
                        NPC.ai[0] = 2f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                    }
                    else if (num23 == 3)
                    {
                        NPC.ai[0] = 3f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                    }
                    else if (num23 == 4)
                    {
                        NPC.ai[0] = 4f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                    }
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 1f)
            {
                int num24 = 7;
                for (int j = 0; j < num24; j++)
                {
                    Vector2 vector4 = Vector2.Normalize(NPC.velocity) * new Vector2((NPC.width + 50) / 2f, NPC.height) * 0.75f;
                    vector4 = vector4.RotatedBy((j - (num24 / 2 - 1)) * 3.1415926535897931 / (double)(float)num24, default) + vector;
                    Vector2 value3 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);
                    int num25 = Dust.NewDust(vector4 + value3, 0, 0, ModContent.DustType<Dusts.CthulhuDust>(), value3.X * 2f, value3.Y * 2f, 100, default, 1.4f);
                    Main.dust[num25].noGravity = true;
                    Main.dust[num25].noLight = true;
                    Main.dust[num25].velocity /= 4f;
                    Main.dust[num25].velocity -= NPC.velocity;
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= ChargeTime)
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] += 2f;
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 2f)
            {
                if (NPC.ai[1] == 0f)
                {
                    NPC.ai[1] = 300 * Math.Sign((vector - player.Center).X);
                }
                Vector2 value4 = player.Center + new Vector2(NPC.ai[1], -200f) - vector;
                Vector2 vector5 = Vector2.Normalize(value4 - NPC.velocity) * scaleFactor2;
                if (NPC.velocity.X < vector5.X)
                {
                    NPC.velocity.X = NPC.velocity.X + num8;
                    if (NPC.velocity.X < 0f && vector5.X > 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + num8;
                    }
                }
                else if (NPC.velocity.X > vector5.X)
                {
                    NPC.velocity.X = NPC.velocity.X - num8;
                    if (NPC.velocity.X > 0f && vector5.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - num8;
                    }
                }
                if (NPC.velocity.Y < vector5.Y)
                {
                    NPC.velocity.Y = NPC.velocity.Y + num8;
                    if (NPC.velocity.Y < 0f && vector5.Y > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num8;
                    }
                }
                else if (NPC.velocity.Y > vector5.Y)
                {
                    NPC.velocity.Y = NPC.velocity.Y - num8;
                    if (NPC.velocity.Y > 0f && vector5.Y < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num8;
                    }
                }
                if (NPC.ai[2] == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Zombie20, vector);
                }
                if (NPC.ai[2] % num7 == 0f)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.Center);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 vector6 = Vector2.Normalize(player.Center - vector) * (NPC.width + 20) / 2f + vector;
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector6.X, (int)vector6.Y + 45, ModContent.NPCType<LeviathanBubble>(), 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                int num26 = Math.Sign(player.Center.X - vector.X);
                if (num26 != 0)
                {
                    NPC.direction = num26;
                    if (NPC.spriteDirection != -NPC.direction)
                    {
                        NPC.rotation += 3.14159274f;
                    }
                    NPC.spriteDirection = -NPC.direction;
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= num6)
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 3f)
            {
                NPC.velocity *= 0.98f;
                NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
                if (NPC.ai[2] == num9 - 30)
                {
                    SoundEngine.PlaySound(SoundID.Zombie9, vector);
                }
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[2] == num9 - 30)
                {
                    Vector2 vector7 = NPC.rotation.ToRotationVector2() * (Vector2.UnitX * NPC.direction) * (NPC.width + 20) / 2f + vector;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), vector7.X, vector7.Y, NPC.direction * 2, 8f, ModContent.ProjectileType<DeityLeviathan_RazorbladeRift>(), 0, 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), vector7.X, vector7.Y, (float)(-(float)NPC.direction * 2), 8f, ModContent.ProjectileType<DeityLeviathan_RazorbladeRift>(), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= num9)
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 4f)
            {
                NPC.velocity *= 0.98f;
                NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
                if (NPC.ai[2] == num10 - 60)
                {
                    SoundEngine.PlaySound(SoundID.Zombie20, vector);
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= num10)
                {
                    NPC.ai[0] = 5f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 5f && !player.dead)
            {
                if (NPC.ai[1] == 0f)
                {
                    NPC.ai[1] = 300 * Math.Sign((vector - player.Center).X);
                }
                Vector2 value5 = player.Center + new Vector2(NPC.ai[1], -200f) - vector;
                Vector2 vector8 = Vector2.Normalize(value5 - NPC.velocity) * scaleFactor;
                if (NPC.velocity.X < vector8.X)
                {
                    NPC.velocity.X = NPC.velocity.X + npcVelocity;
                    if (NPC.velocity.X < 0f && vector8.X > 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + npcVelocity;
                    }
                }
                else if (NPC.velocity.X > vector8.X)
                {
                    NPC.velocity.X = NPC.velocity.X - npcVelocity;
                    if (NPC.velocity.X > 0f && vector8.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - npcVelocity;
                    }
                }
                if (NPC.velocity.Y < vector8.Y)
                {
                    NPC.velocity.Y = NPC.velocity.Y + npcVelocity;
                    if (NPC.velocity.Y < 0f && vector8.Y > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + npcVelocity;
                    }
                }
                else if (NPC.velocity.Y > vector8.Y)
                {
                    NPC.velocity.Y = NPC.velocity.Y - npcVelocity;
                    if (NPC.velocity.Y > 0f && vector8.Y < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - npcVelocity;
                    }
                }
                int num27 = Math.Sign(player.Center.X - vector.X);
                if (num27 != 0)
                {
                    if (NPC.ai[2] == 0f && num27 != NPC.direction)
                    {
                        NPC.rotation += 3.14159274f;
                    }
                    NPC.direction = num27;
                    if (NPC.spriteDirection != -NPC.direction)
                    {
                        NPC.rotation += 3.14159274f;
                    }
                    NPC.spriteDirection = -NPC.direction;
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= aiChangeRate)
                {
                    int num28 = 0;
                    switch ((int)NPC.ai[3])
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            num28 = 1;
                            break;
                        case 6:
                            NPC.ai[3] = 1f;
                            num28 = 2;
                            break;
                        case 7:
                            NPC.ai[3] = 0f;
                            num28 = 3;
                            break;
                    }
                    if (ExpertPhaseCheck)
                    {
                        num28 = 4;
                    }
                    if (num28 == 1)
                    {
                        NPC.ai[0] = 6f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                        NPC.velocity = Vector2.Normalize(player.Center - vector) * ChargeSpeed;
                        NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);
                        if (num27 != 0)
                        {
                            NPC.direction = num27;
                            if (NPC.spriteDirection == 1)
                            {
                                NPC.rotation += 3.14159274f;
                            }
                            NPC.spriteDirection = -NPC.direction;
                        }
                    }
                    else if (num28 == 2)
                    {
                        NPC.velocity = Vector2.Normalize(player.Center - vector) * scaleFactor4;
                        NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);
                        if (num27 != 0)
                        {
                            NPC.direction = num27;
                            if (NPC.spriteDirection == 1)
                            {
                                NPC.rotation += 3.14159274f;
                            }
                            NPC.spriteDirection = -NPC.direction;
                        }
                        NPC.ai[0] = 7f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                    }
                    else if (num28 == 3)
                    {
                        NPC.ai[0] = 8f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                    }
                    else if (num28 == 4)
                    {
                        NPC.ai[0] = 9f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                    }
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 6f)
            {
                int num29 = 7;
                for (int k = 0; k < num29; k++)
                {
                    Vector2 vector9 = Vector2.Normalize(NPC.velocity) * new Vector2((NPC.width + 50) / 2f, NPC.height) * 0.75f;
                    vector9 = vector9.RotatedBy((k - (num29 / 2 - 1)) * 3.1415926535897931 / (double)(float)num29, default) + vector;
                    Vector2 value6 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);
                    int num30 = Dust.NewDust(vector9 + value6, 0, 0, ModContent.DustType<Dusts.CthulhuDust>(), value6.X * 2f, value6.Y * 2f, 100, default, 1.4f);
                    Main.dust[num30].noGravity = true;
                    Main.dust[num30].noLight = true;
                    Main.dust[num30].velocity /= 4f;
                    Main.dust[num30].velocity -= NPC.velocity;
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= ChargeTime)
                {
                    NPC.ai[0] = 5f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] += 2f;
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 7f)
            {
                if (NPC.ai[2] == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Zombie20, vector);
                }
                if (NPC.ai[2] % num14 == 0f)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.Center);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 vector10 = Vector2.Normalize(NPC.velocity) * (NPC.width + 20) / 2f + vector;
                        int num31 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector10.X, (int)vector10.Y + 45, ModContent.NPCType<LeviathanBubble>(), 0, 0f, 0f, 0f, 0f, 255);
                        Main.npc[num31].target = NPC.target;
                        Main.npc[num31].velocity = Vector2.Normalize(NPC.velocity).RotatedBy((double)(1.57079637f * NPC.direction), default) * scaleFactor3;
                        Main.npc[num31].netUpdate = true;
                        Main.npc[num31].ai[3] = Main.rand.Next(80, 121) / 100f;
                    }
                }
                NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num15 * NPC.direction), default);
                NPC.rotation -= num15 * NPC.direction;
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= num13)
                {
                    NPC.ai[0] = 5f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 8f)
            {
                NPC.velocity *= 0.98f;
                NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
                if (NPC.ai[2] == num9 - 30)
                {
                    SoundEngine.PlaySound(SoundID.Zombie20, vector);
                }
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[2] == num9 - 30)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), vector.X, vector.Y, 0f, 0f, ModContent.ProjectileType<DeityLeviathan_RazorbladeRift>(), 0, 0f, Main.myPlayer, 1f, NPC.target + 1);
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= num9)
                {
                    NPC.ai[0] = 5f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 9f)
            {
                if (NPC.ai[2] < num11 - 90)
                {
                    bool flag9 = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);
                    if (flag9)
                    {
                        NPC.alpha += 15;
                    }
                    else
                    {
                        NPC.alpha -= 15;
                    }
                    if (NPC.alpha < 0)
                    {
                        NPC.alpha = 0;
                    }
                    if (NPC.alpha > 150)
                    {
                        NPC.alpha = 150;
                    }
                }
                else if (NPC.alpha < 255)
                {
                    NPC.alpha += 4;
                    if (NPC.alpha > 255)
                    {
                        NPC.alpha = 255;
                    }
                }
                NPC.velocity *= 0.98f;
                NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
                if (NPC.ai[2] == num11 - 60)
                {
                    SoundEngine.PlaySound(SoundID.Zombie20, vector);
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= num11)
                {
                    NPC.ai[0] = 10f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 10f && !player.dead)
            {
                NPC.dontTakeDamage = false;
                NPC.chaseable = false;
                if (NPC.alpha < 255)
                {
                    NPC.alpha += 25;
                    if (NPC.alpha > 255)
                    {
                        NPC.alpha = 255;
                    }
                }
                if (NPC.ai[1] == 0f)
                {
                    NPC.ai[1] = 360 * Math.Sign((vector - player.Center).X);
                }
                Vector2 value7 = player.Center + new Vector2(NPC.ai[1], -200f) - vector;
                Vector2 desiredVelocity = Vector2.Normalize(value7 - NPC.velocity) * scaleFactor;
                NPC.SimpleFlyMovement(desiredVelocity, npcVelocity);
                int num32 = Math.Sign(player.Center.X - vector.X);
                if (num32 != 0)
                {
                    if (NPC.ai[2] == 0f && num32 != NPC.direction)
                    {
                        NPC.rotation += 3.14159274f;
                        for (int l = 0; l < NPC.oldPos.Length; l++)
                        {
                            NPC.oldPos[l] = Vector2.Zero;
                        }
                    }
                    NPC.direction = num32;
                    if (NPC.spriteDirection != -NPC.direction)
                    {
                        NPC.rotation += 3.14159274f;
                    }
                    NPC.spriteDirection = -NPC.direction;
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= aiChangeRate)
                {
                    int num33 = 0;
                    switch ((int)NPC.ai[3])
                    {
                        case 0:
                        case 2:
                        case 3:
                        case 5:
                        case 6:
                        case 7:
                            num33 = 1;
                            break;
                        case 1:
                        case 4:
                        case 8:
                            num33 = 2;
                            break;
                    }
                    if (num33 == 1)
                    {
                        NPC.ai[0] = 11f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                        NPC.velocity = Vector2.Normalize(player.Center - vector) * ChargeSpeed;
                        NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);
                        if (num32 != 0)
                        {
                            NPC.direction = num32;
                            if (NPC.spriteDirection == 1)
                            {
                                NPC.rotation += 3.14159274f;
                            }
                            NPC.spriteDirection = -NPC.direction;
                        }
                    }
                    else if (num33 == 2)
                    {
                        NPC.ai[0] = 12f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                    }
                    else if (num33 == 3)
                    {
                        NPC.ai[0] = 13f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                    }
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 11f)
            {
                NPC.dontTakeDamage = false;
                NPC.chaseable = true;
                NPC.alpha -= 25;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
                int num34 = 7;
                for (int m = 0; m < num34; m++)
                {
                    Vector2 vector11 = Vector2.Normalize(NPC.velocity) * new Vector2((NPC.width + 50) / 2f, NPC.height) * 0.75f;
                    vector11 = vector11.RotatedBy((m - (num34 / 2 - 1)) * 3.1415926535897931 / (double)(float)num34, default) + vector;
                    Vector2 value8 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);
                    int num35 = Dust.NewDust(vector11 + value8, 0, 0, ModContent.DustType<Dusts.CthulhuDust>(), value8.X * 2f, value8.Y * 2f, 100, default, 1.4f);
                    Main.dust[num35].noGravity = true;
                    Main.dust[num35].noLight = true;
                    Main.dust[num35].velocity /= 4f;
                    Main.dust[num35].velocity -= NPC.velocity;
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= ChargeTime)
                {
                    NPC.ai[0] = 10f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] += 1f;
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 12f)
            {
                NPC.dontTakeDamage = true;
                NPC.chaseable = false;
                if (NPC.alpha < 255)
                {
                    NPC.alpha += 17;
                    if (NPC.alpha > 255)
                    {
                        NPC.alpha = 255;
                    }
                }
                NPC.velocity *= 0.98f;
                NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
                if (NPC.ai[2] == num12 / 2)
                {
                    SoundEngine.PlaySound(SoundID.Zombie20, vector);
                }
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[2] == num12 / 2)
                {
                    if (NPC.ai[1] == 0f)
                    {
                        NPC.ai[1] = 300 * Math.Sign((vector - player.Center).X);
                    }
                    Vector2 center = player.Center + new Vector2(-NPC.ai[1], -200f);
                    vector = NPC.Center = center;
                    int num36 = Math.Sign(player.Center.X - vector.X);
                    if (num36 != 0)
                    {
                        if (NPC.ai[2] == 0f && num36 != NPC.direction)
                        {
                            NPC.rotation += 3.14159274f;
                            for (int n = 0; n < NPC.oldPos.Length; n++)
                            {
                                NPC.oldPos[n] = Vector2.Zero;
                            }
                        }
                        NPC.direction = num36;
                        if (NPC.spriteDirection != -NPC.direction)
                        {
                            NPC.rotation += 3.14159274f;
                        }
                        NPC.spriteDirection = -NPC.direction;
                    }
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= num12)
                {
                    NPC.ai[0] = 10f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] += 1f;
                    if (NPC.ai[3] >= 9f)
                    {
                        NPC.ai[3] = 0f;
                    }
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 13f)
            {
                if (NPC.ai[2] == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Zombie20, vector);
                }
                NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num15 * NPC.direction), default);
                NPC.rotation -= num15 * NPC.direction;
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= num13)
                {
                    NPC.ai[0] = 10f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] += 1f;
                    NPC.netUpdate = true;
                }
            }
        }
        
        
    }
}
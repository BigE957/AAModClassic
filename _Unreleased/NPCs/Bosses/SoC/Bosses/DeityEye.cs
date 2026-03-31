using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using AAModClassic._Unreleased;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic;
using AAModClassic.Dusts;

namespace AAModClassic._Unreleased.NPCs.Bosses.SoC.Bosses
{
    [AutoloadBossHead]
    public class DeityEye : ModNPC
    {
        public bool HeadsSpawned = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cyaegha");

            Main.npcFrameCount[NPC.type] = 6;
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 110;
            NPC.aiStyle = -1;
            NPC.defense = 100;
            NPC.damage = 60;
            NPC.lifeMax = 150000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.timeLeft = NPC.activeTime * 30;
            NPC.boss = true;
            NPC.npcSlots = 5f;
            Music = Mod.GetSoundSlot(SoundType.Music, "_Unreleased/Sounds/Music/SoC");
            for (int m = 0; m < NPC.buffImmune.Length; m++) NPC.buffImmune[m] = true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                SoC.ComeBack = true;
            }
        }

        //TODOSOC
        /*
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (AAWorld_Unreleased.Anticheat == true)
            {
                if (damage > NPC.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    damage = 0;
                }

                return false;
            }

            return true;
        }
        */

        public override bool PreKill()
        {
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            int num = 1;
            if (!Main.dedServ)
            {
                Main.instance.LoadNPC(NPC.type);
                if (TextureAssets.Npc[NPC.type].Value == null)
                {
                    return;
                }
                num = TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type];
            }
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter < 7.0)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.frameCounter < 14.0)
            {
                NPC.frame.Y = num;
            }
            else if (NPC.frameCounter < 21.0)
            {
                NPC.frame.Y = num * 2;
            }
            else
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y = 0;
            }
            if (NPC.ai[0] > 1f)
            {
                NPC.frame.Y = NPC.frame.Y + num * 3;
                return;
            }
        }

        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            
            bool dead3 = Main.player[NPC.target].dead;
            float num406 = NPC.position.X + (float)(NPC.width / 2) - Main.player[NPC.target].position.X - (float)(Main.player[NPC.target].width / 2);
            float num407 = NPC.position.Y + (float)NPC.height - 59f - Main.player[NPC.target].position.Y - (float)(Main.player[NPC.target].height / 2);
            float num408 = (float)Math.Atan2((double)num407, (double)num406) + 1.57f;
            if (num408 < 0f)
            {
                num408 += 6.283f;
            }
            else if ((double)num408 > 6.283)
            {
                num408 -= 6.283f;
            }
            float num409 = 0.15f;
            if (NPC.rotation < num408)
            {
                if ((double)(num408 - NPC.rotation) > 3.1415)
                {
                    NPC.rotation -= num409;
                }
                else
                {
                    NPC.rotation += num409;
                }
            }
            else if (NPC.rotation > num408)
            {
                if ((double)(NPC.rotation - num408) > 3.1415)
                {
                    NPC.rotation += num409;
                }
                else
                {
                    NPC.rotation -= num409;
                }
            }
            if (NPC.rotation > num408 - num409 && NPC.rotation < num408 + num409)
            {
                NPC.rotation = num408;
            }
            if (NPC.rotation < 0f)
            {
                NPC.rotation += 6.283f;
            }
            else if ((double)NPC.rotation > 6.283)
            {
                NPC.rotation -= 6.283f;
            }
            if (NPC.rotation > num408 - num409 && NPC.rotation < num408 + num409)
            {
                NPC.rotation = num408;
            }
            if (Main.rand.Next(5) == 0)
            {
                int num410 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height * 0.25f), NPC.width, (int)((float)NPC.height * 0.5f), 5, NPC.velocity.X, 2f, 0, default(Color), 1f);
                Dust expr_1447B_cp_0 = Main.dust[num410];
                expr_1447B_cp_0.velocity.X = expr_1447B_cp_0.velocity.X * 0.5f;
                Dust expr_1449B_cp_0 = Main.dust[num410];
                expr_1449B_cp_0.velocity.Y = expr_1449B_cp_0.velocity.Y * 0.1f;
            }
            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000.0 || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000.0)
            {
                NPC.alpha += 5;
                if (NPC.alpha >= 255)
                {
                    NPC.active = false;
                }
            }
            else
            {
                NPC.alpha -= 5;
            }
            if (NPC.ai[0] == 0f)
            {
                if (NPC.ai[1] == 0f)
                {
                    NPC.TargetClosest(true);
                    float num412 = 12f;
                    float num413 = 0.4f;
                    int num414 = 1;
                    if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width)
                    {
                        num414 = -1;
                    }
                    Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    float num415 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(num414 * 400) - vector40.X;
                    float num416 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector40.Y;
                    float num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
                    num417 = num412 / num417;
                    num415 *= num417;
                    num416 *= num417;
                    if (NPC.velocity.X < num415)
                    {
                        NPC.velocity.X = NPC.velocity.X + num413;
                        if (NPC.velocity.X < 0f && num415 > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num413;
                        }
                    }
                    else if (NPC.velocity.X > num415)
                    {
                        NPC.velocity.X = NPC.velocity.X - num413;
                        if (NPC.velocity.X > 0f && num415 < 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X - num413;
                        }
                    }
                    if (NPC.velocity.Y < num416)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num413;
                        if (NPC.velocity.Y < 0f && num416 > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num413;
                        }
                    }
                    else if (NPC.velocity.Y > num416)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num413;
                        if (NPC.velocity.Y > 0f && num416 < 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num413;
                        }
                    }
                    NPC.ai[2] += 1f;
                    if (NPC.ai[2] >= 600f)
                    {
                        NPC.ai[1] = 1f;
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 0f;
                        NPC.target = 255;
                        NPC.netUpdate = true;
                    }
                    else
                    {
                        if (!Main.player[NPC.target].dead)
                        {
                            NPC.ai[3] += 1f;
                            if (Main.expertMode && (double)NPC.life < (double)NPC.lifeMax * 0.8)
                            {
                                NPC.ai[3] += 0.6f;
                            }
                        }
                        if (NPC.ai[3] >= 60f)
                        {
                            NPC.ai[3] = 0f;
                            vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                            num415 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector40.X;
                            num416 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector40.Y;
                            if (Main.netMode != 1)
                            {
                                float num418 = 12f;
                                int num419 = 25;
                                int num420 = ModContent.ProjectileType<DeityFlames>();
                                if (Main.expertMode)
                                {
                                    num418 = 14f;
                                    num419 = 22;
                                }
                                num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
                                num417 = num418 / num417;
                                num415 *= num417;
                                num416 *= num417;
                                num415 += (float)Main.rand.Next(-40, 41) * 0.05f;
                                num416 += (float)Main.rand.Next(-40, 41) * 0.05f;
                                vector40.X += num415 * 4f;
                                vector40.Y += num416 * 4f;
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), vector40.X, vector40.Y, num415, num416, num420, num419, 0f, Main.myPlayer, 0f, 0f);
                            }
                        }
                    }
                }
                else if (NPC.ai[1] == 1f)
                {
                    NPC.rotation = num408;
                    float num421 = 13f;
                    if (Main.expertMode)
                    {
                        if ((double)NPC.life < (double)NPC.lifeMax * 0.9)
                        {
                            num421 += 0.5f;
                        }
                        if ((double)NPC.life < (double)NPC.lifeMax * 0.8)
                        {
                            num421 += 0.5f;
                        }
                        if ((double)NPC.life < (double)NPC.lifeMax * 0.7)
                        {
                            num421 += 0.55f;
                        }
                        if ((double)NPC.life < (double)NPC.lifeMax * 0.6)
                        {
                            num421 += 0.6f;
                        }
                        if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
                        {
                            num421 += 0.65f;
                        }
                    }
                    Vector2 vector41 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    float num422 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector41.X;
                    float num423 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector41.Y;
                    float num424 = (float)Math.Sqrt((double)(num422 * num422 + num423 * num423));
                    num424 = num421 / num424;
                    NPC.velocity.X = num422 * num424;
                    NPC.velocity.Y = num423 * num424;
                    NPC.ai[1] = 2f;
                }
                else if (NPC.ai[1] == 2f)
                {
                    NPC.ai[2] += 1f;
                    if (NPC.ai[2] >= 8f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.9f;
                        NPC.velocity.Y = NPC.velocity.Y * 0.9f;
                        if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
                        {
                            NPC.velocity.X = 0f;
                        }
                        if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1)
                        {
                            NPC.velocity.Y = 0f;
                        }
                    }
                    else
                    {
                        NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
                    }
                    if (NPC.ai[2] >= 42f)
                    {
                        NPC.ai[3] += 1f;
                        NPC.ai[2] = 0f;
                        NPC.target = 255;
                        NPC.rotation = num408;
                        if (NPC.ai[3] >= 10f)
                        {
                            NPC.ai[1] = 0f;
                            NPC.ai[3] = 0f;
                        }
                        else
                        {
                            NPC.ai[1] = 1f;
                        }
                    }
                }
                if ((double)NPC.life < (double)NPC.lifeMax * 0.4)
                {
                    NPC.ai[0] = 1f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 1f || NPC.ai[0] == 2f)
            {
                if (NPC.ai[0] == 1f)
                {
                    NPC.ai[2] += 0.005f;
                    if ((double)NPC.ai[2] > 0.5)
                    {
                        NPC.ai[2] = 0.5f;
                    }
                }
                else
                {
                    NPC.ai[2] -= 0.005f;
                    if (NPC.ai[2] < 0f)
                    {
                        NPC.ai[2] = 0f;
                    }
                }
                NPC.rotation += NPC.ai[2];
                NPC.ai[1] += 1f;
                if (NPC.ai[1] == 100f)
                {
                    NPC.ai[0] += 1f;
                    NPC.ai[1] = 0f;
                    if (NPC.ai[0] == 3f)
                    {
                        NPC.ai[2] = 0f;
                    }
                    else
                    {
                        for (int num426 = 0; num426 < 20; num426++)
                        {
                            Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                        }
                        SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                    }
                }
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                NPC.velocity.X = NPC.velocity.X * 0.98f;
                NPC.velocity.Y = NPC.velocity.Y * 0.98f;
                if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
                {
                    NPC.velocity.X = 0f;
                }
                if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1)
                {
                    NPC.velocity.Y = 0f;
                    return;
                }
            }
            else
            {
                NPC.HitSound = SoundID.NPCHit1;
                NPC.damage = (int)((double)NPC.defDamage * 1.5);
                NPC.defense = NPC.defDefense + 18;
                if (NPC.ai[1] == 0f)
                {
                    float num427 = 4f;
                    float num428 = 0.1f;
                    int num429 = 1;
                    if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width)
                    {
                        num429 = -1;
                    }
                    Vector2 vector42 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    float num430 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(num429 * 400) - vector42.X;
                    float num431 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector42.Y;
                    float num432 =  (float)Math.Sqrt((num430 * num430) + (num431 * num431));
                    if (Main.expertMode)
                    {
                        if (num432 > 300f)
                        {
                            num427 += 0.5f;
                        }
                        if (num432 > 400f)
                        {
                            num427 += 0.5f;
                        }
                        if (num432 > 500f)
                        {
                            num427 += 0.55f;
                        }
                        if (num432 > 600f)
                        {
                            num427 += 0.55f;
                        }
                        if (num432 > 700f)
                        {
                            num427 += 0.6f;
                        }
                        if (num432 > 800f)
                        {
                            num427 += 0.6f;
                        }
                    }
                    num432 = num427 / num432;
                    num430 *= num432;
                    num431 *= num432;
                    if (NPC.velocity.X < num430)
                    {
                        NPC.velocity.X = NPC.velocity.X + num428;
                        if (NPC.velocity.X < 0f && num430 > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num428;
                        }
                    }
                    else if (NPC.velocity.X > num430)
                    {
                        NPC.velocity.X = NPC.velocity.X - num428;
                        if (NPC.velocity.X > 0f && num430 < 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X - num428;
                        }
                    }
                    if (NPC.velocity.Y < num431)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num428;
                        if (NPC.velocity.Y < 0f && num431 > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num428;
                        }
                    }
                    else if (NPC.velocity.Y > num431)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num428;
                        if (NPC.velocity.Y > 0f && num431 < 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num428;
                        }
                    }
                    NPC.ai[2] += 1f;
                    if (NPC.ai[2] >= 400f)
                    {
                        NPC.ai[1] = 1f;
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 0f;
                        NPC.target = 255;
                        NPC.netUpdate = true;
                    }
                    if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                    {
                        NPC.localAI[2] += 1f;
                        if (NPC.localAI[2] > 22f)
                        {
                            NPC.localAI[2] = 0f;
                            SoundEngine.PlaySound(SoundID.Item34, NPC.position);
                        }
                        if (Main.netMode != 1)
                        {
                            NPC.localAI[1] += 1f;
                            if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
                            {
                                NPC.localAI[1] += 1f;
                            }
                            if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
                            {
                                NPC.localAI[1] += 1f;
                            }
                            if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
                            {
                                NPC.localAI[1] += 1f;
                            }
                            if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
                            {
                                NPC.localAI[1] += 2f;
                            }
                            if (NPC.localAI[1] > 8f)
                            {
                                NPC.localAI[1] = 0f;
                                float num433 = 6f;
                                int num434 = 30;
                                if (Main.expertMode)
                                {
                                    num434 = 27;
                                }
                                int num435 = ModContent.ProjectileType<DeityFlames>();
                                vector42 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                                num430 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector42.X;
                                num431 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector42.Y;
                                num432 = (float)Math.Sqrt((double)(num430 * num430 + num431 * num431));
                                num432 = num433 / num432;
                                num430 *= num432;
                                num431 *= num432;
                                num431 += (float)Main.rand.Next(-40, 41) * 0.01f;
                                num430 += (float)Main.rand.Next(-40, 41) * 0.01f;
                                num431 += NPC.velocity.Y * 0.5f;
                                num430 += NPC.velocity.X * 0.5f;
                                vector42.X -= num430 * 1f;
                                vector42.Y -= num431 * 1f;
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), vector42.X, vector42.Y, num430, num431, num435, num434, 0f, Main.myPlayer, 0f, 0f);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (NPC.ai[1] == 1f)
                    {
                        SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                        NPC.rotation = num408;
                        float num436 = 14f;
                        if (Main.expertMode)
                        {
                            num436 += 2.5f;
                        }
                        Vector2 vector43 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                        float num437 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector43.X;
                        float num438 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector43.Y;
                        float num439 = (float)Math.Sqrt((double)(num437 * num437 + num438 * num438));
                        num439 = num436 / num439;
                        NPC.velocity.X = num437 * num439;
                        NPC.velocity.Y = num438 * num439;
                        NPC.ai[1] = 2f;
                        return;
                    }
                    if (NPC.ai[1] == 2f)
                    {
                        NPC.ai[2] += 1f;
                        if (Main.expertMode)
                        {
                            NPC.ai[2] += 0.5f;
                        }
                        if (NPC.ai[2] >= 50f)
                        {
                            NPC.velocity.X = NPC.velocity.X * 0.93f;
                            NPC.velocity.Y = NPC.velocity.Y * 0.93f;
                            if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
                            {
                                NPC.velocity.X = 0f;
                            }
                            if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1)
                            {
                                NPC.velocity.Y = 0f;
                            }
                        }
                        else
                        {
                            NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
                        }
                        if (NPC.ai[2] >= 80f)
                        {
                            NPC.ai[3] += 1f;
                            NPC.ai[2] = 0f;
                            NPC.target = 255;
                            NPC.rotation = num408;
                            if (NPC.ai[3] >= 6f)
                            {
                                NPC.ai[1] = 0f;
                                NPC.ai[3] = 0f;
                                return;
                            }
                            NPC.ai[1] = 1f;
                            return;
                        }
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D currentTex = TextureAssets.Npc[NPC.type].Value;
            Texture2D GlowTex = Mod.GetTexture("_Unreleased/Glowmasks/DeityEye_Glow");
            
            BaseDrawing.DrawTexture(spriteBatch, currentTex, 0, NPC, drawColor);

            //draw glow/glow afterimage
            BaseDrawing.DrawTexture(spriteBatch, GlowTex, 0, NPC, AAColor.Cthulhu2);
            BaseDrawing.DrawAfterimage(spriteBatch, GlowTex, 0, NPC, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);
            
            return false;
        }

    }
}
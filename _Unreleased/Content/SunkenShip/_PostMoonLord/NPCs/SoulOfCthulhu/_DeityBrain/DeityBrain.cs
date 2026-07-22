using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu;
using AAModClassic._Unreleased.Content.SunkenShip.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityBrain
{
    [AutoloadBossHead]
    public class DeityBrain : ModNPC
    {
        public override string BossHeadTexture => "AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/NPCs/SoulOfCthulhu/_DeityEater/DeityEater_Head_Boss";
        //public override string Texture { get { return "AAMod/NPCs/Bosses/SoC/Bosses/DeityEater"; } }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lu'Kthu");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            Main.npcFrameCount[NPC.type] = 8;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.damage = 90;
            NPC.defense = 100;
            NPC.lifeMax = 150000;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath11;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.behindTiles = true;
            NPC.scale = 1f;
            NPC.boss = true;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[39] = true;
            for (int m = 0; m < NPC.buffImmune.Length; m++) NPC.buffImmune[m] = true;
            NPC.dontTakeDamage = true;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            NPC.timeLeft = NPC.activeTime * 30;
            Music = NPC.AnyNPCs(ModContent.NPCType<Cthulhu>()) ? MusicManagementSystem.MusicSlots["Cthulhu"] : MusicManagementSystem.MusicSlots["SoulOfCthulhu"];
            SpawnModBiomes = [ModContent.GetInstance<SunkenShipBiome>().Type];
        }

        public override void FindFrame(int frameHeight)
        {
            int num = TextureAssets.Npc[NPC.type].Height() / Main.npcFrameCount[NPC.type];
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter > 6.0)
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y = NPC.frame.Y + num;
            }
            if (NPC.ai[0] >= 0f)
            {
                if (NPC.frame.Y > num * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
            else
            {
                if (NPC.frame.Y < num * 4)
                {
                    NPC.frame.Y = num * 4;
                }
                if (NPC.frame.Y > num * 7)
                {
                    NPC.frame.Y = num * 4;
                }
            }
        }


        public static int EyeCount => Main.expertMode ? 20 : 15;
        //public int[] totalEyes = null;
        //public int fireTimer = 0;

        //Client Side
        public bool spawnAlpha = false;

        public override void AI()
        {
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }

            if (!Main.dedServ)
            {
                if (spawnAlpha == false)
                {
                    NPC.alpha -= 12;
                }
                if (NPC.alpha < 0 && spawnAlpha == false)
                {
                    NPC.alpha = 0;
                    spawnAlpha = true;
                }
            }

            AAModGlobalNPC.Brain = NPC.whoAmI;

            bool Cthulhu = NPC.AnyNPCs(ModContent.NPCType<Cthulhu>());

            if (Cthulhu)
            {
                Music = 0;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.localAI[0] == 0f)
            {
                NPC.localAI[0] = 1f;
                for (int num761 = 0; num761 < EyeCount; num761++)
                {
                    float x = NPC.Center.X;
                    float y = NPC.Center.Y;
                    x += (float)Main.rand.Next(-NPC.width, NPC.width);
                    y += (float)Main.rand.Next(-NPC.height, NPC.height);
                    NPC npc = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)x, (int)y, ModContent.NPCType<EyeOfAzathoth>(), 0, num761);
                    npc.velocity = new Vector2((float)Main.rand.Next(-30, 31) * 0.1f, (float)Main.rand.Next(-30, 31) * 0.1f);
                    npc.netUpdate = true;
                }
            }
            //totalEyes = BaseAI.GetNPCs(NPC.Center, ModContent.NPCType<EyeOfAzathoth>(), 1500f);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.TargetClosest(true);
                int num765 = 6000;
                if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)num765)
                {
                    NPC.active = false;
                    NPC.life = 0;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NPC.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            bool phase2 = NPC.ai[0] < 0f;
            if (phase2)
            {
                if (NPC.localAI[2] == 0f)
                {
                    SoundEngine.PlaySound(SoundID.NPCHit1, NPC.position);
                    NPC.localAI[2] = 1f;
                    if (!Main.dedServ)
                    {
                        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DeityBrain1").Type, 1f);
                        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DeityBrain2").Type, 1f);
                    }
                    for (int num766 = 0; num766 < 20; num766++)
                    {
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.CthulhuDust>(), (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                    }
                    SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                }
                NPC.dontTakeDamage = false;
                NPC.knockBackResist = 0.5f;
                if (Main.expertMode)
                {
                    NPC.knockBackResist *= Main.GameModeInfo.KnockbackToEnemiesMultiplier;
                }
                NPC.TargetClosest(true);
                Vector2 vector94 = new Vector2(NPC.Center.X, NPC.Center.Y);
                float num767 = Main.player[NPC.target].Center.X - vector94.X;
                float num768 = Main.player[NPC.target].Center.Y - vector94.Y;
                float num769 = (float)Math.Sqrt((double)(num767 * num767 + num768 * num768));
                float num770 = 8f;
                num769 = num770 / num769;
                num767 *= num769;
                num768 *= num769;
                NPC.velocity.X = (NPC.velocity.X * 50f + num767) / 51f;
                NPC.velocity.Y = (NPC.velocity.Y * 50f + num768) / 51f;
                if (NPC.ai[0] == -1f)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.localAI[1] += 1f;
                        if (NPC.justHit)
                        {
                            NPC.localAI[1] -= (float)Main.rand.Next(5);
                        }
                        int num771 = 60 + Main.rand.Next(120);
                        if (Main.netMode != NetmodeID.SinglePlayer)
                        {
                            num771 += Main.rand.Next(30, 90);
                        }
                        if (NPC.localAI[1] >= (float)num771)
                        {
                            NPC.localAI[1] = 0f;
                            NPC.TargetClosest(true);
                            int num772 = 0;
                            int num773;
                            int num774;
                            while (true)
                            {
                                num772++;
                                num773 = (int)Main.player[NPC.target].Center.X / 16;
                                num774 = (int)Main.player[NPC.target].Center.Y / 16;
                                if (Main.rand.NextBool(2))
                                {
                                    num773 += Main.rand.Next(7, 13);
                                }
                                else
                                {
                                    num773 -= Main.rand.Next(7, 13);
                                }
                                if (Main.rand.NextBool(2))
                                {
                                    num774 += Main.rand.Next(7, 13);
                                }
                                else
                                {
                                    num774 -= Main.rand.Next(7, 13);
                                }
                                if (!WorldGen.SolidTile(num773, num774))
                                {
                                    break;
                                }
                                if (num772 > 100)
                                {
                                    goto Block_2789;
                                }
                            }
                            NPC.ai[3] = 0f;
                            NPC.ai[0] = -2f;
                            NPC.ai[1] = (float)num773;
                            NPC.ai[2] = (float)num774;
                            NPC.netUpdate = true;
                            NPC.netSpam = 0;
                            Block_2789:;
                        }
                    }
                }
                else if (NPC.ai[0] == -2f)
                {
                    NPC.velocity *= 0.9f;
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NPC.ai[3] += 15f;
                    }
                    else
                    {
                        NPC.ai[3] += 25f;
                    }
                    if (NPC.ai[3] >= 255f)
                    {
                        NPC.ai[3] = 255f;
                        NPC.position.X = NPC.ai[1] * 16f - (float)(NPC.width / 2);
                        NPC.position.Y = NPC.ai[2] * 16f - (float)(NPC.height / 2);
                        SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                        NPC.ai[0] = -3f;
                        NPC.netUpdate = true;
                        NPC.netSpam = 0;
                    }
                    NPC.alpha = (int)NPC.ai[3];
                }
                else if (NPC.ai[0] == -3f)
                {
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NPC.ai[3] -= 15f;
                    }
                    else
                    {
                        NPC.ai[3] -= 25f;
                    }
                    if (NPC.ai[3] <= 0f)
                    {
                        NPC.ai[3] = 0f;
                        NPC.ai[0] = -1f;
                        NPC.netUpdate = true;
                        NPC.netSpam = 0;
                    }
                    NPC.alpha = (int)NPC.ai[3];
                }
            }
            else
            {
                NPC.TargetClosest(true);
                Vector2 vector95 = new Vector2(NPC.Center.X, NPC.Center.Y);
                float num775 = Main.player[NPC.target].Center.X - vector95.X;
                float num776 = Main.player[NPC.target].Center.Y - vector95.Y;
                float num777 = (float)Math.Sqrt((double)(num775 * num775 + num776 * num776));
                float num778 = 1f;
                if (num777 < num778)
                {
                    NPC.velocity.X = num775;
                    NPC.velocity.Y = num776;
                }
                else
                {
                    num777 = num778 / num777;
                    NPC.velocity.X = num775 * num777;
                    NPC.velocity.Y = num776 * num777;
                }
                if (NPC.ai[0] == 0f)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int num779 = 0;
                        for (int num780 = 0; num780 < 200; num780++)
                        {
                            if (Main.npc[num780].active && Main.npc[num780].type == ModContent.NPCType<EyeOfAzathoth>())
                            {
                                num779++;
                            }
                        }
                        if (num779 == 0)
                        {
                            NPC.ai[0] = -1f;
                            NPC.localAI[1] = 0f;
                            NPC.alpha = 0;
                            NPC.netUpdate = true;
                        }
                        NPC.localAI[1] += 1f;
                        if (NPC.localAI[1] >= (float)(120 + Main.rand.Next(300)))
                        {
                            NPC.localAI[1] = 0f;
                            NPC.TargetClosest(true);
                            int num781 = 0;
                            int num782;
                            int num783;
                            while (true)
                            {
                                num781++;
                                num782 = (int)Main.player[NPC.target].Center.X / 16;
                                num783 = (int)Main.player[NPC.target].Center.Y / 16;
                                num782 += Main.rand.Next(-50, 51);
                                num783 += Main.rand.Next(-50, 51);
                                if (!WorldGen.SolidTile(num782, num783) && Collision.CanHit(new Vector2((float)(num782 * 16), (float)(num783 * 16)), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                                {
                                    break;
                                }
                                if (num781 > 100)
                                {
                                    goto Block_2806;
                                }
                            }
                            NPC.ai[0] = 1f;
                            NPC.ai[1] = (float)num782;
                            NPC.ai[2] = (float)num783;
                            NPC.netUpdate = true;
                            Block_2806:;
                        }
                    }
                }
                else if (NPC.ai[0] == 1f)
                {
                    NPC.alpha += 5;
                    if (NPC.alpha >= 255)
                    {
                        SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                        NPC.alpha = 255;
                        NPC.position.X = NPC.ai[1] * 16f - (float)(NPC.width / 2);
                        NPC.position.Y = NPC.ai[2] * 16f - (float)(NPC.height / 2);
                        NPC.ai[0] = 2f;
                    }
                }
                else if (NPC.ai[0] == 2f)
                {
                    NPC.alpha -= 5;
                    if (NPC.alpha <= 0)
                    {
                        NPC.alpha = 0;
                        NPC.ai[0] = 0f;
                    }
                }
            }
            if (Main.player[NPC.target].dead)
            {
                if (NPC.localAI[3] < 120f)
                {
                    NPC.localAI[3] += 1f;
                }
                if (NPC.localAI[3] > 60f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + (NPC.localAI[3] - 60f) * 0.25f;
                }
                NPC.ai[0] = 2f;
                NPC.alpha = 10;
                return;
            }
            if (NPC.localAI[3] > 0f)
            {
                NPC.localAI[3] -= 1f;
                return;
            }
        }


        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                //SoulOfCthulhu.ComeBack = true;
                int num121 = 0;
                while ((double)num121 < hit.Damage / (double)NPC.lifeMax * 3.0)
                {
                    if (Main.rand.NextBool(3))
                    {
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.CthulhuDust>(), hit.HitDirection, -1f, 0, Color.Transparent, 0.75f);
                    }
                    if (Main.rand.NextBool(2))
                    {
                        Dust dust39 = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                        dust39.noGravity = true;
                    }
                    for (int num122 = 0; num122 < NPC.oldPos.Length; num122++)
                    {
                        if (Main.rand.NextBool(4))
                        {
                            if (NPC.oldPos[num122] == Vector2.Zero)
                            {
                                break;
                            }
                            if (Main.rand.NextBool(3))
                            {
                                Dust.NewDust(NPC.oldPos[num122], NPC.width, NPC.height, ModContent.DustType<Dusts.CthulhuDust>(), hit.HitDirection, -1f, 0, Color.Transparent, 0.75f);
                            }
                            if (Main.rand.NextBool(2))
                            {
                                Dust dust40 = Main.dust[Dust.NewDust(NPC.oldPos[num122], NPC.width, NPC.height, ModContent.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                                dust40.noGravity = true;
                            }
                        }
                    }
                    num121++;
                }
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return;

            Player player = Main.player[NPC.target];
            if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
            {
                modifiers.FinalDamage /= 2;
                modifiers.DisableCrit();
            }
            if (projectile.penetrate == -1 && !projectile.minion && Main.player[projectile.owner].heldProj != projectile.whoAmI)
            {
                projectile.damage = (int)(projectile.damage * 0.2f);
            }
            else if (projectile.penetrate > 1)
            {
                projectile.damage = (int)(projectile.damage * 0.2f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D currentTex = TextureAssets.Npc[NPC.type].Value;
            Texture2D GlowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

            spriteBatch.Draw(currentTex, NPC.Center - screenPos, NPC.frame, NPC.IsABestiaryIconDummy ? Color.White : NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

            //draw glow/glow afterimage
            spriteBatch.Draw(GlowTex, NPC.Center - screenPos, NPC.frame, AAColor.Cthulhu2, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            BaseDrawing.DrawAfterimage(Main.spriteBatch, GlowTex, 0, NPC, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);

            return false;
        }
    }
}
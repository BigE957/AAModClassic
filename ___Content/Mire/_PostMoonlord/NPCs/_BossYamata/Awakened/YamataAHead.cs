using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria.ID;
using Terraria.Audio;
using AAModClassic.Music;

namespace AAModClassic.___Content.Mire._PostMoonlord.NPCs._BossYamata.Awakened
{
    [AutoloadBossHead]
    public class YamataAHead : ModNPC
    {
        public override void SetStaticDefaults()
        {
			base.SetStaticDefaults();
            // DisplayName.SetDefault("Yamata no Orochi");
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
            NPC.damage = (int)(NPC.damage * .8f);
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 570000;
            NPC.damage = 100;
            NPC.defense = 100;
            NPC.width = 78;
            NPC.height = 60;
            NPC.npcSlots = 0;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.DeathSound = Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/YamataRoar");
            Music = MusicManagementSystem.MusicSlots["Yamata_Awakened"];
            NPC.knockBackResist *= 0.05f;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public YamataA Body = null;

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadSingle();
                internalAI[1] = reader.ReadSingle();
                internalAI[2] = reader.ReadSingle();
                internalAI[3] = reader.ReadSingle();
            }
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return NPC.alpha == 0;
        }

        bool spawnHaruka = false;

        public override void AI()
        {
            if (Body == null)
            {
                NPC npcBody = Main.npc[(int)NPC.ai[0]];
                if (npcBody.type == ModContent.NPCType<YamataA>())
                {
                    Body = (YamataA)npcBody.ModNPC;
                }
            }
            if (Body == null)
                return;

            NPC.alpha = Body.NPC.alpha;


            if (!Body.NPC.active)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) //force a kill to prevent 'ghost hands'
                {
                    NPC.life = 0;
                    NPC.checkDead();
                    NPC.netUpdate = true;
                }
                return;
            }

            NPC.realLife = Body.NPC.whoAmI;
            NPC.timeLeft = 100;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            if (Yamata.TeleportMeBitch)
            {
                YamataA.TeleportMeBitch = false;
                NPC.Center = Body.NPC.Center;
                return;
            }

            Laugh();

            if (!player.active || player.dead || !Body.NPC.active)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead || !Body.NPC.active)
                {
                    if (NPC.timeLeft > 10)
                    {
                        NPC.timeLeft = 10;
                    }
                    return;
                }
            }

            NPC.rotation = 0;
            Vector2 nextTarget = new Vector2(Body.NPC.Center.X + NPC.ai[1], Body.NPC.Center.Y + NPC.ai[2]);
            float dist = Vector2.Distance(nextTarget, NPC.Center);
            if (dist < 100)
            {
                NPC.velocity *= 0.9f;
                if (Math.Abs(NPC.velocity.X) < 0.05f) NPC.velocity.X = 0f;
                if (Math.Abs(NPC.velocity.Y) < 0.05f) NPC.velocity.Y = 0f;
            }
            else
            {
                NPC.velocity = Vector2.Normalize(nextTarget - NPC.Center);
                NPC.velocity *= 10f;
            }
            //npc.position += Body.position - Body.oldPosition;

            switch ((int)internalAI[0])
            {
                case 0: //while other heads are charging
                    if (internalAI[3] == 0)
                    {
                        internalAI[3] = 1;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<YamataAHead_HarukaProj>(), NPC.damage / 4, 0f, Main.myPlayer, NPC.target);


                        if (NPC.life <= NPC.lifeMax / 2 && !spawnHaruka)
                        {
                            spawnHaruka = true;
                        }
                    }
                    /*if (++internalAI[2] > 60)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != 1)
                            // for future note if u wanna reenable this function, this proj would be "YamataAHead_AbyssalStorm"
                            Projectile.NewProjectile(npc.Center, Vector2.UnitY * 5, mod.ProjectileType("YamataAShockBomb"), npc.damage / 6, 0f, Main.myPlayer, npc.target);
                    }*/
                    if (++internalAI[1] > 180)
                    {
                        if (laughTimer <= 0)
                        {
                            laughTimer = 120;
                        }
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 1: //while other heads are shooting waveray
                    if (++internalAI[1] > 300)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 2: //shoot shit
                    if (++internalAI[2] > 20)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(Main.player[NPC.target].Center) * 5f, ModContent.ProjectileType<YamataAVenom2>(), NPC.damage / 6, 0f, Main.myPlayer);
                    }
                    if (++internalAI[1] > 240)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 3: //breathe lingering flame
                    if (++internalAI[2] > 60)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(Main.player[NPC.target].Center) * 7f, ModContent.ProjectileType<YamataAHead_AbyssalBomb>(), NPC.damage / 6, 0f, Main.myPlayer);
                    }
                    if (++internalAI[1] > 180)
                    {
                        if (laughTimer <= 0)
                        {
                            laughTimer = 120;
                        }
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 4: //shoot direct aim deathrays
                    if (internalAI[1] == NPC.ai[3] * 60 - 30)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(Main.player[NPC.target].Center), ModContent.ProjectileType<YamataAHead_MireDeathraySmall>(), NPC.damage / 4, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                    }
                    if (++internalAI[1] > 360)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 5: //shoot the shit again
                    goto case 2;

                case 6: //drop meteor that creates ripples across ground
                    if (++internalAI[2] > 90)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            for (int i = -1; i <= 1; i++)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(Main.player[NPC.target].Center).RotatedBy(MathHelper.ToRadians(i * 5)) * 5f, ModContent.ProjectileType<YamataAVenom2>(), NPC.damage / 6, 0f, Main.myPlayer);
                    }
                    if (++internalAI[1] > 420)
                    {
                        if (laughTimer <= 0)
                        {
                            laughTimer = 120;
                        }
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 7: //pause, let previous waves disperse
                    if (++internalAI[1] > 120)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 8: //breathe the lingering shit
                    goto case 3;

                case 9: //some mix of 2 attacks he already does, something homing + something directly aimed
                    if (--internalAI[2] < 0)
                    {
                        internalAI[2] = 120;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(Main.player[NPC.target].Center) * 7f, ModContent.ProjectileType<YamataAHead_AbyssalBomb>(), NPC.damage / 6, 0f, Main.myPlayer);
                    }
                    if (++internalAI[1] > 360)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 10: //shoot the shit again
                    goto case 2;

                default:
                    internalAI[0] = 0;
                    NPC.netUpdate = true;
                    goto case 0;
            }
            
            if (YamataA.TeleportMeBitch)
            {
                YamataA.TeleportMeBitch = false;
                NPC.Center = Body.NPC.Center;
                return;
            }
        }

        int laughTimer = 0;
        bool Laughing = false;

        public void Laugh()
        {
            if (laughTimer > 0 && !Laughing)
            {
                CombatText.NewText(NPC.getRect(), new Color(146, 30, 68), "NYEH", true, true);
                Laughing = true;
            }
            else if (laughTimer <= 0)
            {
                Laughing = false;
            }
            if (Laughing)
            {
                laughTimer--;
                if (laughTimer % 20 == 0 && laughTimer != 120)
                {
                    CombatText.NewText(NPC.getRect(), new Color(146, 30, 68), "HEH", true, true);
                }
            }
        }
    }
}

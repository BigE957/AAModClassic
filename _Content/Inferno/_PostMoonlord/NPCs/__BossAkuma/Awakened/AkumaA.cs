using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Boss.Akuma;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened
{
    [AutoloadBossHead]
    public class AkumaA : ModNPC
    {
        public bool Loludided;
        public int fireTimer = 0;
        public int damage = 0;
        private bool weakness;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oni Akuma");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.noTileCollide = true;
            NPC.width = 80;
            NPC.height = 80;
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.damage = 150;
            NPC.defense = 90;
            NPC.lifeMax = 500000;
            NPC.value = Item.sellPrice(0, 40, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("Sounds/AkumaRoar");
            Music = MusicManagementSystem.MusicSlots["Akuma_Awakened"];
            SceneEffectPriority = SceneEffectPriority.BossHigh;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.buffImmune[103] = false;
            NPC.alpha = 255;
            SceneEffectPriority = SceneEffectPriority.BossHigh;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }


        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossLoot(ref int potionType)
        {
            if (Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;
            }
            else
            {
                potionType = 0;
            }
        }
        public static int MinionCount = 0;

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
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
                internalAI[1] = reader.ReadSingle();
                internalAI[2] = reader.ReadSingle();
                internalAI[3] = reader.ReadSingle();
            }
        }
        public Texture2D AkumaTex = null;

        public bool spawnAshe = false;

        public override bool PreAI()
        {
            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

            Player player = Main.player[NPC.target];
            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }

            NPC.frameCounter++;
            if (NPC.frameCounter > 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 146;
            }
            if (NPC.frame.Y > 146 * 2)
            {
                NPC.frame.Y = 0;
            }

            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.localAI[2] == 0)
                {
                    NPC.realLife = NPC.whoAmI;
                    int latestNPC = NPC.whoAmI;
                    int[] Frame = { 1, 2, 0, 1, 2, 2, 1, 2, 2, 0, 1, 2, 2, 1, 2, 2, 0, 1, 2, 3, 4};
                    for (int i = 0; i < Frame.Length; ++i)
                    {
                        latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AkumaABody>(), NPC.whoAmI, 0, latestNPC);
                        Main.npc[latestNPC].realLife = NPC.whoAmI;
                        Main.npc[latestNPC].ai[3] = NPC.whoAmI;
                        Main.npc[latestNPC].netUpdate = true;
                        Main.npc[latestNPC].ai[2] = Frame[i];
                    }
                    NPC.localAI[2] = 1;
                    NPC.netUpdate2 = true;
                }
            }

            bool collision = true;

            Vector2 targetPos;
            switch ((int)NPC.ai[0])
            {
                case 0: //chase while breathing fire, original code
                    if (!NPC.HasPlayerTarget)
                        NPC.TargetClosest(true);
                    targetPos = Main.player[NPC.target].Center;
                    MovementWorm(targetPos, 15f, 0.13f); //original movement
                    SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                    AAAI.BreatheFire(NPC, true, ModContent.ProjectileType<AkumaABreath>(), 2, 4);
                    if (NPC.HasBuff(BuffID.Wet))
                    {
                        fireTimer++;

                        if (fireTimer % 20 == 0)
                        {
                            for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                            {
                                int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.MireBubbleDust>(), 0f, 0f, 90, default, 2f);
                                Main.dust[num935].noGravity = true;
                                Main.dust[num935].velocity.Y -= 1f;
                            }
                            if (weakness == false)
                            {
                                weakness = true;
                                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma1"), Color.DeepSkyBlue);
                            }
                        }
                    }
                    else
                    {
                        AAAI.BreatheFire(NPC, true, ModContent.ProjectileType<AkumaBreath>(), 2, 4);
                    }
                    if (++NPC.ai[1] > 240)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 1: //chase harder, shoot fragballs
                    targetPos = player.Center;
                    MovementWorm(targetPos, 16f, 0.26f);
                    if (++NPC.ai[2] > 60)
                    {
                        NPC.ai[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 20f * Vector2.Normalize(NPC.velocity), ModContent.ProjectileType<AkumaAFireballFrag>(), NPC.damage / 4, 0f, Main.myPlayer);
                    }
                    if (++NPC.ai[1] > 300)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 2: //fly up for overhead meteor rain dash
                    targetPos = player.Center;
                    targetPos.X += 800 * (NPC.Center.X < player.Center.X ? -1 : 1);
                    targetPos.Y -= 400;
                    MovementWorm(targetPos, 20f, 0.6f);
                    if (++NPC.ai[1] > 240 || NPC.Distance(targetPos) < 100) //initiate dash
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = NPC.Center.X < player.Center.X ? 1 : -1; //remember which side to end up on
                        NPC.velocity = 20f * Vector2.UnitX * NPC.ai[2];
                        NPC.velocity.Y /= 5f;
                        NPC.netUpdate = true;
                    }
                    break;

                case 3: //meteor rain
                    targetPos = new Vector2(player.Center.X + NPC.ai[2] * 1000, NPC.Center.Y);
                    MovementWorm(targetPos, 30f, 0.26f); //accelerate horizontally
                    if (++NPC.ai[3] > 40)
                    {
                        NPC.ai[3] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            bool fire = true;
                            for (int i = 0; i < Main.maxNPCs; i++)
                                if (Main.npc[i].active && Main.npc[i].realLife == NPC.whoAmI)
                                {
                                    fire = !fire;
                                    if (fire)
                                    {
                                        Vector2 vel = 4f * Vector2.UnitY;
                                        vel.X += Main.rand.NextFloat(-1f, 1f);
                                        vel.Y += Main.rand.NextFloat(-1f, 1f);
                                        Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center, vel, ModContent.ProjectileType<AkumaRock>(), Main.npc[i].damage / 4, 0f, Main.myPlayer);
                                    }
                                }
                        }
                    }
                    if (++NPC.ai[1] > 240 || (NPC.ai[2] > 0 ? NPC.Center.X > player.Center.X + 700 : NPC.Center.X < player.Center.X - 700))
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            bool fire = true;
                            for (int i = 0; i < Main.maxNPCs; i++)
                                if (Main.npc[i].active && Main.npc[i].realLife == NPC.whoAmI)
                                {
                                    fire = !fire;
                                    if (fire)
                                    {
                                        Vector2 vel = 4f * Vector2.UnitY;
                                        vel.X += Main.rand.NextFloat(-1f, 1f);
                                        vel.Y += Main.rand.NextFloat(-1f, 1f);
                                        Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center, vel, ModContent.ProjectileType<AkumaRock>(), Main.npc[i].damage / 4, 0f, Main.myPlayer);
                                    }
                                }
                        }
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        NPC.netUpdate = true;
                        NPC.velocity.Normalize();
                        NPC.velocity *= 15f;
                        NPC.velocity = NPC.velocity.RotatedBy(NPC.velocity.X > 0 ? Math.PI / 2 : -Math.PI / 2);
                    }
                    break;

                case 4: //turn around, chase player for a bit
                    targetPos = player.Center;
                    MovementWorm(targetPos, 15f, 0.13f);
                    if (++NPC.ai[1] > 120)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                        if (Main.netMode != NetmodeID.MultiplayerClient) //fire deathray
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity), ModContent.ProjectileType<AkumaADeathraySmall>(), NPC.damage / 4, 0f, Main.myPlayer, 0, NPC.whoAmI);
                    }
                    break;

                case 5: //currently firing deathray, weaker acceleration
                    targetPos = player.Center;
                    MovementWorm(targetPos, 15f, 0.08f);
                    if (++NPC.ai[1] > 240)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 6: //fire lasers from all segments, slower now
                    targetPos = player.Center;
                    MovementWorm(targetPos, 10f, 0.26f);
                    if (NPC.ai[1] == 120 - 60 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        bool fire = true;
                        for (int i = 0; i < Main.maxNPCs; i++)
                            if (Main.npc[i].active && Main.npc[i].realLife == NPC.whoAmI)
                            {
                                fire = !fire;
                                if (fire)
                                {
                                    Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center, Main.npc[i].rotation.ToRotationVector2(), ModContent.ProjectileType<AkumaADeathraySmall>(), Main.npc[i].damage / 4, 0f, Main.myPlayer, (float)Math.PI / 2, Main.npc[i].whoAmI);
                                    Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center, (Main.npc[i].rotation + (float)Math.PI).ToRotationVector2(), ModContent.ProjectileType<AkumaADeathraySmall>(), Main.npc[i].damage / 4, 0f, Main.myPlayer, (float)-Math.PI / 2, Main.npc[i].whoAmI);
                                }
                            }
                    }
                    if (++NPC.ai[2] > 140)
                    {
                        NPC.ai[2] = 0;
                    }
                    if (++NPC.ai[1] > 120 + 180)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 7: //go under and prepare for dash
                    targetPos = player.Center;
                    targetPos.X += 700 * (NPC.Center.X < player.Center.X ? -1 : 1);
                    targetPos.Y += 400;
                    MovementWorm(targetPos, 20f, 0.6f);
                    if (++NPC.ai[1] > 240 || NPC.Distance(targetPos) < 100) //initiate dash
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = NPC.Center.X < player.Center.X ? 1 : -1; //remember which side to end up on
                        NPC.velocity.X = 25f * NPC.ai[2];
                        NPC.velocity.Y /= 5f;
                        NPC.netUpdate = true;
                    }
                    break;

                case 8: //wait till past player
                    if (++NPC.ai[1] > 240 || (NPC.ai[2] > 0 ? NPC.Center.X > player.Center.X : NPC.Center.X < player.Center.X))
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 9: //eruption
                    NPC.velocity *= 0.9875f;
                    if (++NPC.ai[2] == 30)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            bool fire = true;
                            for (int i = 0; i < Main.maxNPCs; i++)
                                if (Main.npc[i].active && Main.npc[i].realLife == NPC.whoAmI)
                                {
                                    fire = !fire;
                                    if (fire)
                                    {
                                        Vector2 vel = -5f * Vector2.UnitY;
                                        vel.X += Main.rand.NextFloat(-1f, 1f);
                                        vel.Y += Main.rand.NextFloat(-.5f, .5f);
                                        vel *= 1.5f;
                                        Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center, vel, ModContent.ProjectileType<AkumaAMeteor>(), Main.npc[i].damage / 4, 0f, Main.myPlayer, 0f, 1f);
                                    }
                                }
                        }
                    }
                    if (++NPC.ai[1] > 120)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            bool fire = true;
                            for (int i = 0; i < Main.maxNPCs; i++)
                                if (Main.npc[i].active && Main.npc[i].realLife == NPC.whoAmI)
                                {
                                    fire = !fire;
                                    if (fire)
                                    {
                                        Vector2 vel = -5f * Vector2.UnitY;
                                        vel.X += Main.rand.NextFloat(-1f, 1f);
                                        vel.Y += Main.rand.NextFloat(-.5f, .5f);
                                        vel *= 1.5f;
                                        Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center, vel, ModContent.ProjectileType<AkumaAMeteor>(), Main.npc[i].damage / 4, 0f, Main.myPlayer, 0f, 1f);
                                    }
                                }
                        }
                    }
                    break;

                case 10: //lakitu and chase player
                    targetPos = player.Center;
                    MovementWorm(targetPos, 17f, 0.3f);
                    if (NPC.ai[2] == 0)
                    {
                        NPC.ai[2] = 1;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<AsheA>(), NPC.damage / 4, 0f, Main.myPlayer, NPC.target); 
                        if (!spawnAshe)
                        {
                            spawnAshe = true;
                            if (AAWorld.downedAkuma)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Awakened.AsheAppear.Akuma"), Color.DeepSkyBlue);
                                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Awakened.AsheAppear.Ashe"), new Color(102, 20, 48));
                            }
                            else
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Awakened.AsheAppear.First.Ashe"), new Color(102, 20, 48));
                                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Awakened.AsheAppear.First.Akuma"), Color.DeepSkyBlue);
                            }
                        }
                    }
                    if (++NPC.ai[1] > 300)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                default:
                    NPC.ai[0] = 0;
                    goto case 0;
            }

            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = 1;

            }
            else
            {
                NPC.spriteDirection = -1;
            }

            if (!Main.dayTime)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Awakened.DayReset"), Color.DeepSkyBlue);
                Main.dayTime = true;
                Main.time = 0;
            }

            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                if (Loludided == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Awakened.Kill"), new Color(180, 41, 32));
                    Loludided = true;
                }
                NPC.velocity.Y = NPC.velocity.Y + 1f;
                if (NPC.position.Y > Main.rockLayer * 16.0)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 1f;
                }
                if (NPC.position.Y > Main.rockLayer * 16.0)
                {
                    for (int num957 = 0; num957 < 200; num957++)
                    {
                        if (Main.npc[num957].aiStyle == NPC.aiStyle)
                        {
                            Main.npc[num957].active = false;
                        }
                    }
                }
            }

            if (collision)
            {
                if (NPC.localAI[0] != 1)
                    NPC.netUpdate = true;
                NPC.localAI[0] = 1f;
            }
            if ((NPC.velocity.X > 0.0 && NPC.oldVelocity.X < 0.0 || NPC.velocity.X < 0.0 && NPC.oldVelocity.X > 0.0 || NPC.velocity.Y > 0.0 && NPC.oldVelocity.Y < 0.0 || NPC.velocity.Y < 0.0 && NPC.oldVelocity.Y > 0.0) && !NPC.justHit)
                NPC.netUpdate = true;

            return false;
        }

        public void MovementWorm(Vector2 target, float speed, float acceleration)
        {
            Vector2 npcCenter = NPC.Center;// new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            //float targetXPos = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            //float targetYPos = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);

            float targetRoundedPosX = target.X;// (int)(targetXPos / 16.0) * 16;
            float targetRoundedPosY = target.Y;// (int)(targetYPos / 16.0) * 16;
            //npcCenter.X = (int)(npcCenter.X / 16.0) * 16;
            //npcCenter.Y = (int)(npcCenter.Y / 16.0) * 16;
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;
            NPC.TargetClosest(true);
            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

            float absDirX = Math.Abs(dirX);
            float absDirY = Math.Abs(dirY);
            float newSpeed = speed / length;
            dirX *= newSpeed;
            dirY *= newSpeed;
            if (NPC.velocity.X > 0.0 && dirX > 0.0 || NPC.velocity.X < 0.0 && dirX < 0.0 || NPC.velocity.Y > 0.0 && dirY > 0.0 || NPC.velocity.Y < 0.0 && dirY < 0.0)
            {
                if (NPC.velocity.X < dirX)
                    NPC.velocity.X = NPC.velocity.X + acceleration;
                else if (NPC.velocity.X > dirX)
                    NPC.velocity.X = NPC.velocity.X - acceleration;
                if (NPC.velocity.Y < dirY)
                    NPC.velocity.Y = NPC.velocity.Y + acceleration;
                else if (NPC.velocity.Y > dirY)
                    NPC.velocity.Y = NPC.velocity.Y - acceleration;
                if (Math.Abs(dirY) < speed * 0.2 && (NPC.velocity.X > 0.0 && dirX < 0.0 || NPC.velocity.X < 0.0 && dirX > 0.0))
                {
                    if (NPC.velocity.Y > 0.0)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration * 2f;
                    else
                        NPC.velocity.Y = NPC.velocity.Y - acceleration * 2f;
                }
                if (Math.Abs(dirX) < speed * 0.2 && (NPC.velocity.Y > 0.0 && dirY < 0.0 || NPC.velocity.Y < 0.0 && dirY > 0.0))
                {
                    if (NPC.velocity.X > 0.0)
                        NPC.velocity.X = NPC.velocity.X + acceleration * 2f;
                    else
                        NPC.velocity.X = NPC.velocity.X - acceleration * 2f;
                }
            }
            else if (absDirX > absDirY)
            {
                if (NPC.velocity.X < dirX)
                    NPC.velocity.X = NPC.velocity.X + acceleration * 1.1f;
                else if (NPC.velocity.X > dirX)
                    NPC.velocity.X = NPC.velocity.X - acceleration * 1.1f;

                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                {
                    if (NPC.velocity.Y > 0.0)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration;
                    else
                        NPC.velocity.Y = NPC.velocity.Y - acceleration;
                }
            }
            else
            {
                if (NPC.velocity.Y < dirY)
                    NPC.velocity.Y = NPC.velocity.Y + acceleration * 1.1f;
                else if (NPC.velocity.Y > dirY)
                    NPC.velocity.Y = NPC.velocity.Y - acceleration * 1.1f;

                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                {
                    if (NPC.velocity.X > 0.0)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                    else
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                }
            }
        }

        public override void OnKill()
        {
            if (Main.expertMode)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    ChatUtils.Chat(NPCExtensions.BeenKilled<AkumaA>(true) ? Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Awakened.Defeat.Repeat") : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Awakened.Defeat.First"), Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);

            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Awakened.Defeat.Cheat"), Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AkumaBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AkumaATrophy>(), 10));

            LeadingConditionRule firstKill = new(new FirstTimeKillingAkumaA());

            firstKill.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DraconianSunRune>()));

            LeadingConditionRule shenDefeated = new(new ShenDefeated());

            shenDefeated.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EXSoul>(), 50));

            npcLoot.Add(firstKill);
            npcLoot.Add(shenDefeated);
        }

        public class FirstTimeKillingAkumaA : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !NPCExtensions.BeenKilled<AkumaA>(true);
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }

        //TODO: Organize the various conditions lying around

        public class ShenDefeated : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => AAWorld.downedShen;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }

        public bool Quote1;
        public bool Quote2;
        public bool Quote3;
        public bool Quote4;
        public bool Quote5;
        public bool QuoteSaid;

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.penetrate > 1)
            {
                damage = (int)(damage * .5f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            AkumaTex = TextureAssets.Npc[NPC.type].Value;
            if (NPC.type == ModContent.NPCType<AkumaA>())
            {
                if (NPC.ai[0] == 0 || NPC.ai[0] == 1 || NPC.ai[0] == 5 || NPC.ai[0] == 9)
                {
                    AkumaTex = ModContent.Request<Texture2D>("_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Awakened/AkumaA1").Value;
                }
                else
                {
                    AkumaTex = ModContent.Request<Texture2D>("_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Awakened/AkumaA").Value;
                }
            }

            Texture2D glowTex = ModContent.Request<Texture2D>("Glowmasks/AkumaA_Glow").Value;
            Texture2D glowTex1 = ModContent.Request<Texture2D>("Glowmasks/AkumaA1_Glow").Value;
            Texture2D glowTex2 = ModContent.Request<Texture2D>("Glowmasks/AkumaABody_Glow").Value;
            
            int shader;
            if (NPC.ai[1] == 1 || NPC.ai[2] >= 470 || Main.npc[(int)NPC.ai[3]].ai[1] == 1 || Main.npc[(int)NPC.ai[3]].ai[2] >= 500)
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            }
            else
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            }

            Texture2D HeadGlow = (NPC.ai[0] == 0 || NPC.ai[0] == 4) ? glowTex1 : glowTex;

            Texture2D myGlowTex = NPC.type == ModContent.NPCType<AkumaA>() ? HeadGlow : glowTex2;
            BaseDrawing.DrawTexture(spriteBatch, AkumaTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, 3, NPC.frame, NPC.GetAlpha(drawColor), true);
            BaseDrawing.DrawTexture(spriteBatch, myGlowTex, shader, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, 3, NPC.frame, NPC.GetAlpha(Color.White), true);
            return false;
        }


        public override void HitEffect(NPC.HitInfo hit)
        {
            int dust1 = ModContent.DustType<Dusts.AkumaADust>();
            int dust2 = ModContent.DustType<Dusts.AkumaDust>();
            if (NPC.life <= 0)
            {
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }


        public int roarTimer = 0;
        public int roarTimerMax = 120;
        public bool Roaring
        {
            get
            {
                return roarTimer > 0;
            }
        }

        public void Roar(int timer, bool fireSound)
        {
            roarTimer = timer;
            if (fireSound)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath60, NPC.Center);
            }
            else
            {
                SoundEngine.PlaySound(new SoundStyle("Sounds/AkumaRoar"), NPC.Center);
            }
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
            {
                return false;
            }
            return true;
        }
    }

    [AutoloadBossHead]
    public class AkumaABody : AkumaA
    {
        public override string Texture => "AAModClassic/_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Awakened/AkumaABody";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oni Akuma");
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.boss = false;
            NPC.width = 40;
            NPC.height = 40;
            NPC.dontCountMe = true;
            NPC.chaseable = false;
        }

        public override bool PreAI()
        {
            Vector2 chasePosition = Main.npc[(int)NPC.ai[1]].Center;
            Vector2 directionVector = chasePosition - NPC.Center;
            NPC.spriteDirection = (directionVector.X > 0f) ? 1 : -1;
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }


            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active || Main.npc[(int)NPC.ai[3]].type != ModContent.NPCType<AkumaA>())
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, NPC.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - npcCenter.Y;
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    NPC.spriteDirection = 1;

                }
                else
                {
                    NPC.spriteDirection = -1;
                }

                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
            }

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.netUpdate = true;
            return false;
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.TargetDamageMultiplier *= .1f;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameHeight * (int)NPC.ai[2];
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
            {
                return false;
            }
            NPC.active = false;
            return true;
        }
    }
}

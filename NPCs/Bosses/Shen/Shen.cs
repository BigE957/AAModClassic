
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.NPCs.Bosses.Shen.Projectiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Potions;
using AAModClassic.Buffs;
using AAModClassic.Globals;
using Terraria.Localization;
using AAModClassic.UI.Titles;
using AAModClassic.NPCs.Bosses.Shen.AwakenedShenAH;
using AAModClassic.NPCs.Bosses.Shen.GripsShen;
using AAModClassic.Items.Boss.Shen;
using AAModClassic.Music;
using AAModClassic.Utilities;

namespace AAModClassic.NPCs.Bosses.Shen
{
    [AutoloadBossHead]
    public class Shen : ModNPC
    {
        public int damage = 0;

        public bool SpawnGrips = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shen Doragon; Discordian Doomsayer");
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.noTileCollide = true;
            NPC.height = 100;
            NPC.width = 444;
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.knockBackResist = 0f;
            NPC.damage = 120;
            NPC.defense = 70;
            NPC.lifeMax = 800000;
            NPC.value = Item.sellPrice(20, 0, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/ShenRoar");
            Music = MusicManagementSystem.MusicSlots["Shen"];
            SceneEffectPriority = (SceneEffectPriority)11;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.buffImmune[ModContent.BuffType<Terrablaze_Buff>()] = false;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
            NPC.damage = (int)(NPC.damage * .8f);
        }

        public bool Weakness = false;
        public bool isAwakened = false;
        public float _normalSpeed = 15f;
        public float _chargeSpeed = 40f;
        public float MoveSpeed
        {
            get
            {
                float playerRunAcceleration = 1f;
                if (Main.player[NPC.target].active && !Main.player[NPC.target].dead) //if you have a target, speed up to keep up
                {
                    playerRunAcceleration = Math.Max(Math.Abs(Main.player[NPC.target].moveSpeed), Main.player[NPC.target].runAcceleration);
                    if (playerRunAcceleration <= 1f) playerRunAcceleration = 1f;
                }
                if (Dashing)
                {
                    return _chargeSpeed * playerRunAcceleration;
                }
                else
                {
                    return _normalSpeed * playerRunAcceleration;
                }
            }
        }

        //clientside stuff
        public Rectangle wingFrame = new Rectangle(0, 0, 444, 400); //the wing frame.
        public int wingFrameY = 400; //the frame height for the wings.
        public int frameY = 400; //the frame height for the body.
        public int roarTimer = 0; //if this is > 0, then use the roaring frame.
        public int roarTimerMax = 120; //default roar timer. only changed for fire breath as it's longer.
        public bool Roaring => roarTimer > 0; //wether or not he is roaring. only used clientside for frame visuals.

        public int chargeWidth = 50;
        public int normalWidth = 444;

        public override void BossLoot(ref int potionType)
        {
            if (Main.expertMode && !isAwakened)
            {
                potionType = 0;
                return;
            }
            potionType = ModContent.ItemType<GrandHealingPotion>();
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
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
                SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/ShenRoar"), NPC.Center);
            }
        }

        public int Side;
        public bool Health4 = false;
        public bool Health3 = false;
        public bool Health2 = false;
        public bool Health1 = false;

        public float[] FleeTimer = new float[1];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(FleeTimer[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                FleeTimer[0] = reader.ReadSingle();
            }
        }

        public override void AI()
        {
            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            Vector2 targetPos;

            Main.dayTime = false;
            Main.time = 18000;

            if (!AliveCheck(player)) return;

            #region ProjIDs

            int AccelR = ModContent.ProjectileType<FireballAccelR>();
            int AccelB = ModContent.ProjectileType<FireballAccelB>();

            int FragR = ModContent.ProjectileType<FireballFragR>();
            int FragB = ModContent.ProjectileType<FireballFragB>();

            int HomingR = ModContent.ProjectileType<FireballHomingR>();
            int HomingB = ModContent.ProjectileType<FireballHomingB>();

            int SpreadR = ModContent.ProjectileType<FireballSpreadR>();
            int SpreadB = ModContent.ProjectileType<FireballSpreadB>();

            int Accel = NPC.spriteDirection == 1 ? AccelR : AccelB;
            int Homing = NPC.spriteDirection == 1 ? HomingR : HomingB;
            int Spread = NPC.spriteDirection == 1 ? SpreadR : SpreadB;
            int Frag = NPC.spriteDirection == 1 ? FragR : FragB;
            int Inferno = ModContent.ProjectileType<DiscordianInferno>();

            #endregion

            Dashing = false;
            if (Roaring) roarTimer--;

            if (Dashing)
            {
                if (NPC.width != chargeWidth)
                {
                    Vector2 center = NPC.Center;
                    NPC.width = chargeWidth;
                    NPC.Center = center;
                    NPC.netUpdate = true;
                }
            }
            else
            if (NPC.width != normalWidth)
            {
                Vector2 center = NPC.Center;
                NPC.width = normalWidth;
                NPC.Center = center;
                NPC.netUpdate = true;
            }

            if (!NPC.AnyNPCs(ModContent.NPCType<ShenHitbox>()))
            {
                int hitbox = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ShenHitbox>(), 0, NPC.whoAmI, 0f, 0f, 0f, 255);
                Main.npc[hitbox].netUpdate = true;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<GripsShen.BlazeGrip>()) || NPC.AnyNPCs(ModContent.NPCType<GripsShen.AbyssGrip>()))
            {
                if (NPC.alpha > 50)
                {
                    NPC.alpha = 50;
                }
                else
                {
                    NPC.alpha += 4;
                }
                NPC.dontTakeDamage = true;
            }
            else
            {
                if (NPC.alpha > 0)
                {
                    for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                    {
                        int dust = spawnDust == 1 ? ModContent.DustType<Dusts.AkumaADust>() : ModContent.DustType<Dusts.YamataADust>();
                        if (Main.rand.Next(4) == 0) dust = ModContent.DustType<Dusts.Discord_Dust>();
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust, 0f, 0f, 100, default, 2f);
                        Main.dust[num935].noGravity = true;
                        Main.dust[num935].noLight = true;
                    }
                    NPC.alpha -= 4;
                }
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
                NPC.dontTakeDamage = false;
            }

            if (player.dead || !player.active || Vector2.Distance(NPC.Center, player.Center) > 10000)
            {
                NPC.TargetClosest();

                if (player.dead || !player.active || Vector2.Distance(NPC.Center, player.Center) > 10000)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && FleeTimer[0]++ >= 120)
                    {
                        if (FleeTimer[0] < 130)
                        {
                            NPC.velocity.Y += 1f;
                            NPC.netUpdate = true;
                        }
                        else if (FleeTimer[0] == 130)
                        {
                            NPC.velocity.Y = -6f;
                            NPC.netUpdate = true;
                        }
                        else if (FleeTimer[0] > 130)
                        {
                            NPC.velocity.Y = -6f;
                        }
                        if (NPC.position.Y + NPC.velocity.Y <= 0f && Main.netMode != NetmodeID.MultiplayerClient) { BaseAI.KillNPC(NPC); NPC.netUpdate = true; }
                    }
                }
                else
                {
                    FleeTimer[0] = 0;
                }
            }

            switch ((int)NPC.ai[0])
            {
                case 0: //target for first time, navigate beside player
                    if (!NPC.HasPlayerTarget)
                        NPC.TargetClosest();
                    if (!AliveCheck(Main.player[NPC.target]))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 600 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    Movement(targetPos, 1f);
                    if (++NPC.ai[2] > 240)
                    {
                        Roar(roarTimerMax, false);
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = NPC.Center.X < player.Center.X ? 0 : (float)Math.PI;
                        NPC.netUpdate = true;
                        NPC.velocity.X = 2 * (NPC.Center.X < player.Center.X ? -1 : 1);
                        NPC.velocity.Y *= 0.2f;
                    }
                    if (++NPC.ai[1] > 60)
                    {
                        NPC.ai[1] = 0;
                        Roar(roarTimerMax, false);
                        NPC.netUpdate = true;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            for (int i = -2; i <= 2; i++)
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 30 * Vector2.UnitX.RotatedBy(Math.PI / 4 * i) * (NPC.Center.X < player.Center.X ? -1 : 1), Spread, NPC.damage / 4, 0f, Main.myPlayer, 20, 20 + 60);
                    }
                    break;

                case 1: //Fire Breath
                    BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<ShenABreath>(), ref NPC.ai[2], 5, NPC.damage / 2, 13, false, new Vector2(167 * NPC.direction, 0));
                    if (++NPC.ai[1] > 120)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 2: //fly to corner for dash
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 800 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y -= 800;
                    Movement(targetPos, 1.2f);
                    if (++NPC.ai[1] > 180 || Math.Abs(NPC.Center.Y - targetPos.Y) < 100) //initiate dash
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                        NPC.velocity = NPC.DirectionTo(player.Center) * 45;
                    }
                    NPC.rotation = 0;
                    break;

                case 3: //dashing
                    if (NPC.Center.Y > player.Center.Y + 700 || Math.Abs(NPC.Center.X - player.Center.X) > 1500)
                    {
                        NPC.velocity.Y *= 0.5f;
                        NPC.ai[1] = 0;
                        if (++NPC.ai[2] >= 3) //repeat three times
                        {
                            NPC.ai[0]++;
                            NPC.ai[2] = 0;
                        }
                        else
                            NPC.ai[0]--;
                        NPC.netUpdate = true;
                    }
                    Dashing = true;
                    NPC.rotation = NPC.velocity.ToRotation();
                    if (NPC.velocity.X < 0)
                        NPC.rotation += (float)Math.PI;
                    break;

                case 4: //prepare for queen bee dashes
                    if (!AliveCheck(player))
                        break;
                    if (++NPC.ai[1] > 30)
                    {
                        targetPos = player.Center;
                        targetPos.X += 1000 * (NPC.Center.X < targetPos.X ? -1 : 1);
                        Movement(targetPos, 0.8f);
                        if (NPC.ai[1] > 180 || Math.Abs(NPC.Center.Y - targetPos.Y) < 50) //initiate dash
                        {
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.netUpdate = true;
                            NPC.velocity.X = -40 * (NPC.Center.X < player.Center.X ? -1 : 1);
                            NPC.velocity.Y *= 0.1f;
                        }
                    }
                    else
                    {
                        NPC.velocity *= 0.9f; //decelerate briefly
                    }
                    NPC.rotation = 0;
                    break;

                case 5: //dashing
                    if (NPC.ai[3] == 0 && --NPC.ai[2] < 0)
                    {
                        NPC.ai[2] = 4;
                        Roar(roarTimerMax, false);
                    }
                    if (++NPC.ai[1] > 240 || (Math.Sign(NPC.velocity.X) > 0 ? NPC.Center.X > player.Center.X + 900 : NPC.Center.X < player.Center.X - 900))
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        if (++NPC.ai[3] >= 3) //repeat dash three times
                        {
                            NPC.ai[0]++;
                            NPC.ai[3] = 0;
                        }
                        else
                            NPC.ai[0]--;
                        NPC.netUpdate = true;
                    }
                    Dashing = true;
                    break;

                case 6: //fly at player, spit mega balls
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 700 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    Movement(targetPos, .8f);
                    if (++NPC.ai[2] > 80)
                    {
                        NPC.ai[2] = 0;
                        Roar(roarTimerMax, false);
                        NPC.netUpdate = true;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Vector2 spawnPos = NPC.Center;
                            spawnPos.X += 250 * (NPC.Center.X < player.Center.X ? 1 : -1);
                            spawnPos.Y -= 25;
                            Vector2 vel = (player.Center - spawnPos) / 30;
                            if (vel.Length() < 25)
                                vel = Vector2.Normalize(vel) * 25;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, Frag, NPC.damage / 4, 0f, Main.myPlayer);
                        }
                    }
                    if (++NPC.ai[1] > 210)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 7: goto case 2;
                case 8: goto case 3;

                case 9: //prepare for fishron dash
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center + player.DirectionTo(NPC.Center) * 600;
                    Movement(targetPos, 0.8f);
                    if (++NPC.ai[1] > 20)
                    {
                        Roar(roarTimerMax, false);
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                        NPC.velocity = NPC.DirectionTo(player.Center) * 40;
                    }
                    NPC.rotation = 0;
                    break;

                case 10: //dashing
                    if (++NPC.ai[2] > 3)
                    {
                        NPC.ai[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            const float ai0 = 0.01f;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(Math.PI / 2), Accel, NPC.damage / 4, 0f, Main.myPlayer, ai0);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(-Math.PI / 2), Accel, NPC.damage / 4, 0f, Main.myPlayer, ai0);
                        }
                    }
                    if (++NPC.ai[1] > 40)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        if (++NPC.ai[3] >= 3) //dash three times
                        {
                            NPC.ai[0]++;
                            NPC.ai[3] = 0;
                        }
                        else
                            NPC.ai[0]--;
                        NPC.netUpdate = true;
                    }
                    Dashing = true;
                    NPC.rotation = NPC.velocity.ToRotation();
                    if (NPC.velocity.X < 0)
                        NPC.rotation += (float)Math.PI;
                    break;

                case 11: //fly up, prepare to spit mega homing and dash
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 600 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y -= 600;
                    Movement(targetPos, 0.8f);
                    if (++NPC.ai[1] > 180 || NPC.Distance(targetPos) < 50)
                    {
                        Roar(roarTimerMax, false);
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                        NPC.velocity.X = -30 * (NPC.Center.X < player.Center.X ? -1 : 1);
                        NPC.velocity.Y = 5f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, Homing, NPC.damage / 3, 0f, Main.myPlayer, NPC.target, 8f);
                    }
                    NPC.rotation = 0;
                    break;

                case 12: //dashing
                    Dashing = true;
                    NPC.velocity *= 0.98f;
                    if (++NPC.ai[1] > 30)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 13: //hover nearby, shoot fireballs
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 700 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    Movement(targetPos, 0.7f);
                    if (++NPC.ai[2] > 60)
                    {
                        Roar(roarTimerMax, false);
                        NPC.ai[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient) //spawn lightning
                        {
                            Vector2 infernoPos = new Vector2(200f, NPC.direction == 1 ? 65f : -45f);
                            Vector2 vel = new Vector2(MathHelper.Lerp(6f, 8f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-4f, 4f, (float)Main.rand.NextDouble()));

                            if (player.active && !player.dead)
                            {
                                float rot = BaseUtility.RotationTo(NPC.Center, player.Center);
                                infernoPos = BaseUtility.RotateVector(Vector2.Zero, infernoPos, rot);
                                vel = BaseUtility.RotateVector(Vector2.Zero, vel, rot);
                                vel *= MoveSpeed / _normalSpeed; //to compensate for players running away
                                int dir = NPC.Center.X < player.Center.X ? 1 : -1;
                                if ((dir == -1 && NPC.velocity.X < 0) || (dir == 1 && NPC.velocity.X > 0)) vel.X += NPC.velocity.X;
                                vel.Y += NPC.velocity.Y;
                                infernoPos += NPC.Center;
                                infernoPos.Y -= 70;
                            }
                            //REMEMBER: PROJECTILES DOUBLE DAMAGE so to get an accurate damage count you divide it by 2!
                            float InfernoType;
                            if (NPC.spriteDirection == -1)
                            {
                                InfernoType = 1;
                            }
                            else
                            {
                                InfernoType = 2;
                            }

                            int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)infernoPos.X, (int)infernoPos.Y, vel.X, vel.Y, Inferno, damage, 0f, Main.myPlayer, InfernoType, 0f);
                            Main.projectile[projectile].velocity = vel;
                            Main.projectile[projectile].netUpdate = true;
                        }
                    }
                    if (++NPC.ai[1] > 360)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = NPC.Distance(player.Center);
                        NPC.netUpdate = true;
                        NPC.velocity = NPC.DirectionTo(player.Center).RotatedBy(Math.PI / 2) * 40;
                    }
                    break;

                case 14: //fly in jumbo circle
                    NPC.velocity -= NPC.velocity.RotatedBy(Math.PI / 2) * NPC.velocity.Length() / NPC.ai[3];
                    if (++NPC.ai[2] > 5)
                    {
                        NPC.ai[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            const float ai0 = 0.004f;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(Math.PI / 2), Accel, NPC.damage / 4, 0f, Main.myPlayer, ai0);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(-Math.PI / 2), Accel, NPC.damage / 4, 0f, Main.myPlayer, ai0);
                        }
                    }
                    if (NPC.ai[1] <= 1)
                    {
                        Roar(roarTimerMax, false);
                    }
                    if (++NPC.ai[1] > 150)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                    }
                    NPC.rotation = NPC.velocity.ToRotation();
                    Dashing = true;
                    break;

                case 15: //wait for old attack to go away
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 600 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    Movement(targetPos, 1f);
                    if (++NPC.ai[2] > 120)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        NPC.netUpdate = true;
                    }
                    NPC.rotation = 0;
                    break;

                default:
                    NPC.ai[0] = 0;
                    goto case 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[NPC.target];
            NPC.frame = new Rectangle(0, Roaring ? frameY : 0, 444, frameY);
            if (Dashing)
            {
                NPC.frameCounter = 0;
                wingFrame.Y = wingFrameY;
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 5)
                {
                    NPC.frameCounter = 0;
                    wingFrame.Y += wingFrameY;
                    if (wingFrame.Y > (wingFrameY * 4))
                    {
                        NPC.frameCounter = 0;
                        wingFrame.Y = 0;
                    }
                }
                NPC.spriteDirection = NPC.Center.X < player.Center.X ? 1 : -1;
            }
        }

        private bool AliveCheck(Player player)
        {
            if ((!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 5000f))
            {
                NPC.TargetClosest();
                if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 5000f)
                {
                    if (NPC.timeLeft > 60)
                        NPC.timeLeft = 60;
                    BaseAI.KillNPC(NPC);
                    NPC.netUpdate2 = true;
                    return false;
                }
            }
            if (NPC.timeLeft < 600)
                NPC.timeLeft = 600;
            return true;
        }

        private void Movement(Vector2 targetPos, float speedModifier)
        {
            if (NPC.Center.X < targetPos.X)
            {
                NPC.velocity.X += speedModifier;
                if (NPC.velocity.X < 0)
                    NPC.velocity.X += speedModifier * 2;
            }
            else
            {
                NPC.velocity.X -= speedModifier;
                if (NPC.velocity.X > 0)
                    NPC.velocity.X -= speedModifier * 2;
            }
            if (NPC.Center.Y < targetPos.Y)
            {
                NPC.velocity.Y += speedModifier;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speedModifier * 2;
            }
            else
            {
                NPC.velocity.Y -= speedModifier;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(NPC.velocity.X) > 30)
                NPC.velocity.X = 30 * Math.Sign(NPC.velocity.X);
            if (Math.Abs(NPC.velocity.Y) > 30)
                NPC.velocity.Y = 30 * Math.Sign(NPC.velocity.Y);
        }

        bool Dashing = false;

        public void HandleFrames(Player player)
        {
            NPC.frame = new Rectangle(0, Roaring ? frameY : 0, 444, frameY);
            if (Dashing)
            {
                NPC.frameCounter = 0;
                wingFrame.Y = wingFrameY;
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 5)
                {
                    NPC.frameCounter = 0;
                    wingFrame.Y += wingFrameY;
                    if (wingFrame.Y > (wingFrameY * 4))
                    {
                        NPC.frameCounter = 0;
                        wingFrame.Y = 0;
                    }
                }
            }
            NPC.direction = NPC.Center.X < player.Center.X ? 1 : -1;
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.TargetDamageMultiplier *= .8f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            Player player = Main.player[NPC.target];
            if (NPC.life <= NPC.lifeMax / 2 && !SpawnGrips && !isAwakened)
            {
                SpawnGrips = true;
                if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon2"), Color.DarkMagenta);
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<AbyssGrip>(), false, 0, 0);
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<BlazeGrip>(), false, 0, 0);
                SoundEngine.PlaySound(SoundID.Roar, player.position);
            }
            if (NPC.life <= NPC.lifeMax * .4f && !SpawnGrips && isAwakened)
            {
                SpawnGrips = true;

                if (AAWorld.downedShen)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon3"), Color.DarkMagenta);
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon4"), new Color(102, 20, 48));
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon5"), new Color(72, 78, 117));
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon6"), Color.DarkMagenta);
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon7"), new Color(102, 20, 48));
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon8"), new Color(72, 78, 117));
                }

                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<FuryAshe>(), false, 0, 0);
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<WrathHaruka>(), false, 0, 0);
            }

            if (NPC.life <= NPC.lifeMax * 0.80f && !Health4 && !isAwakened)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon19"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon20"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health4 = true;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.66f && !Health3 && !isAwakened)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon21"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon22"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health3 = true;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.30f && !Health1 && !isAwakened)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon23"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon24"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health1 = true;
                NPC.netUpdate = true;
            }
        }

        public override void OnKill()
        {
            if (NPC.type != ModContent.NPCType<ShenA>())
            {
                NPC.DropLoot(Items.Vanity.Mask.ShenMask.type, 1f / 7);
                if (!Main.expertMode)
                {
                    if (!NPC.BeenKilled(true))
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon16"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon18"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    NPC.DropLoot(ModContent.ItemType<ChaosScale>(), 20, 30);
                    string[] lootTable = { "ChaosSlayer", "MeteorStrike", "Skyfall", "Astroid", "DraconicRipper", "FlamingTwilight", "ShenTerratool", "Timesplitter" };
                    int loot = Main.rand.Next(lootTable.Length);
                    NPC.DropLoot(Mod.Find<ModItem>(lootTable[loot]).Type);

                }
                else
                {
                    NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ShenTransition>());
                }

                if(Main.rand.NextBool(10))
                    NPC.DropLoot(ModContent.ItemType<ShenTrophy>());
                //NPC.value = 0f;
                //NPC.boss = false;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D currentTex = NPC.spriteDirection == 1 ? Mod.GetTexture("NPCs/Bosses/Shen/ShenDoragonBlue") : TextureAssets.Npc[NPC.type].Value;
            Texture2D currentWingTex = NPC.spriteDirection == 1 ? Mod.GetTexture("NPCs/Bosses/Shen/ShenDoragonBlueWings") : Mod.GetTexture("NPCs/Bosses/Shen/ShenDoragonWings");

            //offset
            NPC.position.Y += 130f;

            //draw body/charge afterimage
            if (Dashing)
            {
                BaseDrawing.DrawAfterimage(spriteBatch, currentTex, 0, NPC, 1.5f, 1f, 3, false, 0f, 0f, new Color(drawColor.R, drawColor.G, drawColor.B, 150));
            }
            BaseDrawing.DrawTexture(spriteBatch, currentTex, 0, NPC, NPC.GetAlpha(drawColor), false);
            //draw wings
            BaseDrawing.DrawTexture(spriteBatch, currentWingTex, 0, NPC.position + new Vector2(0, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, 5, wingFrame, NPC.GetAlpha(drawColor), false);

            //deoffset
            NPC.position.Y -= 130f;
            return false;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public static int ShootPeriodic(Entity codable, Vector2 position, int width, int height, int projType, ref float delayTimer, float delayTimerMax = 100f, int damage = -1, float speed = 10f, bool checkCanHit = true)
        {
            int pID = -1;
            if (damage == -1) { Projectile proj = new Projectile(); proj.SetDefaults(projType); damage = proj.damage; }
            bool properSide = codable is NPC ? Main.netMode != NetmodeID.MultiplayerClient : codable is Projectile ? ((Projectile)codable).owner == Main.myPlayer : true;
            if (properSide)
            {
                Vector2 targetCenter = position + new Vector2(width * 0.5f, height * 0.5f);
                delayTimer--;
                if (delayTimer <= 0)
                {
                    if (!checkCanHit || Collision.CanHit(codable.position, codable.width, codable.height, position, width, height))
                    {
                        Vector2 fireTarget = codable.Center + new Vector2(167 * codable.direction, 0);
                        float rot = BaseUtility.RotationTo(codable.Center, targetCenter);
                        fireTarget = BaseUtility.RotateVector(codable.Center, fireTarget, rot);
                        pID = BaseAI.FireProjectile(targetCenter, fireTarget, projType, damage, 0f, speed);
                    }
                    delayTimer = delayTimerMax;
                    if (codable is NPC) { ((NPC)codable).netUpdate = true; }
                }
            }
            return pID;
        }
    }
}

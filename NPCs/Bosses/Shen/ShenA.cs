using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.CrossMod;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic.NPCs.Bosses.Shen
{
    [AutoloadBossHead]
    public class ShenA : Shen
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shen Doragon Awakened");
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 130;
            NPC.defense = 80;
            NPC.lifeMax = 1000000;
            NPC.value = Item.sellPrice(1, 0, 0, 0);
            bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("ShenCache").Type;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA");
            SceneEffectPriority = (SceneEffectPriority)11;
            isAwakened = true;
            NPC.alpha = 255;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * bossLifeScale);
            NPC.defense = (int)(NPC.defense * 1.2f);
            NPC.damage = (int)(NPC.damage * .8f);
        }

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
                FleeTimer[0] = reader.ReadFloat();
            }
        }

        public override void AI()
        {
            Main.dayTime = false;
            Main.time = 18000;

            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            Vector2 targetPos;

            if (!AliveCheck(player)) return;

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

            if (!NPC.AnyNPCs(Mod.Find<ModNPC>("ShenAHitbox").Type))
            {
                int hitbox = NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("ShenAHitbox").Type, 0, NPC.whoAmI, 0f, 0f, 0f, 255);
                Main.npc[hitbox].netUpdate = true;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<AwakenedShenAH.FuryAshe>()) || NPC.AnyNPCs(ModContent.NPCType<AwakenedShenAH.WrathHaruka>()))
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
                        int dust = ModContent.DustType<DiscordLight>();
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
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.Center, Vector2.UnitX.RotatedBy(NPC.ai[3]), Mod.Find<ModProjectile>("ShenDeathray").Type, NPC.damage / 3, 0f, Main.myPlayer, 0, NPC.whoAmI);
                    }
                    if (++NPC.ai[1] > 60)
                    {
                        NPC.ai[1] = 0;
                        Roar(roarTimerMax, false);
                        NPC.netUpdate = true;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            for (int i = -2; i <= 2; i++)
                                Projectile.NewProjectile(NPC.Center, 30 * Vector2.UnitX.RotatedBy(Math.PI / 4 * i) * (NPC.Center.X < player.Center.X ? -1 : 1), Mod.Find<ModProjectile>("ShenFireballSpread").Type, NPC.damage / 4, 0f, Main.myPlayer, 20, 20 + 60);
                    }
                    break;

                case 1: //firing mega ray
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

                case 5: //dashing, leave trail of vertical deathrays
                    if (NPC.ai[3] == 0 && --NPC.ai[2] < 0) //spawn rays on first dash only
                    {
                        NPC.ai[2] = 4;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.Center, Vector2.UnitY, Mod.Find<ModProjectile>("ShenDeathrayVertical").Type, NPC.damage / 4, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                            Projectile.NewProjectile(NPC.Center, -Vector2.UnitY, Mod.Find<ModProjectile>("ShenDeathrayVertical").Type, NPC.damage / 4, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                        }
                    }
                    if (++NPC.ai[1] > 240 || (Math.Sign(NPC.velocity.X) > 0 ? NPC.Center.X > player.Center.X + 900 : NPC.Center.X < player.Center.X - 900))
                    {
                        Roar(roarTimerMax, false);
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
                    Movement(targetPos, 0.5f);
                    if (++NPC.ai[2] > 60)
                    {
                        NPC.ai[2] = 0;
                        Roar(roarTimerMax, false);
                        NPC.netUpdate = true;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Vector2 spawnPos = NPC.Center;
                            spawnPos.X += 250 * (NPC.Center.X < player.Center.X ? 1 : -1);
                            Vector2 vel = (player.Center - spawnPos) / 30;
                            if (vel.Length() < 25)
                                vel = Vector2.Normalize(vel) * 25;
                            Projectile.NewProjectile(spawnPos, vel, Mod.Find<ModProjectile>("ShenFireballFrag").Type, NPC.damage / 4, 0f, Main.myPlayer);
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
                            Projectile.NewProjectile(NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(Math.PI / 2), Mod.Find<ModProjectile>("ShenFireballAccel").Type, NPC.damage / 4, 0f, Main.myPlayer, ai0);
                            Projectile.NewProjectile(NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(-Math.PI / 2), Mod.Find<ModProjectile>("ShenFireballAccel").Type, NPC.damage / 4, 0f, Main.myPlayer, ai0);
                        }
                    }
                    if (++NPC.ai[1] > 40)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        if (++NPC.ai[3] >= 5) //dash five times
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
                        NPC.velocity.X = -40 * (NPC.Center.X < player.Center.X ? -1 : 1);
                        NPC.velocity.Y = 5f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("ShenFireballHoming").Type, NPC.damage / 3, 0f, Main.myPlayer, NPC.target, 8f);
                    }
                    NPC.rotation = 0;
                    break;

                case 12: //dashing
                    Dashing = true;
                    NPC.velocity *= 0.99f;
                    if (++NPC.ai[1] > 30)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 13: //hover nearby, shoot lightning
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 700 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    Movement(targetPos, 0.7f);
                    if (++NPC.ai[2] > 40)
                    {
                        Roar(roarTimerMax, false);
                        NPC.ai[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient) //spawn lightning
                        {
                            Vector2 infernoPos = new Vector2(200f, NPC.direction == -1 ? 65f : -45f);
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
                            }
                            Projectile.NewProjectile((int)infernoPos.X, (int)infernoPos.Y + 16, vel.X * 2, vel.Y * 2, Mod.Find<ModProjectile>("ChaosLightning").Type, NPC.damage / 4, 0f, Main.myPlayer, vel.ToRotation(), 0f);
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
                    if (++NPC.ai[2] > 1)
                    {
                        NPC.ai[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            const float ai0 = 0.004f;
                            Projectile.NewProjectile(NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(Math.PI / 2), Mod.Find<ModProjectile>("ShenFireballAccel").Type, NPC.damage / 4, 0f, Main.myPlayer, ai0);
                            Projectile.NewProjectile(NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(-Math.PI / 2), Mod.Find<ModProjectile>("ShenFireballAccel").Type, NPC.damage / 4, 0f, Main.myPlayer, ai0);
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

        public override void OnKill()
        {
            if (NPC.type == ModContent.NPCType<ShenA>())
            {
                if (Main.expertMode)
                {
                    NPC.DropLoot(Items.Vanity.Mask.ShenAMask.type, 1f / 7);
                    if (!AAWorld.downedShen)
                    {
                        NPC.DropLoot(ModContent.ItemType<Items.BossSummons.ChaosRune>());
                    }

                    BaseAI.DropItem(NPC, Mod.Find<ModItem>("ShenATrophy").Type, 1, 1, 15, true);

                    if (!NPC.AnyNPCs(ModContent.NPCType<ShenDefeat>()))
                    {
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ShenDefeat>());
                    }

                    NPC.DropBossBags();
                }
            }
                
        }

            bool Dashing = false;

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
                if (NPC.ai[0] != 1)
                {
                    NPC.spriteDirection = NPC.Center.X < player.Center.X ? 1 : -1;
                }
            }
        }

        public bool Health9 = false;
        public bool Health8 = false;
        public bool Health7 = false;
        public bool Health6 = false;
        public bool Health5 = false;
        public bool HealthOneHalf = false;

        public override void HitEffect(NPC.HitInfo hit)
        {
            base.HitEffect(hitDirection, damage);
            if (NPC.life <= NPC.lifeMax * 0.9f && !Health9)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA1"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA2"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health9 = true;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.8f && !Health8)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA3"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA4"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health8 = true;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.7f && !Health7)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA5"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA6"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health7 = true;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.6f && !Health6)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA7"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA8"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health6 = true;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.5f && !Health5)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(BossDialogue(), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health5 = true;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.3f && !Health3)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA11"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA12"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health3 = true;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.2f && !Health2)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA13"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA14"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health2 = true;
                NPC.netUpdate = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.1f && !Health1)
            {
                if (AAWorld.downedShen)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA15"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ShenA16"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                Health1 = true;
                NPC.netUpdate = true;
            }
            if (Health2)
            {
               // music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/LastStand");
            }
        }

        public bool DownedRag => (bool)ModSupport.GetModWorldConditions("ThoriumMod", "ThoriumWorld", "downedRealityBreaker", false, true);
        public bool DownedScal => (bool)ModSupport.GetModWorldConditions("CalamityMod", "CalamityWorld", "downedSCal", false, true);
        public bool DownedMantid => (bool)ModSupport.GetModWorldConditions("GRealm", "MWorld", "downedMatriarch", false, true);
        public bool DownedNeb => (bool)ModSupport.GetModWorldConditions("Redemption", "RedeWorld", "downedNebuleus", false, true);
        public bool DownedOverseer => (bool)ModSupport.GetModWorldConditions("SpiritMod", "MyWorld", "downedOverseer", false, true);
        //public bool DownedDuo => JetshiftMod.JetshiftWorld.downedCosmicMystery;

        public string BossDialogue()
        {
            WeightedRandom<string> Text = new WeightedRandom<string>();

            bool a = false;

            if (ModSupport.GetMod("ThoriumMod") != null && DownedRag)
            {
                a = true;
                Text.Add(Lang.BossChat("ShenAThorium"));
            }

            if (ModSupport.GetMod("CalamityMod") != null && DownedScal)
            {
                a = true;
                Text.Add(Lang.BossChat("ShenACalamity"));
            }

            if (ModSupport.GetMod("GRealm") != null && DownedMantid)
            {
                a = true;
                Text.Add(Lang.BossChat("ShenAGRealm"));
            }

            if (ModSupport.GetMod("Redemption") != null && DownedNeb)
            {
                a = true;
                Text.Add(Lang.BossChat("ShenARedemption"));
            }

            if (ModSupport.GetMod("SpiritMod") != null && DownedOverseer)
            {
                a = true;
                Text.Add(Lang.BossChat("ShenASpirit"));
            }

            /*if (AAMod.jsLoaded && DownedDuo)
            {
                a = true;
                Text.Add("But slaying those two meteor-squatting crystal things? That's quite an eye-catcher.");
            }*/

            if (!a)
            {
                Text.Add(Lang.BossChat("ShenANoMod"));
            }
            return Text;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D currentTex = TextureAssets.Npc[NPC.type].Value;
            Texture2D currentWingTex1 = Mod.GetTexture("NPCs/Bosses/Shen/ShenWingBack");
            Texture2D currentWingTex2 = Mod.GetTexture("NPCs/Bosses/Shen/ShenWingFront");
            Texture2D glowTex = Mod.GetTexture("NPCs/Bosses/Shen/ShenA_Glow");

            //offset
            NPC.position.Y += 130f;

            //draw body/charge afterimage
            BaseDrawing.DrawTexture(sb, currentWingTex1, 0, NPC.position + new Vector2(0, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, 5, wingFrame, drawColor);
            if (Dashing)
            {
                BaseDrawing.DrawAfterimage(sb, currentTex, 0, NPC, 1.5f, 1f, 3, false, 0f, 0f, new Color(drawColor.R, drawColor.G, drawColor.B, 150));
            }
            BaseDrawing.DrawTexture(sb, currentTex, 0, NPC, drawColor);

            //draw glow/glow afterimage
            BaseDrawing.DrawTexture(sb, glowTex, 0, NPC, AAColor.Shen3);
            BaseDrawing.DrawAfterimage(sb, glowTex, 0, NPC, 0.3f, 1f, 8, false, 0f, 0f, AAColor.Shen3);

            //draw wings
            BaseDrawing.DrawTexture(sb, currentWingTex2, 0, NPC.position + new Vector2(0, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, 5, wingFrame, drawColor);

            //deoffset
            NPC.position.Y -= 130f; // offsetVec;			

            return false;
        }
    }

}

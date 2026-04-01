using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

using System.IO;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Localization;

namespace AAModClassic.NPCs.Bosses.Yamata.Awakened
{
    [AutoloadBossHead]
    public class YamataAHeadF : ModNPC
    {
		public bool isAwakened = false;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yamata no Orochi");
            Main.npcFrameCount[NPC.type] = 3;
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.lifeMax = 30000;
            NPC.width = 76;
            NPC.height = 92;
            NPC.npcSlots = 0;
            NPC.dontCountMe = true;
            NPC.noTileCollide = true;
            NPC.boss = false;
            NPC.noGravity = true;
            NPC.chaseable = false;
            NPC.damage = 100;
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            NPC.DeathSound = Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/Sounds/YamataRoar");
            NPC.lifeMax = 45000;
            NPC.width = 46;
            NPC.height = 46;
            isAwakened = true;
            NPC.knockBackResist *= 0.1f;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

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
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return NPC.alpha == 0;
        }

        public YamataA Body = null;
        public bool killedbyplayer = true;	
		public bool leftHead = false;
        public bool fireAttack = false;
		public int distFromBodyX = 110; //how far from the body to centeralize the movement points. (X coord)
		public int distFromBodyY = 150; //how far from the body to centeralize the movement points. (Y coord)
		public int movementVariance = 60; //how far from the center point to move.

        public override void AI()
        {
            //npc.defDamage = isAwakened ? 200 : 180;
            NPC npcBody = Main.npc[(int)NPC.ai[0]];
            if (npcBody.active && npcBody.type == ModContent.NPCType<YamataA>())
            {
                Body = (YamataA)npcBody.ModNPC;
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) //force a kill to prevent 'ghost hands'
                {
                    NPC.life = 0;
                    NPC.checkDead();
                    NPC.netUpdate = true;
                    killedbyplayer = false;
                }
                return;
            }

            NPC.alpha = Body.NPC.alpha;
            
            NPC.TargetClosest();
            Player targetPlayer = Main.player[NPC.target];
            if (targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null
            
            Vector2 nextTarget = Body.NPC.Center + new Vector2(NPC.ai[1], NPC.ai[2]);
            
            switch ((int)internalAI[0])
            {
                case 0: //charge up

                    //insert charging dust here

                    if (internalAI[1] == 180 - 60)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            float ai0 = (float)Math.PI * 2 / 300 * (NPC.ai[3] == 2 ? 1 : -1) * Math.Sign(NPC.ai[1]);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY, Mod.Find<ModProjectile>("YamataWaveDeathraySmall").Type, NPC.damage / 4, 0f, Main.myPlayer, ai0, NPC.whoAmI);
                        }
                    }

                    if (++internalAI[1] > 180)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 1: //idle while firing laser
                    if (++internalAI[3] > 20) 
                    {
                        internalAI[3] = 0;
                        if (NPC.ai[3] == 3 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (Math.Sign(NPC.Center.X - targetPlayer.Center.X) != Math.Sign(NPC.ai[1])) //outermost heads enrage at player if they walk away from underneath
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(targetPlayer.Center) * 7f, Mod.Find<ModProjectile>("YamataAVenom2").Type, NPC.damage / 5, 0f, Main.myPlayer);
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(targetPlayer.Center) * 7f, Mod.Find<ModProjectile>("YamataABomb").Type, NPC.damage / 5, 0f, Main.myPlayer);
                            }
                            else
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY * 10, Mod.Find<ModProjectile>("AbyssalThunder").Type, NPC.damage / 5, 0f, Main.myPlayer);
                            }
                        }
                    }
                    if (++internalAI[1] > 300)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[3] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 2: //shoot shit
                    internalAI[2] += NPC.ai[3];
                    if (internalAI[2] > 180)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(targetPlayer.Center) * 5f, Mod.Find<ModProjectile>("YamataAVenom2").Type, NPC.damage / 5, 0f, Main.myPlayer);
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
                    internalAI[2] += NPC.ai[3];
                    if (++internalAI[2] > 120)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(targetPlayer.Center) * 20f, Mod.Find<ModProjectile>("YamataABreath").Type, NPC.damage / 5, 0f, Main.myPlayer);
                    }
                    if (++internalAI[1] > 180)
                    {
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
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(targetPlayer.Center), Mod.Find<ModProjectile>("YamataDeathraySmall").Type, NPC.damage / 4, 0f, Main.myPlayer, 0f, NPC.whoAmI);
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
                    internalAI[2] += NPC.ai[3];
                    if (internalAI[2] > 360)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY * 5, Mod.Find<ModProjectile>("YamataAShockBomb").Type, NPC.damage / 5, 0f, Main.myPlayer, NPC.target);
                    }
                    if (++internalAI[1] > 420)
                    {
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

                case 9: //weaker meteor rain
                    internalAI[2] += NPC.ai[3];
                    if (internalAI[2] > 120)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY * 10, Mod.Find<ModProjectile>("AbyssalThunder").Type, NPC.damage / 5, 0f, Main.myPlayer);
                    }
                    if (++internalAI[3] > 20) //outermost heads enrage at player if they walk away from underneath
                    {
                        internalAI[3] = 0;
                        if (NPC.ai[3] == 3 && Math.Sign(NPC.Center.X - targetPlayer.Center.X) != Math.Sign(NPC.ai[1]) && Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(targetPlayer.Center) * 5f, Mod.Find<ModProjectile>("YamataAVenom2").Type, NPC.damage / 5, 0f, Main.myPlayer);
                    }
                    if (++internalAI[1] > 360)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
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

            float dist = Vector2.Distance(nextTarget, NPC.Center);
            /*if (YamataHead.EATTHELITTLEMAGGOT && playerDistance < 300f)
            {
                BaseAI.AIFlier(npc, ref customAI, true, .5f, .8f, 5, 5, false, 300);
            }
            else*/
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
            //npc.position += Body.npc.position - Body.npc.oldPosition;
            NPC.spriteDirection = -1;
            if (Body.TeleportMe1)
            {
                Body.TeleportMe1 = false;
                NPC.Center = Body.NPC.Center;
                return;
            }
            if (Body.TeleportMe2)
            {
                Body.TeleportMe2 = false;
                NPC.Center = Body.NPC.Center;
                return;
            }
            if (Body.TeleportMe3)
            {
                Body.TeleportMe3 = false;
                NPC.Center = Body.NPC.Center;
                return;
            }
            if (Body.TeleportMe4)
            {
                Body.TeleportMe4 = false;
                NPC.Center = Body.NPC.Center;
                return;
            }
            if (Body.TeleportMe5)
            {
                Body.TeleportMe5 = false;
                NPC.Center = Body.NPC.Center;
                return;
            }
            if (Body.TeleportMe6)
            {
                Body.TeleportMe6 = false;
                NPC.Center = Body.NPC.Center;
                return;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (fireAttack || YamataHead.EATTHELITTLEMAGGOT)
            {
                if (NPC.frameCounter < 5)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else if (NPC.frameCounter < 10)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
            }
            else
            {
                NPC.frameCounter = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                CombatText.NewText(NPC.getRect(), new Color(146, 30, 68), Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.YamataAHead"), false, false);
                NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<YamataSoul>());
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player targetPlayer = Main.player[NPC.target];
            if (targetPlayer.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
            {
                modifiers.TargetDamageMultiplier /= 2;
                modifiers.DisableCrit();
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                modifiers.TargetDamageMultiplier *= 0.2f;
            }
            else if (projectile.penetrate >= 1)
            {
                modifiers.TargetDamageMultiplier *= 0.2f;
            }
            else if (projectile.type == ProjectileID.LastPrismLaser)
            {
                modifiers.TargetDamageMultiplier *= 0.05f;
            }
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Yamata>()) || NPC.AnyNPCs(ModContent.NPCType<YamataA>()))
            {
                return false;
            }
            return true;
        }
    }
}

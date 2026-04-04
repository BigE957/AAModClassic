using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

using System.IO;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic;
using AAModClassic.NPCs.Bosses.Yamata.Awakened;

namespace AAModClassic.NPCs.Bosses.Yamata
{
    [AutoloadBossHead]
    public class YamataHeadF1 : ModNPC
    {
		public bool isAwakened = false;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yamata");
            Main.npcFrameCount[NPC.type] = 3;
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.lifeMax = 30000;
            NPC.width = 64;
            NPC.height = 48;
            NPC.npcSlots = 0;
            NPC.dontCountMe = true;
            NPC.noTileCollide = true;
            NPC.boss = false;
            NPC.noGravity = true;
            NPC.damage = 80;
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            NPC.DeathSound = Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/Sounds/YamataRoar");
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(customAI[0]);
                writer.Write(customAI[1]);
                writer.Write(customAI[2]);
                writer.Write(customAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                customAI[0] = reader.ReadFloat();
                customAI[1] = reader.ReadFloat();
                customAI[2] = reader.ReadFloat();
                customAI[3] = reader.ReadFloat();
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

		public Yamata Body = null;
        public Yamata Head = null;
        public bool killedbyplayer = true;	
		public bool leftHead = false;
        public bool fireAttack = false;
		public int distFromBodyX = 110; //how far from the body to centeralize the movement points. (X coord)
		public int distFromBodyY = 150; //how far from the body to centeralize the movement points. (Y coord)
		public int movementVariance = 60; //how far from the center point to move.

        public override void AI()
        {
            NPC.defDamage = isAwakened ? 200 : 180;
            if (Body == null)
            {
                NPC npcBody = Main.npc[(int)NPC.ai[0]];
                if (npcBody.type == ModContent.NPCType<Yamata>() || npcBody.type == ModContent.NPCType<YamataA>())
                {
                    Body = (Yamata)npcBody.ModNPC;
                }
            }
            if (Body == null)
                return;			

            NPC.alpha = Body.NPC.alpha;

            if (NPC.alpha > 0)
            {
                NPC.damage = 0;
            }
            else
            {
                NPC.damage = NPC.defDamage;
            }
            NPC.TargetClosest();
            Player targetPlayer = Main.player[NPC.target];
            if (targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null


            float playerDistance = targetPlayer == null ? 99999f : Vector2.Distance(targetPlayer.Center, NPC.Center);
            if (!Body.NPC.active)
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
            Vector2 nextTarget = Body.NPC.Center + new Vector2(leftHead ? -distFromBodyX : distFromBodyX, -distFromBodyY) + new Vector2(NPC.ai[2], NPC.ai[3]);
            float dist = Vector2.Distance(nextTarget, NPC.Center);
            if (YamataHead.EATTHELITTLEMAGGOT && playerDistance < 300f)
            {
                BaseAI.AIFlier(NPC, ref customAI, true, .5f, .8f, 5, 5, false, 300);
            }
            else
            if (dist < 40f)
            {
                NPC.velocity *= 0.9f;
                if (Math.Abs(NPC.velocity.X) < 0.05f) NPC.velocity.X = 0f;
                if (Math.Abs(NPC.velocity.Y) < 0.05f) NPC.velocity.Y = 0f;
            }
            else
            {
                NPC.velocity = Vector2.Normalize(nextTarget - NPC.Center);
                NPC.velocity *= 5f;
            }
            NPC.position += Body.NPC.position - Body.NPC.oldPosition;
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
                for (int i = 0; i < 5; ++i)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                    Vector2 dir = Vector2.Normalize(targetPlayer.Center - NPC.Center);
                    dir *= 5f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, dir.X, dir.Y, isAwakened ? ModContent.ProjectileType<YamataABreath>() : ModContent.ProjectileType<YamataBreath>(), NPC.damage / 4, 0f, Main.myPlayer);
                }
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

            if (Main.netMode != NetmodeID.MultiplayerClient && !YamataHead.EATTHELITTLEMAGGOT)
            {
                if (NPC.alpha <= 0)
                {
                    NPC.ai[1]++; ;
                }
                int aiTimerFire = NPC.whoAmI % 3 == 0 ? 50 : NPC.whoAmI % 2 == 0 ? 150 : 100;
                if (leftHead) aiTimerFire += 30;
                if (targetPlayer != null && NPC.ai[1] == aiTimerFire)
                {
                    fireAttack = true;
                    for (int i = 0; i < 5; ++i)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                        Vector2 dir = Vector2.Normalize(targetPlayer.Center - NPC.Center);
                        dir *= 5f;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, dir.X, dir.Y, isAwakened ? ModContent.ProjectileType<YamataABreath>() : ModContent.ProjectileType<YamataBreath>(), NPC.damage / 4, 0f, Main.myPlayer);
                    }
                }
                else
                if (NPC.ai[1] >= 200) //pick random spot to move head to
                {
                    fireAttack = false;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = Main.rand.Next(-movementVariance, movementVariance);
                    NPC.ai[3] = Main.rand.Next(-movementVariance, movementVariance);
                    NPC.netUpdate = true;
                }
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
                CombatText.NewText(NPC.getRect(), new Color(45, 46, 70), Lang.BossChat("YamataHead"), true, true);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[NPC.target];
            if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
            {
                modifiers.TargetDamageMultiplier /= 2;
                modifiers.DisableCrit();
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                modifiers.TargetDamageMultiplier *= .2f;
            }
            else if (projectile.penetrate >= 1)
            {
                modifiers.TargetDamageMultiplier *= (int).2;
            }
            else if (projectile.type == ProjectileID.LastPrismLaser)
            {
                modifiers.TargetDamageMultiplier *= .05f;
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

        private int HomeOnTarget()
        {
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.type == NPCID.Bunny)
                {
                    float distance = NPC.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            NPC.Distance(Main.npc[selectedTarget].Center) > distance) 
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }
    }
}

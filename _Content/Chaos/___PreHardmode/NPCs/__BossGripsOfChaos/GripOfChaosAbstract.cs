using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs;
using AAModClassic._Content.Mire.___PreHardmode.NPCs;
using AAModClassic._CrossMod.CalamityMod;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.___PreHardmode.NPCs.__BossGripsOfChaos
{
    public abstract class GripOfChaosAbstract : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grip of Chaos");
            Main.npcFrameCount[NPC.type] = 8;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.width = 66;
            NPC.height = 60;
            NPC.aiStyle = -1;
			NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 1, 50, 0);
            NPC.npcSlots = 1f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
            Music = MusicManagementSystem.MusicSlots["Grips"];
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 6)
            {
				NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.ai[0] == 2 || NPC.ai[0] == 3 || NPC.ai[0] == 4)
                {
                    if (NPC.frame.Y < 4 * frameHeight || NPC.frame.Y < 7 * frameHeight)
                    {
                        NPC.frame.Y = 4 * frameHeight;
                    }
                }
                else
                {
                    if (NPC.frame.Y > 4 * frameHeight)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
        }
        public static bool canGrab;
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);  //boss life scale in expertmode
            NPC.damage = (int)(NPC.damage * 0.8f);  //boss damage increase in expermode
        }

		public override void BossHeadRotation(ref float rotation)
		{
			rotation = NPC.rotation;
		}
		public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
		{
			spriteEffects = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
		}

		public Vector2 offsetBasePoint = Vector2.Zero;
		public float moveSpeed = 6f;
        public int MinionTimer = 0;
        public static bool checkOver = true;
        public float[] internalAI = new float[3];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
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
            }
        }

        public override void AI()
		{
			NPC.TargetClosest();
			Player targetPlayer = Main.player[NPC.target];

            if (Main.dayTime)
            {
                DespawnHandler();
                return;
            }
            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                NPC.TargetClosest(false);
                if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
                {
                    DespawnHandler();
                }
                return;
            }

            bool forceChange = false;
			if(Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] != 2 && NPC.ai[0] != 3)
			{
				int stopValue = 250;
				NPC.ai[3]++;
				if(NPC.ai[3] > stopValue) NPC.ai[3] = stopValue;
				forceChange = NPC.ai[3] >= stopValue;
			}
            if(NPC.ai[0] == 0)
                checkOver = true;
            if (NPC.ai[0] == 1) //move to starting charge position
			{ 
                if (Main.netMode != NetmodeID.MultiplayerClient && checkOver)
                {
                    NPC.ai[3] = 0;
                    foreach(NPC n in Main.ActiveNPCs)
                    {
                        if (NPC.type == ModContent.NPCType<GripOfChaosMire>())
                        {
                            if (n.type == ModContent.NPCType<GripOfChaosInferno>())
                            {
                                n.ai[0] = 1;
                                n.ai[3] = 0;
                                break;
                            }
                        }
                        if (NPC.type == ModContent.NPCType<GripOfChaosInferno>())
                        {
                            if (n.type == ModContent.NPCType<GripOfChaosMire>())
                            {
                                n.ai[0] = 1;
                                n.ai[3] = 0;
                                break;
                            }
                        }
                    }
                    NPC.netUpdate = true;
                    checkOver = false;
                    canGrab = true;
                }
                internalAI[1] = 0;
                internalAI[2] = 0;
                moveSpeed = 7f;
				Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -250f);
				MoveToPoint(point);
				if(Main.netMode != NetmodeID.MultiplayerClient && (Vector2.Distance(NPC.Center, point) < 10f || forceChange))
				{
					NPC.ai[0] = 2;
					NPC.ai[1] = targetPlayer.Center.X;
					NPC.ai[2] = targetPlayer.Center.Y;
					NPC.ai[3] = 0;
					NPC.netUpdate = true;
				}
				BaseAI.LookAt(targetPlayer.Center, NPC, 0, 0f, 0.1f, false);
                
            }
            else if(NPC.ai[0] == 2) //dive down
			{              
                moveSpeed = 9f;
				Vector2 targetCenter = new Vector2(NPC.ai[1], NPC.ai[2]);
                
                Vector2 point = targetCenter - offsetBasePoint + new Vector2(0f, 250f);
				MoveToPoint(point);
				if(Main.netMode != NetmodeID.MultiplayerClient && Vector2.Distance(NPC.Center, point) < 10f && internalAI[1] == 0 && internalAI[2] == 0 || internalAI[2] >= 60)
				{
					bool doubleDive = NPC.life < NPC.lifeMax / 2;
                    NPC.ai[0] = doubleDive ? 3 : 0;
                    NPC.ai[1] = doubleDive ? targetPlayer.Center.X : 0;
                    NPC.ai[2] = doubleDive ? targetPlayer.Center.Y : 0;
                    NPC.ai[3] = 0;
					NPC.netUpdate = true;
				}
                
			}
            else if(NPC.ai[0] == 3) //dive up
			{
                Player player = Main.player[NPC.target];
                Rectangle rectangle1 = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
                Rectangle rectangle2 = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
                if (rectangle1.Intersects(rectangle2) && canGrab)
                {
                    canGrab = false;
                    internalAI[1]++;
                }

                if (internalAI[1] != 0)
                {
                    targetPlayer.Center = NPC.Center + new Vector2(Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-4, 4));
                    internalAI[1]++;
                    if (NPC.type == ModContent.NPCType<GripOfChaosInferno>())
                        NPC.rotation += 0.1f - internalAI[1] / 600f;
                    if (NPC.type == ModContent.NPCType<GripOfChaosMire>())
                        NPC.rotation -= 0.1f - internalAI[1] / 600f;
                    if (internalAI[1] == 120)
                    {
                        player.invis = true;
                        targetPlayer.velocity = new Vector2((float)Math.Cos(NPC.rotation + Math.PI * 0.5f) * 20, (float)Math.Sin(NPC.rotation + Math.PI * 0.5f) * 20);
                        internalAI[2] = 1;
                        internalAI[1] = 0;
                    }
                }
                if (internalAI[2] != 0)
                {
                    if (NPC.type == ModContent.NPCType<GripOfChaosInferno>())
                        NPC.rotation -= 0.1f - internalAI[2] / 600;
                    if (NPC.type == ModContent.NPCType<GripOfChaosMire>())
                        NPC.rotation += 0.1f - internalAI[2] / 600;
                    internalAI[2]++;
                }
                NPC.ai[3] = 0;
                moveSpeed = 9f;
				Vector2 targetCenter = new Vector2(NPC.ai[1], NPC.ai[2]);
				Vector2 point = targetCenter + offsetBasePoint + new Vector2(0f, -250f);
				MoveToPoint(point);
				if(Main.netMode != NetmodeID.MultiplayerClient && Vector2.Distance(NPC.Center, point) < 10f && internalAI[1] == 0 && internalAI[2] == 0 || internalAI[2] >= 60)
				{
                    NPC.ai[0] = 0;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
					NPC.netUpdate = true;
				}
                if (internalAI[1] < 20 && internalAI[2] == 0)
                    BaseAI.Look(NPC, 0, 0f, 0.1f, false);
            }
            else
            if (NPC.ai[0] == 4) //dive back down
            {
                NPC.ai[3] = 0;
                moveSpeed = 9f;
                Vector2 targetCenter = new Vector2(NPC.ai[1], NPC.ai[2]);
                Vector2 point = targetCenter - offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                if (Main.netMode != NetmodeID.MultiplayerClient && Vector2.Distance(NPC.Center, point) < 10f)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    NPC.netUpdate = true;
                }
                BaseAI.Look(NPC, 0, 0f, 0.1f, false);
            }
            else //standard movement
			{
                MinionTimer++;
                if (MinionTimer == 160)
                {
                    if (NPC.type == ModContent.NPCType<GripOfChaosInferno>() && NPC.CountNPCS(ModContent.NPCType<DragonClaw_NPC>()) < 4)
                    {
                        NPC grip = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DragonClaw_NPC>());
                        DragonClaw_NPC claw = grip.ModNPC as DragonClaw_NPC;
                        claw.WasSpawnedByGripOfChaos = true;
                    }
                    if (NPC.type == ModContent.NPCType<GripOfChaosMire>() && NPC.CountNPCS(ModContent.NPCType<HydraClaw_NPC>()) < 4)
                    {
                        NPC grip = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HydraClaw_NPC>());
                        HydraClaw_NPC claw = grip.ModNPC as HydraClaw_NPC;
                        claw.WasSpawnedByGripOfChaos = true;
                    }
                    MinionTimer = 0;
                }
				moveSpeed = 5f;
				Vector2 point = targetPlayer.Center + offsetBasePoint;
				MoveToPoint(point);
				if(Main.netMode != NetmodeID.MultiplayerClient && (Vector2.Distance(NPC.Center, point) < 50f || forceChange))
				{
					NPC.ai[1]++;
					if(NPC.ai[1] > 150)
					{
						NPC.ai[0] = 1;
						NPC.ai[1] = 0;
						NPC.ai[2] = 0;
						NPC.ai[3] = 0;
						NPC.netUpdate = true;
					}
				}
				BaseAI.LookAt(targetPlayer.Center, NPC, 0, 0f, 0.1f, false);
			}
            if (NPC.ai[0] == 0)
            {
                NPC.alpha += 5;
                if (NPC.alpha >= 50)
                {
                    NPC.defense = 20;
                    NPC.alpha = 50;
                }
            }
            else
            {
                NPC.alpha -= 5;
                if (NPC.alpha <= 0)
                {
                    if (NPC.type == ModContent.NPCType<GripOfChaosInferno>())
                    {
                        NPC.defense = 12;
                    }
                    if (NPC.type == ModContent.NPCType<GripOfChaosMire>())
                    {
                        NPC.defense = 8;
                    }
                    NPC.alpha = 0;
                }
            }
        }

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
		{
			if(moveSpeed == 0f || NPC.Center == point) return; //don't move if you have no move speed
			float velMultiplier = 1f;
			Vector2 dist = point - NPC.Center;
			float length = dist == Vector2.Zero ? 0f : dist.Length();
			if(length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if(length < 200f)
			{
				moveSpeed *= 0.5f;
			}
			if(length < 100f)
			{
				moveSpeed *= 0.5f;
			}
			if(length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
			NPC.velocity *= moveSpeed;
			NPC.velocity *= velMultiplier;
		}

        private void DespawnHandler()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(false);
            player = Main.player[NPC.target];
            if (!player.active || player.dead || Main.dayTime)        // If the player is dead and not active, the npc flies off-screen and despawns
            {
                NPC.velocity.X = 0;
                NPC.velocity.Y -= 1;
            }
        }

        public class MissingGripAlways : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                int type = ModContent.NPCType<GripOfChaosMire>();
                if (info.npc.type == ModContent.NPCType<GripOfChaosMire>())
                    type = ModContent.NPCType<GripOfChaosInferno>();

                return !NPC.AnyNPCs(type);
            }

            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }

        public class MissingGripNormal : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (Main.expertMode)
                    return false;

                int type = ModContent.NPCType<GripOfChaosMire>();
                if (info.npc.type == ModContent.NPCType<GripOfChaosMire>())
                    type = ModContent.NPCType<GripOfChaosInferno>();

                return !NPC.AnyNPCs(type);
            }
            public bool CanShowItemDropInUI() => !Main.expertMode;
            public string GetConditionDescription() => Language.GetTextValue("Bestiary_ItemDropConditions.NotExpert");
        }

        public class MissingGripMaster : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!Main.masterMode && !CalamityMod.IsRevengance)
                    return false;

                int type = ModContent.NPCType<GripOfChaosMire>();
                if (info.npc.type == ModContent.NPCType<GripOfChaosMire>())
                    type = ModContent.NPCType<GripOfChaosInferno>();

                return !NPC.AnyNPCs(type);
            }

            public bool CanShowItemDropInUI() => Main.masterMode || CalamityMod.IsRevengance;
            public string GetConditionDescription() => CalamityMod.IsEnabled ? Language.GetTextValue("Mods.CalamityMod.Condition.RevOrMM") : Language.GetTextValue("Mods.AAModClassic.Common.Conditions.IsMaster");
        }
    }
}

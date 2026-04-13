using AAModClassic.___Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.___Content.Mire.Projectiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.___PreHardmode.NPCs.__BossHydra
{
    [AutoloadBossHead]
    public class HydraHead1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra");
            Main.npcFrameCount[NPC.type] = 2;
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }

        public float Shoot = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(Shoot);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                Shoot = reader.ReadSingle();
            }
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.lifeMax = 1300;
            NPC.width = 42;
            NPC.height = 54;
            NPC.damage = 40;
            NPC.npcSlots = 0;
            NPC.dontCountMe = true;
            NPC.noTileCollide = true;
            NPC.boss = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.Item88;
            NPC.noGravity = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            Head = 0;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AbyssiumOre>(), 1, 16, 20));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            LeadingConditionRule expertRule = new(new Conditions.IsExpert());

            expertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HydraHide>(), 1, 7, 17));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HydraHide>(), 1, 3, 7));

            npcLoot.Add(notExpertRule);
            npcLoot.Add(expertRule);
        }

        public int Head = 0;
        public HydraBody Body => bodyNPC != null && bodyNPC.ModNPC is HydraBody ? (HydraBody)bodyNPC.ModNPC : null;
        public NPC bodyNPC = null;
        public int damage = 0;

        public int movementVariance = 40;
        public bool fireAttack = false;

        public override bool PreAI()
        {

            return true;
        }

        public override void AI()
        {
            if (bodyNPC == null)
            {
                NPC npcBody = Main.npc[(int)NPC.ai[0]];
                if (npcBody.type == ModContent.NPCType<HydraBody>())
                {
                    bodyNPC = npcBody;
                }
            }

            if (!NPC.AnyNPCs(ModContent.NPCType<HydraBody>()))
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) //force a kill to prevent 'ghosting'
                {
                    NPC.life = 0;
                    NPC.checkDead();
                    NPC.netUpdate = true;
                }
                return;
            }
			if (bodyNPC == null)
				return;

            AssignHead();

            if (!bodyNPC.active)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) //force a kill to prevent 'ghosting'
                {
                    NPC.life = 0;
                    NPC.checkDead();
                    NPC.netUpdate = true;
                }
                return;
            }

            NPC.timeLeft = 100;

            NPC.TargetClosest();
            
            Player targetPlayer = Main.player[NPC.target];

            if (targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null
            
            if (!targetPlayer.ZoneAnyMire())
            {
                NPC.damage = 80;
                NPC.defense = 100;
            }
            else
            {
                NPC.damage = 40;
                NPC.defense = 0;
            }

            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[1]++;

                if (NPC.ai[1] >= 200) //pick random spot to move head to
                {
                    NPC.ai[1] = 0;
                    NPC.ai[2] = Main.rand.Next(-movementVariance, movementVariance);
                    NPC.ai[3] = Main.rand.Next(-movementVariance, movementVariance);
                    NPC.netUpdate = true;
                }
            }

            Vector2 nextTarget = Body.NPC.Center + HeadPos() + new Vector2(NPC.ai[2], NPC.ai[3]);

			float dist = Vector2.Distance(nextTarget, NPC.Center);
            if (dist < 40f)
            {
                NPC.velocity *= 0.9f;
                if (Math.Abs(NPC.velocity.X) < 0.05f) NPC.velocity.X = 0f;
                if (Math.Abs(NPC.velocity.Y) < 0.05f) NPC.velocity.Y = 0f;
            }else
            if (dist > 200f) //teleport to keep up with body
            {
                NPC.Center = Body.NPC.Center;
				NPC.netUpdate = true;
            }	
            else
            {
                NPC.velocity = Vector2.Normalize(nextTarget - NPC.Center);
                NPC.velocity *= 5f;
            }
            NPC.position += Body.NPC.position - Body.NPC.oldPosition;
            NPC.spriteDirection = -1;
        }

        public void AssignHead()
        {
            if (NPC.type == ModContent.NPCType<HydraHead4>() && Body.Head4 == null)
            {
                Body.Head4 = Main.npc[NPC.whoAmI];
            }
            if (NPC.type == ModContent.NPCType<HydraHead5>() && Body.Head5 == null)
            {
                Body.Head5 = Main.npc[NPC.whoAmI];
            }
            if (NPC.type == ModContent.NPCType<HydraHead6>() && Body.Head6 == null)
            {
                Body.Head6 = Main.npc[NPC.whoAmI];
            }
            if (NPC.type == ModContent.NPCType<HydraHead7>() && Body.Head7 == null)
            {
                Body.Head7 = Main.npc[NPC.whoAmI];
            }
            if (NPC.type == ModContent.NPCType<HydraHead8>() && Body.Head8 == null)
            {
                Body.Head8 = Main.npc[NPC.whoAmI];
            }
            if (NPC.type == ModContent.NPCType<HydraHead9>() && Body.Head9 == null)
            {
                Body.Head9 = Main.npc[NPC.whoAmI];
            }
        }

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player player = Main.player[NPC.target];
                bool Red = Head == 5 || Head == 8;
                bool Yellow = Head == 4 || Head == 6;
                bool Blue = Head == 3 || Head == 7;
                bool Green = Head == 0;
                bool Orange = Head == 1;
                bool Purple = Head == 2;

                Shoot++;

                int Interval =
                    Red ? 120 :
                    Yellow ? 180 :
                    Blue ? 210 :
                    Green ? 195 :
                    Orange ? 150 :
                    Purple ? 165 :
                    210;

                int proj =
                    Red ? ModContent.ProjectileType<HydraHead_HydraBreath>() :
                    Yellow ? ModContent.ProjectileType<AcidProj>() :
                    ModContent.ProjectileType<HydraHead_HydraBomb>();

                if (Green)
                {
                    proj = Main.rand.NextBool(2) ? ModContent.ProjectileType<AcidProj>() : ModContent.ProjectileType<HydraHead_HydraBomb>();
                }
                if (Orange)
                {
                    proj = Main.rand.NextBool(2) ? ModContent.ProjectileType<AcidProj>() : ModContent.ProjectileType<HydraHead_HydraBreath>();
                }
                if (Purple)
                {
                    proj = Main.rand.NextBool(2) ? ModContent.ProjectileType<HydraHead_HydraBomb>() : ModContent.ProjectileType<HydraHead_HydraBreath>();
                }

                if (Shoot == Interval)
                {
                    BaseAI.FireProjectile(player.position, NPC.position, proj, NPC.damage / 4, 2, 10, -1, Main.myPlayer);
                }

                if (Shoot >= Interval + 60)
                {
                    Shoot = 0;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            bool Red = Head == 5 || Head == 8;
            bool Yellow = Head == 4 || Head == 6;
            bool Blue = Head == 3 || Head == 7;
            bool Green = Head == 0;
            bool Orange = Head == 1;
            bool Purple = Head == 2;

            int Interval =
                Red ? 120 :
                Yellow ? 180 :
                Blue ? 210 :
                Green ? 195 :
                Orange ? 150 :
                Purple ? 165 :
                210;
            if (Shoot >= Interval)
            {
                NPC.frame.Y = 54;
            }
            else
            {
                NPC.frame.Y = 0;
            }
        }

        public Vector2 HeadPos()
        {
            switch (Head)
            {
                default:
                    return new Vector2(0, -110);
                case 1:
                    return new Vector2(80, -100);
                case 2:
                    return new Vector2(-80, -100);
                case 3:
                    return new Vector2(-30, -110);
                case 4:
                    return new Vector2(30, -110);
                case 5:
                    return new Vector2(70, -100);
                case 6:
                    return new Vector2(90, -90);
                case 7:
                    return new Vector2(-70, -100);
                case 8:
                    return new Vector2(-90, -90);
            }
        }

        public float moveSpeed = 16f; 
        public void MoveToPoint(Vector2 point)
        {
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }

        public override bool PreKill()
        {
            if (bodyNPC != null || NPC.AnyNPCs(ModContent.NPCType<HydraBody>()))
            {
                if (NPC.type == ModContent.NPCType<HydraHead1>())
                {
                    int a = NPC.NewNPC(NPC.GetSource_Death(), (int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead4>(), 0, bodyNPC.whoAmI);
                    Body.Head4 = Main.npc[a];
                    int b = NPC.NewNPC(NPC.GetSource_Death(), (int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead5>(), 0, bodyNPC.whoAmI);
                    Body.Head5 = Main.npc[b];
                    return false;
                }
                if (NPC.type == ModContent.NPCType<HydraHead2>())
                {
                    int a = NPC.NewNPC(NPC.GetSource_Death(), (int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead6>(), 0, bodyNPC.whoAmI);
                    Body.Head6 = Main.npc[a];
                    int b = NPC.NewNPC(NPC.GetSource_Death(), (int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead7>(), 0, bodyNPC.whoAmI);
                    Body.Head7 = Main.npc[b];
                    return false;
                }
                if (NPC.type == ModContent.NPCType<HydraHead3>())
                {
                    int a = NPC.NewNPC(NPC.GetSource_Death(), (int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead8>(), 0, bodyNPC.whoAmI);
                    Body.Head8 = Main.npc[a];
                    int b = NPC.NewNPC(NPC.GetSource_Death(), (int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead9>(), 0, bodyNPC.whoAmI);
                    Body.Head9 = Main.npc[b];
                    return false;
                }
            }
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreHead").Type, 1f);
            }
        }
    }
}

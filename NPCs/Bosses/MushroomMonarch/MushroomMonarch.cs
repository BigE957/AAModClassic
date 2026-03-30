using System.IO;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using AAModClassic.Items.Boss.MushroomMonarch;
using AAModClassic.Items.Usable;
using AAModClassic.Items.Vanity.Mask;


namespace AAModClassic.NPCs.Bosses.MushroomMonarch
{
    [AutoloadBossHead]
    public class MushroomMonarch : ModNPC
    {
		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if(Main.netMode == NetmodeID.Server || Main.dedServ)
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
			if(Main.netMode == NetmodeID.MultiplayerClient)
			{
				internalAI[0] = reader.ReadFloat();
				internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }	
		}	

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mushroom Monarch");
            Main.npcFrameCount[NPC.type] = 12;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 1200;   //boss life
            NPC.damage = 24;  //boss damage
            NPC.defense = 12;    //boss defense
            NPC.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            NPC.value = Item.sellPrice(0, 0, 50, 0);
            NPC.aiStyle = -1;
            NPC.width = 74;
            NPC.height = 108;
            NPC.npcSlots = 1f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.buffImmune[46] = true;
            NPC.buffImmune[47] = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch");

        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public static int AISTATE_WALK = 0, AISTATE_JUMP = 1, AISTATE_CHARGE = 2, AISTATE_FLY = 3;
		public float[] internalAI = new float[4];
		
        public override void AI()
        {
            NPC.TargetClosest();

            Player player = Main.player[NPC.target];

            if (player == null)
            {
                NPC.TargetClosest();
            }

            if (player.dead || !player.active || Vector2.Distance(player.Center, NPC.Center) > 5000)
            {
                NPC.TargetClosest();

                if (player.dead || !player.active || Vector2.Distance(player.Center, NPC.Center) > 5000)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 0f), Mod.Find<ModProjectile>("MonarchRUNAWAY").Type, 0, 0);
                    NPC.active = false;
                    return;
                }
            }

            float dist = NPC.Distance(player.Center);

            NPC.frameCounter++;
            if (internalAI[1] != AISTATE_JUMP && internalAI[1] != AISTATE_FLY) //walk or charge
            {
                int FrameSpeed = 10;
                if (internalAI[1] == AISTATE_CHARGE)
                {
                    FrameSpeed = 6;
                }

                if (NPC.frameCounter >= FrameSpeed)
				{
					NPC.frameCounter = 0;
					NPC.frame.Y += 108;
					if (NPC.frame.Y > (108 * 4))
					{
						NPC.frameCounter = 0;
						NPC.frame.Y = 0;
					}
				}
                if(NPC.velocity.Y != 0)
                {
                    if (NPC.velocity.Y < 0)
                    {
                        NPC.frame.Y = 648;
                    }else
                    if (NPC.velocity.Y > 0)
                    {
                        NPC.frame.Y = 756;
                    }
                }
            }
            else if (internalAI[1] == AISTATE_FLY)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 108;
                if (NPC.frame.Y > (108 * 11) || NPC.frame.Y < (108 * 8))
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 108 * 8;
                }

            }
            else //jump
            {
                if (NPC.velocity.Y == 0)
                {
                    NPC.frame.Y = 540;
                }else
                {
                    if (NPC.velocity.Y < 0)
                    {
                        NPC.frame.Y = 648;
                    }else
                    if (NPC.velocity.Y > 0)
                    {
                        NPC.frame.Y = 756;
                    }
                }
            }
            if (player.Center.X > NPC.Center.X) // so it faces the player
            {
                NPC.spriteDirection = -1;
            }else
            {
                NPC.spriteDirection = 1;
            }

            if (NPC.collideX && NPC.velocity.Y <= 0)
            {
                NPC.velocity.Y = -4f;
                internalAI[1] = AISTATE_CHARGE;
            }
            else if (((player.Center.Y - NPC.Center.Y) < -150f && (internalAI[1] == AISTATE_WALK || internalAI[1] == AISTATE_CHARGE)) || Collision.SolidCollision(new Vector2(NPC.Center.X, NPC.position.Y - NPC.height/2 + 10), NPC.width, NPC.height))
            {
                internalAI[1] = AISTATE_FLY;
                NPC.ai = new float[4];
                NPC.netUpdate = true;
            }
            else if ((player.Center.Y - NPC.Center.Y) > 100f && internalAI[1] != AISTATE_FLY) // player is below the npc.
            {
                internalAI[3] = internalAI[1]; //record the action
                internalAI[1] = AISTATE_WALK;
                NPC.ai = new float[4];
                NPC.netUpdate = true;
            }
            else if(internalAI[1] != AISTATE_WALK)
            {
                internalAI[3] = internalAI[1];
            }
            else
            {
                internalAI[1] = internalAI[3];
            }

            
			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
                if (internalAI[1] != AISTATE_FLY)
                {
                    internalAI[0]++;
                }
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
			}
			if(internalAI[1] == AISTATE_WALK) //fighter
			{
                NPC.noGravity = false;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI[2]++;
                }
                if ((player.Center.Y - NPC.Center.Y) > 120f) // player is below the npc.
                {
                    NPC.noTileCollide = true;
                }
                else
                {
                    NPC.noTileCollide = false;
                }

                if (NPC.CountNPCS(ModContent.NPCType<RedMushling>()) < 4)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int Minion = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<RedMushling>(), 0);
                        Main.npc[Minion].netUpdate = true;
                    }
                    internalAI[2] = 0;
                }
                AAAI.InfernoFighterAI(NPC, ref NPC.ai, true, false, 0, 0.07f, 3f, 3, 4, 60, true, 10, 60, true, null, false);	
			}else
			if(internalAI[1] == AISTATE_JUMP)//jumper
			{
                NPC.noGravity = false;
                NPC.noTileCollide = false;
                if(NPC.ai[0] < -10) NPC.ai[0] = -10; //force rapid jumping
                BaseAI.AISlime(NPC, ref NPC.ai, true, 30, 6f, -8f, 6f, -10f);
								
			}
            else if (internalAI[1] == AISTATE_FLY)//fly
            {
                NPC.noTileCollide = true;
                NPC.noGravity = true;
                if((player.Center.Y - NPC.Center.Y) > 60f)
                {  
                    if (NPC.CountNPCS(ModContent.NPCType<RedMushling>()) < 6)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            int Minion = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<RedMushling>(), 0);
                            Main.npc[Minion].netUpdate = true;
                        }
                    }
                    MoveToPoint(player.Center);
                    
                }
                else
                {
                    BaseAI.AISpaceOctopus(NPC, ref NPC.ai, .05f, 8, 250, 0, null);
                }
                
                
                NPC.rotation = 0;
                if ((player.Center.Y - NPC.Center.Y) > 30f && !Collision.SolidCollision(new Vector2(NPC.Center.X, NPC.position.Y - NPC.height/2 + 10), NPC.width, NPC.height))
                {
                    NPC.rotation = 0;
                    NPC.noGravity = false;
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                    NPC.noTileCollide = false;
                }
            }
            else //charger
			{
                BaseAI.AICharger(NPC, ref NPC.ai, 0.07f, 10f, false, 30);				
			}
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if(Main.rand.Next(5) == 0)
            {
                if(Main.rand.Next(10) == 0)
                {
                    int i = Item.NewItem(NPC.GetSource_OnHurt(projectile), (int)NPC.Center.X, (int)NPC.Center.Y, 16, 16, 5, 1, false, 0, false, false);
                    if (Main.netMode == NetmodeID.MultiplayerClient && i > 0)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, i, 1f, 0f, 0f, 0, 0, 0);
                    }
                }
                else
                {
                    Projectile.NewProjectile(NPC.GetSource_OnHurt(projectile), NPC.Center, new Vector2(0f, 0f), Mod.Find<ModProjectile>("FakeMonarchMushroom").Type, 0, 0);
                }
            }
        }

        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if(Main.rand.Next(5) == 0)
            {
                if(Main.rand.Next(10) == 0)
                {
                    int i = Item.NewItem(NPC.GetSource_OnHurt(player), (int)NPC.Center.X, (int)NPC.Center.Y, 16, 16, 5, 1, false, 0, false, false);
                    if (Main.netMode == NetmodeID.MultiplayerClient && i > 0)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, i, 1f, 0f, 0f, 0, 0, 0);
                    }
                }
                else
                {
                    Projectile.NewProjectile(NPC.GetSource_OnHurt(player), NPC.Center, new Vector2(0f, 0f), Mod.Find<ModProjectile>("FakeMonarchMushroom").Type, 0, 0);
                }
            }
        }
        
        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 8f;
            if (Vector2.Distance(NPC.Center, point) > 500)
            {
                moveSpeed = 12f;
            }
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
        }

        public override void OnKill()
        {
            AAWorld.downedMonarch = true;
            Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, new Vector2(0f, 0f), Mod.Find<ModProjectile>("MonarchRUNAWAY").Type, 0, 0);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<MonarchBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MonarchTrophy>(), 10));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SporeSac>(), 1, 30, 35));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MonarchMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Mushium>(), 1, 25, 35));

            npcLoot.Add(notExpertRule);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);  //boss life scale in expertmode
            NPC.damage = (int)(NPC.damage * 0.6f);
        }
    }
}



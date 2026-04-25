using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using AAModClassic.Base.BaseMod.Base;
using Terraria.ID;
using AAModClassic.UI.WorldGen;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero
{
    [AutoloadBossHead]
    public class InfinityZeroHand1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zero Unit");
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.life = NPC.lifeMax = 90000;
            NPC.height = NPC.width = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) ? 128 : 206;
            NPC.npcSlots = 0;
			NPC.aiStyle = -1;
            NPC.dontCountMe = true;
            NPC.noTileCollide = true;
            NPC.boss = false;
            NPC.noGravity = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            RepairMode = false;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

		public InfinityZero Body = null;
		public int handType = 0; //0 == left top, 1 == left middle, 2 == left bottom, 3 == right top, 4 == right middle, 5 == right bottom
		public bool leftHand= true;
        public static bool RepairMode = false;

        public static int damageIdle = 200;
		public static int damageCharging = 300;
		
        public bool killedbyplayer = true;	
		

        public bool ChargeAttack //actually charging the player
		{
			get
			{
				return NPC.ai[1] == 1;
			}
			set
			{
				float oldValue = NPC.ai[1];
				NPC.ai[1] = value ? 1f : 0f;
				if(NPC.ai[1] != oldValue) NPC.netUpdate = true;
			}
		}
        public bool Charging //preparing to charge the player
		{
			get
			{
				return NPC.ai[1] == 1.5f;
			}
			set
			{
				float oldValue = NPC.ai[1];
				NPC.ai[1] = value ? 1.5f : 0f;
				if(NPC.ai[1] != oldValue) NPC.netUpdate = true;
			}
		}		
		public int chargeTimer = 0;
		
		
		
		public int distFromBodyX = 200; 
		public int distFromBodyY = 150;
		public int movementVariance = 60;
        public int movementtimer = 0;
        public bool direction = false;
        public int chargeTime = 100;

		public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);				
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                customAI[0] = reader.ReadSingle();
                customAI[1] = reader.ReadSingle();
                customAI[2] = reader.ReadSingle();
                customAI[3] = reader.ReadSingle();				
            }
        }

        private int ZeroShot = 0;
        
        public override void AI()
		{
            if(RepairMode)
                NPC.life = NPC.lifeMax;

            int num429 = 1;
            if (NPC.position.X + NPC.width / 2 < Main.player[NPC.target].position.X + Main.player[NPC.target].width)
            {
                num429 = -1;
            }
            Vector2 PlayerDistance = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float PlayerPosX = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 + num429 * 180 - PlayerDistance.X;
            float PlayerPosY = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - PlayerDistance.Y;
            float PlayerPos = (float)Math.Sqrt(PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY);
            float num433 = 6f;
            PlayerPosX = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - PlayerDistance.X;
            PlayerPosY = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - PlayerDistance.Y;
            PlayerPos = (float)Math.Sqrt(PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY);
            PlayerPos = num433 / PlayerPos;
            PlayerPosX *= PlayerPos;
            PlayerPosY *= PlayerPos;
            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosY += NPC.velocity.Y * 0.5f;
            PlayerPosX += NPC.velocity.X * 0.5f;
            PlayerDistance.X -= PlayerPosX * 1f;
            PlayerDistance.Y -= PlayerPosY * 1f;

            ZeroShot++;
            
            int aiTimerShoot = NPC.whoAmI % 3 == 0 ? 50 : NPC.whoAmI % 2 == 0 ? 150 : 100; //aiTimerFire is different per head by using whoAmI (which is usually different) 
            if (leftHand) aiTimerShoot += 30;
            if (ZeroShot >= aiTimerShoot)
            {
                ZeroShot = 0;
                if (!ChargeAttack || !RepairMode)
                {
                    float rotation = MathHelper.ToRadians(20);
                    for (int i = 0; i < 3 + 1; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), PlayerDistance.X, PlayerDistance.Y, PlayerPosX, PlayerPosY, ModContent.ProjectileType<InfinityZero_InfinityZeroShot>(), 140, 0, Main.myPlayer);
                    }
                }
            }

            if (RepairMode)
            {
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;
            }
            if (Body != null && Body.Reseting)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.life = 0;
                    NPC.checkDead();
                    NPC.netUpdate = true;
                    killedbyplayer = false;
                    Body.Reseting = false;
                }
                return;
            }
            Vector2 vectorCenter = NPC.Center;
            if (Body == null)
			{
				NPC npcBody = Main.npc[(int)NPC.ai[0]];
				if(npcBody.type == ModContent.NPCType<InfinityZero>())
				{
					Body = (InfinityZero)npcBody.ModNPC;
				}
				handType = (int)NPC.ai[1];
				NPC.localAI[3] = 30 * handType; //so they start at different rotation points
				Vector2 point = GetVariance(false);
				customAI[1] = point.X;
				customAI[2] = point.Y;
				NPC.netUpdate = true;
			}
            if (Body.NPC.active && NPC.timeLeft < 10)
            {
                NPC.timeLeft = 10;
            }
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
            if (!Body.NPC.active)
            {
				if(NPC.timeLeft > 10) NPC.timeLeft = 10;
                killedbyplayer = false;
                return;
            }
			NPC.TargetClosest();
			Player targetPlayer = Main.player[NPC.target];
			if(targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null

			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				customAI[0]++;
				int aiTimerFire = NPC.whoAmI % 3 == 0 ? 250 : NPC.whoAmI % 2 == 0 ? 250 : 200; //aiTimerFire is different per head by using whoAmI (which is usually different) 
				if(leftHand) aiTimerFire += 60;

				if(customAI[0] >= 150 && customAI[3] == 0) //pick random spot to move head to
				{
					NPC.damage = damageIdle;
					Vector2 movementVector = GetVariance();
                    ChargeAttack = false;
					customAI[0] = 0;
					customAI[1] = movementVector.X;
					customAI[2] = movementVector.Y;
					NPC.netUpdate = true;
					customAI[3] = Main.rand.NextBool(3) ? 1 : 0; //wether or not to charge
                }else
				if(targetPlayer != null && customAI[0] >= aiTimerFire) //get ready to charge player
				{
                    Charging = true;
                    chargeTimer += 1;
                    if (chargeTimer >= chargeTime) //actually charge player
                    {
						ChargeAttack = true;
						Vector2 diff = targetPlayer.Center - NPC.Center;
						//diff = (Vector2.Normalize(diff) * 120);
						if(Vector2.Distance(NPC.Center + diff, NPC.Center) > 2000f) //point is too far away from the body
						{
							diff = GetVariance(false);
						}else
						{
							NPC.damage = damageCharging;
						}
                        customAI[0] = 0f;
                        customAI[1] = diff.X;
                        customAI[2] = diff.Y;
						chargeTimer = 0;
                    }
                }
            }

			//random rotation code
			if(NPC.frame.Y == 0 && !ChargeAttack && !Charging)
			{
				NPC.localAI[3] += Main.rand.Next(3);
				if(NPC.localAI[3] > 150)
				{
					NPC.rotation += MathHelper.Lerp(0.3f, 0.005f, NPC.rotation / ((float)Math.PI * 2));
					if(NPC.rotation >= (float)Math.PI * 2)
					{
						NPC.localAI[3] = 0;
						NPC.rotation = 0f;
					}
				}else
				{
					NPC.rotation = 0f;
				}
			}else
			{
				NPC.localAI[3] = 0;
				if(targetPlayer != null && !ChargeAttack)
				{
					NPC.velocity = targetPlayer.Center - NPC.Center;
					NPC.velocity = Vector2.Normalize(NPC.velocity) * 0.005f;
				}
				NPC.rotation = BaseUtility.RotationTo(NPC.Center, NPC.Center + NPC.velocity);
			}

            Vector2 nextTarget = Body.NPC.Center + new Vector2(customAI[1], customAI[2]);
			if(Vector2.Distance(nextTarget, NPC.Center) < 60f)
			{
				if(ChargeAttack)
				{
					NPC.velocity *= 1.5f; //slow WAY the fuck down (testing to see if speedy)
					if(Main.netMode != NetmodeID.MultiplayerClient)
					{
						ChargeAttack = false;
						Vector2 point = GetVariance(false);
						customAI[1] = point.X;
						customAI[2] = point.Y;
						NPC.netUpdate = true;
					}
				}
				NPC.velocity *= 0.9f;
				if(Math.Abs(NPC.velocity.X) < 0.05f) NPC.velocity.X = 0f;
				if(Math.Abs(NPC.velocity.Y) < 0.05f) NPC.velocity.Y = 0f;
			}else
			{
				NPC.velocity = Vector2.Normalize(nextTarget - NPC.Center);
				NPC.velocity *= ChargeAttack ? 18f : 8f;
			}
			NPC.position += Body.NPC.oldPos[0] - Body.NPC.position;
            //npc.spriteDirection = -1; commented out temporarily	
        }

        public Vector2 GetVariance(bool random = true)
		{
			float offsetX = 0, offsetY = 0;
			switch(handType)
			{
				case 0: offsetX = -distFromBodyX - 100; offsetY = -distFromBodyY; break;
				case 1: offsetX = -distFromBodyX - 50; offsetY = 0; break;
				case 2: offsetX = -distFromBodyX; offsetY = distFromBodyY; break;
				case 3: offsetX = distFromBodyX + 100; offsetY = -distFromBodyY; break;
				case 4: offsetX = distFromBodyX + 50; offsetY = 0; break;
				case 5: offsetX = distFromBodyX; offsetY = distFromBodyY; break;		
				default: break;
			}
			if(random)
			{
				offsetX += Main.rand.Next(-movementVariance, movementVariance); 
				offsetY += Main.rand.Next(-movementVariance, movementVariance); 
			}
			return new Vector2(offsetX, offsetY);
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                NPC.life = NPC.lifeMax;
                RepairMode = true;
                Body.NPC.ai[3] = 6;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            //npc.frameCounter++;
            if (ChargeAttack || Charging)
            {
                NPC.frame.Y = 1 * frameHeight;
            }else
            if (RepairMode)
            {
                NPC.frame.Y = 2 * frameHeight;
            }
            else
            {
				NPC.frame.Y = 0;
                //npc.frameCounter = 0;
            }
        }
        
		public override bool PreKill()
        {
            return false;
        }
    }
}

using AAModClassic._Content.Void.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Vanity.VoidEye;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero
{
    [AutoloadBossHead]
    public class InfinityZeroHand1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chained Zero");
            Main.npcFrameCount[NPC.type] = 3;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
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
            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.InfinityZeroChainedZero")
            ]);
        }

        public InfinityZero Body = null;
		public int handType = 0; //0 == left top, 1 == left middle, 2 == left bottom, 3 == right top, 4 == right middle, 5 == right bottom
		public bool leftHand = true;
        public bool RepairMode = false;

        public static int damageIdle = 200;
		public static int damageCharging = 300;

        private bool ChargeAttack //actually charging the player
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
        private bool Charging //preparing to charge the player
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

        private static int DistFromBodyX => 200;
        private static int DistFromBodyY => 150;
        private static int MovementVariance => 60;
        private static int ChargeTime => 100;

        private int chargeUpCounter = 0;
        private Vector2 goalOffset = Vector2.Zero;
        private int chargeCounter = 0;
        private bool shouldCharge = false;

        private Vector2 startPosition = Vector2.Zero;
        private int chargingCounter = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(chargingCounter);
                writer.Write(chargeUpCounter);
                writer.Write(chargeCounter);
                writer.Write(goalOffset.X);
                writer.Write(goalOffset.Y);
                writer.Write(shouldCharge);				
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                chargingCounter = reader.ReadInt32();
                chargeUpCounter = reader.ReadInt32();
                chargeCounter = reader.ReadInt32();
                goalOffset.X = reader.ReadSingle();
                goalOffset.Y = reader.ReadSingle();
                shouldCharge = reader.ReadBoolean();				
            }
        }

        private int ZeroShot = 0;
        
        public override void AI()
		{
            bool unofficial = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            if (RepairMode)
                NPC.life = NPC.lifeMax;

            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            ZeroShot++;
            
            int aiTimerShoot = NPC.whoAmI % 3 == 0 ? 50 : NPC.whoAmI % 2 == 0 ? 150 : 100; //aiTimerFire is different per head by using whoAmI (which is usually different) 
            if (leftHand) 
                aiTimerShoot += 30;
            if (ZeroShot >= aiTimerShoot)
            {
                ZeroShot = 0;
                if (!ChargeAttack || !RepairMode)
                {
                    Vector2 velocity = player.Center - NPC.Center;
                    float speed = 6f / velocity.Length();
                    velocity *= speed;
                    velocity += Main.rand.NextVector2Circular(0.4f, 0.4f);
                    velocity += NPC.velocity * 0.5f;

                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, velocity, ModContent.ProjectileType<InfinityZero_InfinityZeroShot>(), 140, 0);
                }
            }

            if (RepairMode)
                NPC.dontTakeDamage = true;
            else
                NPC.dontTakeDamage = false;

            #region Refernce Setup and Existence Checks
            if (Body != null && Body.Reseting)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.life = 0;
                    NPC.checkDead();
                    NPC.netUpdate = true;
                    Body.Reseting = false;
                }
                return;
            }
            if (Body == null)
			{
				NPC npcBody = Main.npc[(int)NPC.ai[0]];
				if(npcBody.type == ModContent.NPCType<InfinityZero>())
				{
					Body = (InfinityZero)npcBody.ModNPC;
				}
				handType = (int)NPC.ai[1];
				NPC.localAI[3] = 30 * handType; //so they start at different rotation points
                goalOffset = GetVariance(false);
				NPC.netUpdate = true;
			}
            
            if (Body.NPC.active && NPC.timeLeft < 10)
                NPC.timeLeft = 10;

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
            #endregion

            Player targetPlayer = player;
            bool playerAvailable = !(targetPlayer == null || !targetPlayer.active || targetPlayer.dead);

			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				chargeCounter++;
				int aiTimerFire = NPC.whoAmI % 3 == 0 ? 250 : NPC.whoAmI % 2 == 0 ? 250 : 200; //aiTimerFire is different per head by using whoAmI (which is usually different) 
				if(leftHand)
                    aiTimerFire += 60;

                if (chargeCounter >= 150 && !shouldCharge) //pick random spot to move head to
				{
					NPC.damage = damageIdle;
                    goalOffset = GetVariance();
                    ChargeAttack = false;
					chargeCounter = 0;
					NPC.netUpdate = true;
                    shouldCharge = Main.rand.NextBool(3); //wether or not to charge
                }
                else if(playerAvailable && chargeCounter >= aiTimerFire) //get ready to charge player
				{
                    Charging = true;
                    chargeUpCounter += 1;
                    if (chargeUpCounter >= ChargeTime) //actually charge player
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
                            if(unofficial)
                                NPC.velocity = NPC.DirectionTo(targetPlayer.Center) * (RepairMode ? 24 : 36f);
						}
                        chargeCounter = 0;
                        goalOffset = diff;
						chargeUpCounter = 0;
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
				}
                else
					NPC.rotation = 0f;
			}
            else
			{
				NPC.localAI[3] = 0;
				if(playerAvailable && !ChargeAttack)
					NPC.velocity = Vector2.Normalize(targetPlayer.Center - NPC.Center) * 0.005f;
				NPC.rotation = NPC.velocity.ToRotation();
			}

            if (unofficial)
                NPC.knockBackResist = ChargeAttack ? 0f : 1f;

            Vector2 destination = Body.NPC.Center + goalOffset;
            if (ChargeAttack)
            {
                if (unofficial)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && chargingCounter > 60)
                    {
                        ChargeAttack = false;
                        goalOffset = GetVariance(false);
                        NPC.netUpdate = true;
                        chargingCounter = 0;
                    }
                    else
                    {
                        NPC.velocity *= 0.975f;// Vector2.Normalize(destination - NPC.Center) * 18;
                        chargingCounter++;
                    }
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && Vector2.Distance(destination, NPC.Center) < 60f)
                    {
                        ChargeAttack = false;
                        goalOffset = GetVariance(false);
                        NPC.netUpdate = true;
                    }
                    else
                    {
                        NPC.velocity = Vector2.Normalize(destination - NPC.Center) * 18;
                    }
                }
            }
            else
            {
                if(unofficial)
                    NPC.velocity = (destination - NPC.Center) / 30f;
                else
                {
                    if (Vector2.Distance(destination, NPC.Center) < 60f)
                        NPC.velocity = Vector2.Normalize(destination - NPC.Center) * 8f;
                    else
                        NPC.velocity *= 0.98f;
                }
            }

            NPC.position += Body.NPC.oldPos[0] - Body.NPC.position;
        }

        public Vector2 GetVariance(bool random = true)
		{
			float offsetX = 0, offsetY = 0;
			switch(handType)
			{
				case 0: offsetX = -DistFromBodyX - 100; offsetY = -DistFromBodyY; break;
				case 1: offsetX = -DistFromBodyX - 50; offsetY = 0; break;
				case 2: offsetX = -DistFromBodyX; offsetY = DistFromBodyY; break;
				case 3: offsetX = DistFromBodyX + 100; offsetY = -DistFromBodyY; break;
				case 4: offsetX = DistFromBodyX + 50; offsetY = 0; break;
				case 5: offsetX = DistFromBodyX; offsetY = DistFromBodyY; break;		
				default: break;
			}
			if(random)
			{
				offsetX += Main.rand.Next(-MovementVariance, MovementVariance); 
				offsetY += Main.rand.Next(-MovementVariance, MovementVariance); 
			}
			return new Vector2(offsetX, offsetY);
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                NPC.life = NPC.lifeMax;
                RepairMode = true;
                Body.NPC.ai[3]++;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                string texPath = "AAModClassic/_Unreleased/Content/Void/_PostMoonLord/NPCs/InfinityZero/InfinityZero_Resprite";

                Texture2D zeroTex = ModContent.Request<Texture2D>(texPath + "_Hand").Value;
                Texture2D glowTex = ModContent.Request<Texture2D>(texPath + "_Hand_Glow").Value;
                spriteBatch.Draw(zeroTex, NPC.Center - screenPos, NPC.frame, BaseUtility.ColorClamp(BaseDrawing.GetNPCColor(NPC), InfinityZero.GetGlowAlpha(true)), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);

                if (Body != null && Body.tenthHealth)
                {
                    DrawingUtils.DrawAura(spriteBatch, glowTex, NPC, Body.auraPercent, 1f, 0f, 0f, InfinityZero.GetGlowAlpha(true), true);
                    spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, InfinityZero.GetGlowAlpha(true), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
                }
                else
                    spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, InfinityZero.GetGlowAlpha(false), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
            }
            else
            {
                string zeroTexture = ModContent.GetInstance<InfinityZeroHand1>().Texture;
                string glowMaskTexture = zeroTexture + "_Glow";
                string ArmTex = Texture + "_Arm";
                Texture2D zeroTex = ModContent.Request<Texture2D>(zeroTexture).Value;
                Texture2D glowTex = ModContent.Request<Texture2D>(glowMaskTexture).Value;

                spriteBatch.Draw(zeroTex, NPC.Center - screenPos, NPC.frame, BaseUtility.ColorClamp(BaseDrawing.GetNPCColor(NPC), InfinityZero.GetGlowAlpha(true)), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
                if (Body != null && Body.tenthHealth)
                {
                    BaseDrawing.DrawAura(spriteBatch, glowTex, 0, NPC, Body.auraPercent, 1f, 0f, 0f, InfinityZero.GetGlowAlpha(true), true);
                    spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, InfinityZero.GetRedAlpha(), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
                }
                else
                    spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, InfinityZero.GetGlowAlpha(false), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
            }
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

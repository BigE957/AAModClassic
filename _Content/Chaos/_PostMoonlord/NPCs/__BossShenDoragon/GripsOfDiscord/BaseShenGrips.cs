using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.GripsOfDiscord
{
    public abstract class BaseShenGrips : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grip of Discord");
            Main.npcFrameCount[NPC.type] = 14;
        }

        public int damage = 0;

        public override void SetDefaults()
        {
            NPC.width = 66;
            NPC.height = 60;			
            NPC.aiStyle = -1;
			NPC.knockBackResist = 0f;	
            NPC.value = Item.buyPrice(0, 4, 50, 0);
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
            NPC.scale *= 1.4f;
            Music = MusicManagementSystem.MusicSlots["Shen"];
        }
        
        public override void FindFrame(int frameHeight)
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<ShenDoragon>()))
            {
                NPC.life = 0;
            }
            NPC.frameCounter++;
            if (NPC.frameCounter > 9)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
                if (NPC.ai[0] == 2 || NPC.ai[0] == 3)
                {
                    if (NPC.frame.Y < 4 * frameHeight || NPC.frame.Y > 7 * frameHeight)
                    {
                        NPC.frame.Y = 4 * frameHeight;
                    }

                }
                else if (NPC.ai[0] == 5)
                {
                    if (NPC.frame.Y < 8 * frameHeight || NPC.frame.Y > 11 * frameHeight)
                    {
                        NPC.frame.Y = 8 * frameHeight;
                    }
                }
                else if (NPC.ai[0] == 6)
                {
                    NPC.frame.Y = frameHeight * 8;
                    if (internalAI[0] > 8)
                    {NPC.frame.Y = frameHeight * 9;}
                    if (internalAI[0] > 16)
                    {NPC.frame.Y = frameHeight * 10;}
                    if (internalAI[0] > 24)
                    { NPC.frame.Y = frameHeight * 11; }
                    if (internalAI[0] > 32)
                    { NPC.frame.Y = frameHeight * 12; }
                    if (internalAI[0] > 40)
                    { NPC.frame.Y = frameHeight * 13 ;}
                }
                else
                {
                    if (NPC.frame.Y > 3 * frameHeight)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = 0;
        }


        public override bool CheckActive()
        {
            return !NPC.AnyNPCs(ModContent.NPCType<ShenDoragon>());
        }


        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
            NPC.damage = (int)(NPC.damage * 0.8f);
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

        public Vector2 Keepmove = Vector2.Zero;
		public float moveSpeed = 14f;
        public int MinionTimer = 0;

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

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if(NPC.ai[0] == 3)
            {
                if(NPC.type == ModContent.NPCType<BlazeGrip>())
                {
                    BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1.5f, 1f, 3, false, 0f, 0f, Color.Red);
                }
                else
                {
                    BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1.5f, 1f, 3, false, 0f, 0f, Color.Navy);
                }
            }
            return false;
        }

        public override void AI()
		{
            bool BlazeGrip = NPC.type == ModContent.NPCType<BlazeGrip>();

            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }
            NPC.TargetClosest();
			Player targetPlayer = Main.player[NPC.target];

            float ChangingPosX = 350f * (targetPlayer.Center.X > NPC.Center.X? 1:-1);
            float ChangingPosY = 350f * (targetPlayer.Center.Y > NPC.Center.Y? 1:-1);

            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                NPC.TargetClosest(false);
                DespawnHandler();
                return;
            }

			if(NPC.ai[0] == 1) //move to starting charge position
			{
				moveSpeed = 14f;
				Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -ChangingPosY);
				MoveToPoint(point);
                internalAI[0] ++;
				if(Main.netMode != NetmodeID.MultiplayerClient && (Vector2.Distance(NPC.Center, point) < 10f || internalAI[0] > 100))
				{
                    NPC.ai[0] = 4;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
					NPC.ai[3] = 0;
                    internalAI[0] = 0;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
					NPC.netUpdate = true;
				}
				BaseAI.LookAt(targetPlayer.Center, NPC, 0, 0f, 0.1f, false);			
			}else
			if(NPC.ai[0] == 2) //dive prepare
			{
                if(internalAI[2] >= 1) internalAI[2] ++;
				moveSpeed = 22f;
				Vector2 targetCenter = new Vector2(NPC.ai[1], NPC.ai[2]);
				Vector2 point = targetCenter + new Vector2(ChangingPosX, ChangingPosY);
                Keepmove = point;
                if(Keepmove != new Vector2(0,0))
                {
                    MoveToPoint(Keepmove);
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                        NPC.ai[0] = 3;			
                        NPC.netUpdate = true;
                    }
                } 
				else
                {
                    NPC.ai[0] = 0;
                    NPC.ai[3] = 0;				
                    NPC.netUpdate = true;
                }
				BaseAI.Look(NPC, 0, 0f, 0.1f, false);				
			}else
			if(NPC.ai[0] == 3) //diving
			{
                if(internalAI[2] >= 1) internalAI[2] ++;
				moveSpeed = 22f;
				MoveToPoint(Keepmove);
				if(Main.netMode != NetmodeID.MultiplayerClient && (Vector2.Distance(NPC.Center, Keepmove) < 10f || NPC.ai[3] ++ > 60))
				{
                    NPC.ai[0] = 2;
                    NPC.ai[1] = targetPlayer.Center.X;
                    NPC.ai[2] = targetPlayer.Center.Y;
                    NPC.ai[3] = 0;
                    if(internalAI[1] ++ > 4 && internalAI[2] == 0)
                    {
                        NPC.ai[0] = 0;
                        internalAI[1] = 0;
                    }
                    if(internalAI[2] > 200)
                    {
                        NPC.ai[0] = 6;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                    }
					NPC.netUpdate = true;
				}			
			}else
            if (NPC.ai[0] == 4) //Projectile skill
            {
                NPC.direction = NPC.spriteDirection = NPC.position.X < targetPlayer.position.X ? -1 : 1;
                NPC.rotation = NPC.DirectionTo(targetPlayer.Center).ToRotation() + (NPC.position.X < targetPlayer.position.X ? 0 : (float)Math.PI);
                moveSpeed = 14f;
				Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(-ChangingPosX, 0);
				MoveToPoint(point);
                internalAI[0] ++;
                if(internalAI[0] == 100 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if(BlazeGrip)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + 50f * Vector2.Normalize(NPC.DirectionTo(targetPlayer.Center)), new Vector2(0, 0), ModContent.ProjectileType<BlazeGrip_Clone>(), damage / 2, 0f, Main.myPlayer, NPC.whoAmI, 0);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + 50f * Vector2.Normalize(NPC.DirectionTo(targetPlayer.Center)) + 200f * Vector2.Normalize(NPC.DirectionTo(targetPlayer.Center).RotatedBy(Math.PI / 2)), new Vector2(0, 0), ModContent.ProjectileType<BlazeGrip_Clone>(), damage / 2, 0f, Main.myPlayer, NPC.whoAmI, 1f);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + 50f * Vector2.Normalize(NPC.DirectionTo(targetPlayer.Center)) + 400f * Vector2.Normalize(NPC.DirectionTo(targetPlayer.Center).RotatedBy(Math.PI / 2)), new Vector2(0, 0), ModContent.ProjectileType<BlazeGrip_Clone>(), damage / 2, 0f, Main.myPlayer, NPC.whoAmI, 2f);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + 50f * Vector2.Normalize(NPC.DirectionTo(targetPlayer.Center)) - 200f * Vector2.Normalize(NPC.DirectionTo(targetPlayer.Center).RotatedBy(Math.PI / 2)), new Vector2(0, 0), ModContent.ProjectileType<BlazeGrip_Clone>(), damage / 2, 0f, Main.myPlayer, NPC.whoAmI, -1f);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + 50f * Vector2.Normalize(NPC.DirectionTo(targetPlayer.Center)) - 400f * Vector2.Normalize(NPC.DirectionTo(targetPlayer.Center).RotatedBy(Math.PI / 2)), new Vector2(0, 0), ModContent.ProjectileType<BlazeGrip_Clone>(), damage / 2, 0f, Main.myPlayer, NPC.whoAmI, -2f);
                    }
                    else
                    {
                        for (int m = 0; m < 16; m++)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0, 0), ModContent.ProjectileType<AbyssGrip_Orbiter>(), 40, 0f, Main.myPlayer, NPC.whoAmI, 2f * (float)Math.PI / 16 * m);
                        }
                    }
                }
                
                if(internalAI[0] > 200)
                {
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[0] = 5;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        internalAI[0] = 0;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                    }
                    NPC.netUpdate = true;
                }
            }
            else
            if (NPC.ai[0] == 5) //Projectile skill2
            {
                if(BlazeGrip)
                {
                    if(internalAI[2] < 160)
                    {
                        BaseAI.LookAt(targetPlayer.Center, NPC, 0, 0f, 0.1f, false);
                        moveSpeed = 18f;
                        Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -ChangingPosY);
                        MoveToPoint(point);
                    }
                    else
                    {
                        NPC.direction = NPC.spriteDirection = NPC.position.X < targetPlayer.position.X ? -1 : 1;
                        NPC.rotation += (NPC.DirectionTo(targetPlayer.Center).ToRotation()  + (NPC.position.X < targetPlayer.position.X ? 0 : (float)Math.PI))/100f;
                        NPC.velocity = new Vector2(0,0);
                    }
                    

                    internalAI[2] ++;
                    if(internalAI[2] == 160 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 dir = Vector2.Normalize(targetPlayer.Center - NPC.Center);
                        float baseSpeed = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                        double startAngle = Math.Atan2(dir.X, dir.Y);
                        double deltaAngle = 45f * 0.0174f;
                        for (int i = -1; i < 2; i++)
                        {
                            double offsetAngle = startAngle + deltaAngle * i;
                            Vector2 shootdir = new Vector2(baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle));
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(shootdir), ModContent.ProjectileType<BlazeGrip_Deathray>(), 15, 0f, Main.myPlayer, i, NPC.whoAmI);
                        }
                    }
                    if(internalAI[2] > 200)
                    {
                        NPC.ai[0] = 6;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        internalAI[0] = 0;
                        internalAI[1] = 0;
                        internalAI[2] = 0;		
                        NPC.netUpdate = true;
                    }

                }
                else
                {
                    moveSpeed = 22f;
                    Vector2 targetCenter = NPC.position;
                    Vector2 point = targetCenter + new Vector2(ChangingPosX, ChangingPosY);
                    Keepmove = point;
                    if(Keepmove != new Vector2(0,0))
                    {
                        MoveToPoint(Keepmove);
                        if(Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                            NPC.ai[0] = 3;			
                            internalAI[2] ++;
                            internalAI[1] = 0;
                            NPC.netUpdate = true;
                        }
                    } 
                    else
                    {
                        Keepmove = targetCenter;
                    }
                }

            }
            else
            if (NPC.ai[0] == 6) //Fire Projectile (Shen Grips)
            {
                internalAI[0]++;
                if(BlazeGrip && internalAI[0] < 40)
                {
                    NPC.direction = NPC.spriteDirection = NPC.position.X < targetPlayer.position.X ? -1 : 1;
                    NPC.rotation += (NPC.DirectionTo(targetPlayer.Center).ToRotation()  + (NPC.position.X < targetPlayer.position.X ? 0 : (float)Math.PI))/100f;
                    NPC.velocity = new Vector2(0,0);
                }
                else
                {
                    moveSpeed = 22f;
                    Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -ChangingPosY);
                    MoveToPoint(point);
                }
                if (internalAI[0] == 40)
                {
                    BaseAI.FireProjectile(targetPlayer.Center, NPC.Center, BlazeGrip ? ModContent.ProjectileType<BlazeGrip_ScorchBomb>() : ModContent.ProjectileType<AbyssGrip_AbyssalBomb>(), damage, 2, 9f, -1, Main.myPlayer);
                }
                if (internalAI[0] > 50)
                {
                    NPC.ai[0] = 2;
                    NPC.ai[1] = targetPlayer.Center.X;
                    NPC.ai[2] = targetPlayer.Center.Y;
                    NPC.ai[3] = 0;
                    internalAI[0] = 0;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    NPC.netUpdate = true;
                }
                BaseAI.LookAt(targetPlayer.Center, NPC, 0, 0f, 0.1f, false);
            }
            else //standard movement
			{
				moveSpeed = 14f;
				Vector2 point = targetPlayer.Center + offsetBasePoint;
				MoveToPoint(point);
				if(Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.ai[1]++;
					if(NPC.ai[1] > 90)
					{
						NPC.ai[0] = 1;
						NPC.ai[1] = 0;	
						NPC.ai[2] = 0;
						NPC.ai[3] = 0;	
                        internalAI[1] = 0;
                        internalAI[2] = 0;			
						NPC.netUpdate = true;				
					}
				}		
				BaseAI.LookAt(targetPlayer.Center, NPC, 0, 0f, 0.1f, false);			
			}
            if (NPC.ai[0] == 0)
            {
                if (NPC.alpha >= 50)
                {
                    NPC.defense = 500;
                }
            }
            else
            {
                NPC.alpha -= 5;
                if (NPC.alpha <= 0)
                {
                    if (BlazeGrip)
                    {
                        NPC.defense = 110;
                    }
                    else
                    {
                        NPC.defense = 90;
                    }
                }
            }
        }

      

        public void MoveToPoint(Vector2 point)
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
            NPC.TargetClosest(false);
            Player player = Main.player[NPC.target];
            if (!player.active || player.dead || Main.dayTime)        // If the player is dead and not active, the npc flies off-screen and despawns
            {
                NPC.velocity.X = 0;
                NPC.velocity.Y -= 1;
            }
        }
    }
}

using System;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.CrossMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Enemies.BiomeGuardians
{
    public class Dynasaur : ModNPC
	{
		public override void SetStaticDefaults()
		{
            Main.npcFrameCount[NPC.type] = 8;
		}		
		
        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 38;
            NPC.value = BaseUtility.CalcValue(0, 0, 39, 50);
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 180;
            NPC.defense = 20;
            NPC.damage = 30;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.7f;	
        }

		public Color smokeColor = new Color(145, 55, 26);

		public static int[] velocitiesX = new int[] { -6, -3, 0, 3, 6, 3, 0, -3 };
		public static int[] velocitiesY = new int[] { 0, 3, 6, 3, 0, -3, -6, -3 };

		public override void OnKill()
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				for (int m = 0; m < 8; m++)
				{
                    BaseAI.FireProjectile(NPC.Center + new Vector2(velocitiesX[m], velocitiesY[m]), NPC.Center, Mod.ProjType("BugAcidShot"), 0, 0f, 5f);
				}
			}
            BaseAI.DropItem(NPC, Mod.Find<ModItem>("AcidSac").Type, 1 + Main.rand.Next(2) + (Main.expertMode ? 2 : 0), 2, 65, true);
			if(ModSupport.GetMod("CalamityMod") != null)
			{
                BaseAI.DropItem(NPC, ModSupport.GetModItem("CalamityMod", "BeetleJuice").Item.type, 1, 1, 65, true);
                BaseAI.DropItem(NPC, ModSupport.GetModItem("CalamityMod", "EssenceofCinder").Item.type, 1, 1, Main.expertMode ? 20 : 15, true);
			}
		}

        public float moveSpeed = 14f;
        public Vector2 MovePoint;
        public bool SelectPoint = false;

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.noGravity = true;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[3]++;
            }
            if (NPC.ai[3] > 240)
            {
                if (SelectPoint && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float Point = 500 * NPC.direction;
                    MovePoint = player.Center + new Vector2(Point, 500f);
                    SelectPoint = false;
                    NPC.netUpdate = true;
                }
                MoveToPoint(MovePoint);
                if (NPC.ai[3] > 300 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
            }
            else
            {
                if (NPC.collideX)
                {
                    NPC.velocity.X = NPC.oldVelocity.X * -0.5f;
                    if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
                    {
                        NPC.velocity.X = 2f;
                    }
                    if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
                    {
                        NPC.velocity.X = -2f;
                    }
                }
                if (NPC.collideY)
                {
                    NPC.velocity.Y = NPC.oldVelocity.Y * -0.5f;
                    if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f)
                    {
                        NPC.velocity.Y = 1f;
                    }
                    if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f)
                    {
                        NPC.velocity.Y = -1f;
                    }
                }
                NPC.TargetClosest(true);
                if (NPC.direction == -1 && NPC.velocity.X > -4f)
                {
                    NPC.velocity.X = NPC.velocity.X - 0.1f;
                    if (NPC.velocity.X > 4f)
                    {
                        NPC.velocity.X = NPC.velocity.X - 0.1f;
                    }
                    else if (NPC.velocity.X > 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + 0.05f;
                    }
                    if (NPC.velocity.X < -4f)
                    {
                        NPC.velocity.X = -4f;
                    }
                }
                else if (NPC.direction == 1 && NPC.velocity.X < 4f)
                {
                    NPC.velocity.X = NPC.velocity.X + 0.1f;
                    if (NPC.velocity.X < -4f)
                    {
                        NPC.velocity.X = NPC.velocity.X + 0.1f;
                    }
                    else if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - 0.05f;
                    }
                    if (NPC.velocity.X > 4f)
                    {
                        NPC.velocity.X = 4f;
                    }
                }
                if (NPC.directionY == -1 && NPC.velocity.Y > -1.5)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 0.04f;
                    if (NPC.velocity.Y > 1.5)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - 0.05f;
                    }
                    else if (NPC.velocity.Y > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + 0.03f;
                    }
                    if (NPC.velocity.Y < -1.5)
                    {
                        NPC.velocity.Y = -1.5f;
                    }
                }
                else if (NPC.directionY == 1 && NPC.velocity.Y < 1.5)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 0.04f;
                    if (NPC.velocity.Y < -1.5)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + 0.05f;
                    }
                    else if (NPC.velocity.Y < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - 0.03f;
                    }
                    if (NPC.velocity.Y > 1.5)
                    {
                        NPC.velocity.Y = 1.5f;
                    }
                }
                NPC.ai[1] += 1f;
                if (NPC.ai[1] > 200f)
                {
                    if (!Main.player[NPC.target].wet && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                    {
                        NPC.ai[1] = 0f;
                    }
                    float num205 = 0.2f;
                    float num206 = 0.1f;
                    float num207 = 4f;
                    float num208 = 1.5f;
                    if (NPC.ai[1] > 1000f)
                    {
                        NPC.ai[1] = 0f;
                    }
                    NPC.ai[2] += 1f;
                    if (NPC.ai[2] > 0f)
                    {
                        if (NPC.velocity.Y < num208)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num206;
                        }
                    }
                    else if (NPC.velocity.Y > -num208)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num206;
                    }
                    if (NPC.ai[2] < -150f || NPC.ai[2] > 150f)
                    {
                        if (NPC.velocity.X < num207)
                        {
                            NPC.velocity.X = NPC.velocity.X + num205;
                        }
                    }
                    else if (NPC.velocity.X > -num207)
                    {
                        NPC.velocity.X = NPC.velocity.X - num205;
                    }
                    if (NPC.ai[2] > 300f)
                    {
                        NPC.ai[2] = -300f;
                    }
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[0] += 1f;
                    if (NPC.ai[0] == 20f || NPC.ai[0] == 40f || NPC.ai[0] == 60f || NPC.ai[0] == 80f || NPC.ai[0] == 100f)
                    {
                        if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                        {
                            float num223 = 0.2f;
                            Vector2 value2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                            float num224 = Main.player[NPC.target].position.X + Main.player[NPC.target].width * 0.5f - value2.X + Main.rand.Next(-50, 51);
                            float num225 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height * 0.5f - value2.Y + Main.rand.Next(-50, 51);
                            float num226 = (float)Math.Sqrt(num224 * num224 + num225 * num225);
                            num226 = num223 / num226;
                            num224 *= num226;
                            num225 *= num226;
                            int num227 = 80;
                            value2 += NPC.velocity * 5f;
                            int num229 = Projectile.NewProjectile(value2.X + num224 * 100f, value2.Y + num225 * 100f, num224, num225, ModContent.ProjectileType<DynaBlast>(), num227, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num229].timeLeft = 300;
                            return;
                        }
                    }
                    else if (NPC.ai[0] >= 250 + Main.rand.Next(250))
                    {
                        NPC.ai[0] = 0f;
                        return;
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 8)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
                if (NPC.ai[3] < 240 && NPC.frame.Y > frameHeight * 3)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.ai[3] >= 240 && (NPC.frame.Y < frameHeight * 4 || NPC.frame.Y >= frameHeight * 8))
                {
                    NPC.frame.Y = frameHeight * 4;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			BaseDrawing.DrawAfterimage(sb, TextureAssets.Npc[NPC.type].Value, 0, NPC, 2.5f, 0.9F, 3, true, 0f, 0f, dColor);
			BaseDrawing.DrawTexture(sb, TextureAssets.Npc[NPC.type].Value, 0, NPC, dColor);
            BaseDrawing.DrawTexture(sb, Mod.GetTexture("Glowmasks/Dynasaur_Glow"), 0, NPC, Color.White);
            return false;
		}

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            if (moveSpeed == 0f || NPC.Center == point) return; //don't move if you have no move speed
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
    }
}
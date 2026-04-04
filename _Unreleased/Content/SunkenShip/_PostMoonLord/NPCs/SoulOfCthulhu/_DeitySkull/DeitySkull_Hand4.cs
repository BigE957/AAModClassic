using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeitySkull
{
    public class DeitySkullHand4 : ModNPC
    {

        public override void SetStaticDefaults()
        {

            Main.npcFrameCount[NPC.type] = 4;
        }
        public override string Texture
        {
            get
            {
                return "AAModClassic/_Unreleased/NPCs/Bosses/SoC/Bosses/DeitySkull_Hand";
            }
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 52;
            NPC.height = 52;
            NPC.damage = 40;
            NPC.defense = 23;
            NPC.lifeMax = 25000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.0f;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[39] = true;
            NPC.lavaImmune = true;
            NPC.netAlways = true;

        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)NPC.localAI[0]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.localAI[0] = reader.ReadInt16();
        }

        public override void AI()
        {



            Vector2 vector2_1 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float num1 = (float)(Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2) - 200.0 * NPC.ai[0]) - vector2_1.X;
            float num2 = Main.npc[(int)NPC.ai[1]].position.Y + 230f - vector2_1.Y;
            float num3 = (float)Math.Sqrt(num1 * (double)num1 + num2 * (double)num2);


            if (NPC.ai[2] != 99.0)
            {
                if (num3 > 800.0)
                    NPC.ai[2] = 99f;
            }
            else if (num3 < 400.0)
                NPC.ai[2] = 0.0f;
            NPC.spriteDirection = -(int)NPC.ai[0];
            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.ai[2] += 10f;
                if (NPC.ai[2] > 50.0 || Main.netMode != 2)
                {
                    NPC.life = -1;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                }
            }
            if (NPC.ai[2] == 99.0)
            {
                if (NPC.position.Y > (double)Main.npc[(int)NPC.ai[1]].position.Y - 100)
                {
                    if (NPC.velocity.Y > 0.0)
                        NPC.velocity.Y *= 0.96f;
                    NPC.velocity.Y -= 0.1f;
                    if (NPC.velocity.Y > 8.0)
                        NPC.velocity.Y = 8f;
                }
                else if (NPC.position.Y < (double)Main.npc[(int)NPC.ai[1]].position.Y - 100)
                {
                    if (NPC.velocity.Y < 0.0)
                        NPC.velocity.Y *= 0.96f;
                    NPC.velocity.Y += 0.1f;
                    if (NPC.velocity.Y < -8.0)
                        NPC.velocity.Y = -8f;
                }
                if (NPC.position.X + (double)(NPC.width / 2) > Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2))
                {
                    if (NPC.velocity.X > 0.0)
                        NPC.velocity.X *= 0.96f;
                    NPC.velocity.X -= 0.5f;
                    if (NPC.velocity.X > 12.0)
                        NPC.velocity.X = 12f;
                }
                if (NPC.position.X + (double)(NPC.width / 2) >= Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2))
                    return;
                if (NPC.velocity.X < 0.0)
                    NPC.velocity.X *= 0.96f;
                NPC.velocity.X += 0.5f;
                if (NPC.velocity.X >= -12.0)
                    return;
                NPC.velocity.X = -12f;
            }
            else if (NPC.ai[2] == 0.0 || NPC.ai[2] == 3.0)
            {
                if (Main.npc[(int)NPC.ai[1]].ai[1] == 3.0 && NPC.timeLeft > 10)
                    NPC.timeLeft = 10;
                if (Main.npc[(int)NPC.ai[1]].ai[1] != 0.0)
                {
                    NPC.TargetClosest(true);
                    if (Main.player[NPC.target].dead)
                    {
                        NPC.velocity.Y += 0.1f;
                        if (NPC.velocity.Y > 12.0)
                            NPC.velocity.Y = 12f;
                    }
                    else
                    {
                        if (NPC.position.Y > Main.npc[(int)NPC.ai[1]].position.Y - 100.0)
                        {
                            if (NPC.velocity.Y > 0.0)
                                NPC.velocity.Y *= 0.96f;
                            NPC.velocity.Y -= 0.07f;
                            if (NPC.velocity.Y > 6.0)
                                NPC.velocity.Y = 6f;
                        }
                        else if (NPC.position.Y < Main.npc[(int)NPC.ai[1]].position.Y - 100.0)
                        {
                            if (NPC.velocity.Y < 0.0)
                                NPC.velocity.Y *= 0.96f;
                            NPC.velocity.Y += 0.07f;
                            if (NPC.velocity.Y < -6.0)
                                NPC.velocity.Y = -6f;
                        }
                        if (NPC.position.X + (double)(NPC.width / 2) > Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2) - 120.0 * NPC.ai[0])
                        {
                            if (NPC.velocity.X > 0.0)
                                NPC.velocity.X *= 0.96f;
                            NPC.velocity.X -= 0.1f;
                            if (NPC.velocity.X > 8.0)
                                NPC.velocity.X = 8f;
                        }
                        if (NPC.position.X + (double)(NPC.width / 2) < Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2) - 120.0 * NPC.ai[0])
                        {
                            if (NPC.velocity.X < 0.0)
                                NPC.velocity.X *= 0.96f;
                            NPC.velocity.X += 0.1f;
                            if (NPC.velocity.X < -8.0)
                                NPC.velocity.X = -8f;
                        }

                        NPC.TargetClosest(true);

                        if (Main.netMode == 1 || !Main.expertMode)
                            return;
                        ++NPC.localAI[0];
                        if (NPC.localAI[0] <= 150.0)
                            return;
                        NPC.localAI[0] = 0.0f;
                        Vector2 vector2_6 = vector2_1;
                        float num41 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector2_6.X;
                        float num42 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector2_6.Y;
                        float num43 = (float)Math.Sqrt(num41 * (double)num41 + num42 * (double)num42);
                        float num4 = 8f;
                        int Damage = 15;
                        int Type = 258;
                        float num5 = num4 / num43;
                        float num6 = num41 * num5;
                        float num7 = num42 * num5;
                        float SpeedX = num6 + Main.rand.Next(-5, 6) * 0.05f;
                        float SpeedY = num7 + Main.rand.Next(-5, 6) * 0.05f;
                        vector2_6.X += SpeedX * 6f;
                        vector2_6.Y += SpeedY * 6f;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), vector2_6.X, vector2_6.Y, SpeedX, SpeedY, Type, Damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);

                    }
                    ++NPC.ai[3];
                    if (Main.expertMode) NPC.ai[3] += 0.5f;
                    if (NPC.ai[3] >= 600.0)
                    {
                        NPC.ai[2] = 0.0f;
                        NPC.ai[3] = 0.0f;
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    ++NPC.ai[3];
                    if (Main.expertMode) NPC.ai[3] += 0.5f;
                    if (NPC.ai[3] >= 300.0)
                    {
                        ++NPC.ai[2];
                        NPC.ai[3] = 0.0f;
                        NPC.netUpdate = true;
                    }
                    if (NPC.position.Y > Main.npc[(int)NPC.ai[1]].position.Y + 230.0)
                    {
                        if (NPC.velocity.Y > 0.0)
                            NPC.velocity.Y *= 0.96f;
                        NPC.velocity.Y -= 0.04f;
                        if (NPC.velocity.Y > 3.0)
                            NPC.velocity.Y = 3f;
                    }
                    else if (NPC.position.Y < Main.npc[(int)NPC.ai[1]].position.Y + 230.0)
                    {
                        if (NPC.velocity.Y < 0.0)
                            NPC.velocity.Y *= 0.96f;
                        NPC.velocity.Y += 0.04f;
                        if (NPC.velocity.Y < -3.0)
                            NPC.velocity.Y = -3f;
                    }
                    if (NPC.position.X + (double)(NPC.width / 2) > Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2) - 200.0 * NPC.ai[0])
                    {
                        if (NPC.velocity.X > 0.0)
                            NPC.velocity.X *= 0.96f;
                        NPC.velocity.X -= 0.07f;
                        if (NPC.velocity.X > 8.0)
                            NPC.velocity.X = 8f;
                    }
                    if (NPC.position.X + (double)(NPC.width / 2) < Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2) - 200.0 * NPC.ai[0])
                    {
                        if (NPC.velocity.X < 0.0)
                            NPC.velocity.X *= 0.96f;
                        NPC.velocity.X += 0.07f;
                        if (NPC.velocity.X < -8.0)
                            NPC.velocity.X = -8f;
                    }
                }
                Vector2 vector2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float num10 = (float)(Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2) - 200.0 * NPC.ai[0]) - vector2.X;
                float num20 = Main.npc[(int)NPC.ai[1]].position.Y + 230f - vector2.Y;
                Math.Sqrt(num10 * (double)num10 + num20 * (double)num20);
                NPC.rotation = (float)Math.Atan2(num20, num10) + 1.57f;
            }
            else if (NPC.ai[2] == 1.0)
            {
                Vector2 vector2_2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float num4 = (float)(Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2) - 200.0 * NPC.ai[0]) - vector2_2.X;
                float num5 = Main.npc[(int)NPC.ai[1]].position.Y + 230f - vector2_2.Y;
                float num6 = (float)Math.Sqrt(num4 * (double)num4 + num5 * (double)num5);
                NPC.rotation = (float)Math.Atan2(num5, num4) + 1.57f;
                NPC.velocity.X *= 0.95f;
                NPC.velocity.Y -= 0.1f;
                if (NPC.velocity.Y < -8.0)
                    NPC.velocity.Y = -8f;
                if (NPC.position.Y >= Main.npc[(int)NPC.ai[1]].position.Y - 200.0)
                    return;
                NPC.TargetClosest(true);
                NPC.ai[2] = 2f;
                vector2_2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float num7 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector2_2.X;
                float num8 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector2_2.Y;
                float num9 = 22f / (float)Math.Sqrt(num7 * (double)num7 + num8 * (double)num8);
                NPC.velocity.X = num7 * num9;
                NPC.velocity.Y = num8 * num9;
                NPC.netUpdate = true;
            }
            else if (NPC.ai[2] == 2.0)
            {
                if (NPC.position.Y <= (double)Main.player[NPC.target].position.Y && NPC.velocity.Y >= 0.0)
                    return;
                NPC.ai[2] = 3f;
            }
            else if (NPC.ai[2] == 4.0)
            {
                Vector2 vector2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float num10 = (float)(Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2) - 200.0 * NPC.ai[0]) - vector2.X;
                float num20 = Main.npc[(int)NPC.ai[1]].position.Y + 230f - vector2.Y;
                float num30 = (float)Math.Sqrt((double)num10 * (double)num10 + (double)num20 * (double)num20);
                NPC.rotation = (float)Math.Atan2((double)num20, (double)num10) + 1.57f;
                NPC.velocity.Y *= 0.95f;
                NPC.velocity.X += (float)(0.100000001490116 * -(double)NPC.ai[0]);
                if (Main.expertMode)
                {
                    NPC.velocity.X += (float)(0.0700000002980232 * -(double)NPC.ai[0]);
                    if (NPC.velocity.X < -12.0)
                        NPC.velocity.X = -12f;
                    else if (NPC.velocity.X > 12.0)
                        NPC.velocity.X = 12f;
                }
                else if (NPC.velocity.X < -8.0)
                    NPC.velocity.X = -8f;
                else if (NPC.velocity.X > 8.0)
                    NPC.velocity.X = 8f;
                if (NPC.position.X + (double)(NPC.width / 2) >= Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2) - 500.0 && NPC.position.X + (double)(NPC.width / 2) <= Main.npc[(int)NPC.ai[1]].position.X + (double)(Main.npc[(int)NPC.ai[1]].width / 2) + 500.0)
                    return;
                NPC.TargetClosest(true);
                NPC.ai[2] = 5f;
                vector2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float num4 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector2.X;
                float num5 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector2.Y;
                float num6 = (float)Math.Sqrt((double)num4 * (double)num4 + (double)num5 * (double)num5);
                float num7 = !Main.expertMode ? 17f / num6 : 22f / num6;
                NPC.velocity.X = num4 * num7;
                NPC.velocity.Y = num5 * num7;
                NPC.netUpdate = true;
            }
            else
            {
                if (NPC.ai[2] != 5.0 || (NPC.velocity.X <= 0.0 || NPC.position.X + (double)(NPC.width / 2) <= Main.player[NPC.target].position.X + (double)(Main.player[NPC.target].width / 2)) && (NPC.velocity.X >= 0.0 || NPC.position.X + (double)(NPC.width / 2) >= Main.player[NPC.target].position.X + (double)(Main.player[NPC.target].width / 2)))
                    return;
                NPC.ai[2] = 0.0f;
            }
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Vector2 vector7 = new Vector2(NPC.position.X + NPC.width * 0.5f - 5f * NPC.ai[0], NPC.position.Y + 20f);
            for (int l = 0; l < 2; l++)
            {
                float num21 = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - vector7.X;
                float num22 = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - vector7.Y;
                float num23;
                if (l == 0)
                {
                    num21 -= 200f * NPC.ai[0];
                    num22 += 130f;
                    num23 = (float)Math.Sqrt((double)(num21 * num21 + num22 * num22));
                    num23 = 92f / num23;
                    vector7.X += num21 * num23;
                    vector7.Y += num22 * num23;
                }
                else
                {
                    num21 -= 50f * NPC.ai[0];
                    num22 += 80f;
                    num23 = (float)Math.Sqrt((double)(num21 * num21 + num22 * num22));
                    num23 = 60f / num23;
                    vector7.X += num21 * num23;
                    vector7.Y += num22 * num23;
                }
                float rotation7 = (float)Math.Atan2((double)num22, (double)num21) - 1.57f;
                Color color7 = Lighting.GetColor((int)vector7.X / 16, (int)(vector7.Y / 16f));
                Main.spriteBatch.Draw(Mod.GetTexture("_Unreleased/NPCs/Bosses/SoC/Bosses/DeitySkull_Arm"), new Vector2(vector7.X - Main.screenPosition.X, vector7.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, ModContent.Request<Texture2D>("Terraria/Images/Arm_Bone").Value.Width, ModContent.Request<Texture2D>("Terraria/Images/Arm_Bone").Value.Height)), color7, rotation7, new Vector2(ModContent.Request<Texture2D>("Terraria/Images/Arm_Bone").Value.Width * 0.5f, ModContent.Request<Texture2D>("Terraria/Images/Arm_Bone").Value.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                if (l == 0)
                {
                    vector7.X += num21 * num23 / 2f;
                    vector7.Y += num22 * num23 / 2f;
                }
                else if (Main.rand.Next(2) == 0)
                {

                    vector7.X += num21 * num23 - 16f;
                    vector7.Y += num22 * num23 - 6f;
                }
            }
            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }

    }
}
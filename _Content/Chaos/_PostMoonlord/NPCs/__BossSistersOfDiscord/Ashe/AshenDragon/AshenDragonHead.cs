using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Ashe.AshenDragon
{
    public class AshenDragonHead : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ashen Dragon");
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.noTileCollide = true;
            NPC.npcSlots = 5f;
            NPC.width = 32;
            NPC.height = 32;
            NPC.aiStyle = NPCAIStyleID.Worm;
            NPC.netAlways = true;
            NPC.damage = 100;
            NPC.defense = 40;
            NPC.lifeMax = 10000;
            NPC.HitSound = SoundID.NPCHit56;
            NPC.DeathSound = SoundID.NPCDeath60;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
            NPC.value = 0f;
            NPC.scale = 1f;
            NPC.alpha = 255;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (NPC.localAI[3] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item119, NPC.position);
                NPC.localAI[3] = 1f;
            }

            NPC.dontTakeDamage = NPC.alpha > 0;
            if (NPC.dontTakeDamage)
            {
                for (int j = 0; j < 2; j++)
                {
                    int dust = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.GoldFlame, 0f, 0f, 100, default, 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                }
            }

            NPC.alpha -= 42;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }

            bool flag = true;
            float speedY = 0.2f;

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || flag && Main.player[NPC.target].position.Y < Main.worldSurface * 16.0)
            {
                NPC.TargetClosest(true);
            }

            if (Main.player[NPC.target].dead || flag && Main.player[NPC.target].position.Y < Main.worldSurface * 16.0)
            {
                if (NPC.timeLeft > 300)
                {
                    NPC.timeLeft = 300;
                }

                if (flag)
                {
                    NPC.velocity.Y += speedY;
                }
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 0f)
                {
                    NPC.ai[3] = NPC.whoAmI;
                    NPC.realLife = NPC.whoAmI;
                    int npcWhoAmI = NPC.whoAmI;

                    for (int l = 0; l < 30; l++)
                    {
                        int type = ModContent.NPCType<AshenDragonBody1>();
                        if ((l - 2) % 4 == 0 && l < 26)
                        {
                            type = ModContent.NPCType<AshenDragonArms>();
                        }
                        else if (l == 27)
                        {
                            type = ModContent.NPCType<AshenDragonBody2>();
                        }
                        else if (l == 28)
                        {
                            type = ModContent.NPCType<AshenDragonBody3>();
                        }
                        else if (l == 29)
                        {
                            type = ModContent.NPCType<AshenDragonTail>();
                        }

                        if(Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int newNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)(NPC.position.Y + NPC.height), type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                            if (Main.netMode == NetmodeID.Server && newNPC < 200) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, newNPC);
                            Main.npc[newNPC].ai[3] = NPC.whoAmI;
                            Main.npc[newNPC].realLife = NPC.whoAmI;
                            Main.npc[newNPC].ai[1] = npcWhoAmI;

                            Main.npc[npcWhoAmI].ai[0] = newNPC;
                            npcWhoAmI = newNPC;
                        }
                        NPC.netUpdate = true;
                    }
                }
            }

            int npcLeftPos = (int)(NPC.position.X / 16f) - 1;
            int npcRightPos = (int)((NPC.position.X + NPC.width) / 16f) + 2;
            int npcBottomPos = (int)(NPC.position.Y / 16f) - 1;
            int npcTopPos = (int)((NPC.position.Y + NPC.height) / 16f) + 2;

            if (npcLeftPos < 0)
            {
                npcLeftPos = 0;
            }

            if (npcRightPos > Main.maxTilesX)
            {
                npcRightPos = Main.maxTilesX;
            }

            if (npcBottomPos < 0)
            {
                npcBottomPos = 0;
            }

            if (npcTopPos > Main.maxTilesY)
            {
                npcTopPos = Main.maxTilesY;
            }

            NPC.direction = NPC.velocity.X < 0f ? 1 : -1;

            float num37 = 20f;
            float num38 = 0.55f;

            Vector2 NPCCenter = NPC.Center;
            float playerCenterX = Main.player[NPC.target].Center.X;
            float playerCenterY = Main.player[NPC.target].Center.Y;

            playerCenterX = (int)(playerCenterX / 16f) * 16;
            playerCenterY = (int)(playerCenterY / 16f) * 16;
            NPCCenter.X = (int)(NPCCenter.X / 16f) * 16;
            NPCCenter.Y = (int)(NPCCenter.Y / 16f) * 16;
            playerCenterX -= NPCCenter.X;
            playerCenterY -= NPCCenter.Y;

            float num53 = (float)Math.Sqrt(playerCenterX * playerCenterX + playerCenterY * playerCenterY);
            if (NPC.ai[1] > 0f && NPC.ai[1] < Main.npc.Length)
            {
                try
                {
                    NPCCenter = NPC.Center;
                    playerCenterX = Main.npc[(int)NPC.ai[1]].Center.X - NPCCenter.X;
                    playerCenterY = Main.npc[(int)NPC.ai[1]].Center.Y - NPCCenter.Y;
                }
                catch
                {
                }

                NPC.rotation = (float)Math.Atan2(playerCenterY, playerCenterX) + 1.57f;
                int num54 = 42;
                num53 = (num53 - num54) / num53;
                playerCenterX *= num53;
                playerCenterY *= num53;
                NPC.velocity = Vector2.Zero;
                NPC.position.X += playerCenterX;
                NPC.position.Y += playerCenterY;
            }
            else
            {
                float num56 = Math.Abs(playerCenterX);
                float num57 = Math.Abs(playerCenterY);
                float num58 = num37 / num53;
                playerCenterX *= num58;
                playerCenterY *= num58;
                bool flag6 = false;

                if ((NPC.velocity.X > 0f && playerCenterX < 0f || NPC.velocity.X < 0f && playerCenterX > 0f || NPC.velocity.Y > 0f && playerCenterY < 0f || NPC.velocity.Y < 0f && playerCenterY > 0f) && Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) > num38 / 2f && num53 < 300f)
                {
                    flag6 = true;

                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num37)
                    {
                        NPC.velocity *= 1.1f;
                    }
                }

                if (NPC.position.Y > Main.player[NPC.target].position.Y || Main.player[NPC.target].dead)
                {
                    flag6 = true;

                    if (Math.Abs(NPC.velocity.X) < num37 / 2f)
                    {
                        if (NPC.velocity.X == 0f)
                        {
                            NPC.velocity.X -= NPC.direction;
                        }

                        NPC.velocity.X *= 1.1f;
                    }
                    else if (NPC.velocity.Y > -num37)
                    {
                        NPC.velocity.Y -= num38;
                    }
                }

                if (!flag6)
                {
                    if (NPC.velocity.X > 0f && playerCenterX > 0f || NPC.velocity.X < 0f && playerCenterX < 0f || NPC.velocity.Y > 0f && playerCenterY > 0f || NPC.velocity.Y < 0f && playerCenterY < 0f)
                    {
                        if (NPC.velocity.X < playerCenterX)
                        {
                            NPC.velocity.X += num38;
                        }
                        else if (NPC.velocity.X > playerCenterX)
                        {
                            NPC.velocity.X -= num38;
                        }

                        if (NPC.velocity.Y < playerCenterY)
                        {
                            NPC.velocity.Y += num38;
                        }
                        else if (NPC.velocity.Y > playerCenterY)
                        {
                            NPC.velocity.Y -= num38;
                        }

                        if (Math.Abs(playerCenterY) < num37 * 0.2 && (NPC.velocity.X > 0f && playerCenterX < 0f || NPC.velocity.X < 0f && playerCenterX > 0f))
                        {
                            NPC.velocity.Y += NPC.velocity.Y > 0f ? num38 * 2f : -num38 * 2f;
                        }

                        if (Math.Abs(playerCenterX) < num37 * 0.2 && (NPC.velocity.Y > 0f && playerCenterY < 0f || NPC.velocity.Y < 0f && playerCenterY > 0f))
                        {
                            NPC.velocity.X += NPC.velocity.X > 0f ? num38 * 2f : -num38 * 2f;
                        }
                    }
                    else if (num56 > num57)
                    {
                        if (NPC.velocity.X < playerCenterX)
                        {
                            NPC.velocity.X += num38 * 1.1f;
                        }
                        else if (NPC.velocity.X > playerCenterX)
                        {
                            NPC.velocity.X -= num38 * 1.1f;
                        }

                        if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num37 * 0.5)
                        {
                            NPC.velocity.Y += NPC.velocity.Y > 0f ? num38 : -num38;
                        }
                    }
                    else
                    {
                        if (NPC.velocity.Y < playerCenterY)
                        {
                            NPC.velocity.Y += num38 * 1.1f;
                        }
                        else if (NPC.velocity.Y > playerCenterY)
                        {
                            NPC.velocity.Y -= num38 * 1.1f;
                        }

                        if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num37 * 0.5)
                        {
                            NPC.velocity.X += NPC.velocity.X > 0f ? num38 : -num38;
                        }
                    }
                }

                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;

                float num62 = Vector2.Distance(Main.player[NPC.target].Center, NPC.Center);
                int num63 = 0;
                if (Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center).ToRotation().AngleTowards(NPC.velocity.ToRotation(), (float)Math.PI / 2) == NPC.velocity.ToRotation() && num62 < 350f)
                {
                    num63 = 15;
                }

                if (num63 > NPC.frameCounter)
                {
                    NPC.frameCounter += 1.0;
                }

                if (num63 < NPC.frameCounter)
                {
                    NPC.frameCounter -= 1.0;
                }

                if (NPC.frameCounter < 0.0)
                {
                    NPC.frameCounter = 0.0;
                }

                if (NPC.frameCounter > 15.0)
                {
                    NPC.frameCounter = 15.0;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            int Frame = 0;
            if(NPC.frameCounter < 5.0)
            {
                Frame = 0;
            }
            else if(NPC.frameCounter < 10.0)
            {
                Frame = 1;
            }
            else
            {
                Frame = 2;
            }
            NPC.frame.Y = Frame * frameHeight;
        }

        public override void OnKill()
        {
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(NPC.Center, NPC.width, 1, ModContent.DustType<Dusts.AkumaDust>(), -NPC.velocity.X * 0.2f, -NPC.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;

                num469 = Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.AkumaDust>(), -NPC.velocity.X * 0.2f, -NPC.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
        //    int frameCount = /*npc.type == Terraria.ModLoader.ModContent.NPCType<AsheDragon>() ? 3 :*/ 1;
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, Main.npcFrameCount[NPC.type], NPC.frame, new Color(Color.White.R, Color.White.G, Color.White.B, 100), true);

            return false;
        }
    }
}
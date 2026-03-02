
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Djinn
{
    [AutoloadBossHead]
    public class Djinn : ModNPC
    {
        public int damage = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desert Djinn");
            Main.npcFrameCount[NPC.type] = 15;
        }

        public override void SetDefaults()
        {
            NPC.width = 70;
            NPC.height = 80;
            NPC.aiStyle = -1;
            NPC.damage = 40;
            NPC.defense = 15;
            NPC.lifeMax = 6000;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[44] = true;
            NPC.value = 50000f;
            NPC.HitSound = SoundID.NPCHit23;
            NPC.DeathSound = SoundID.NPCDeath39;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Djinn");
            bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ModContent.ItemType<Items.Boss.Djinn.DjinnBag>();
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 1.6f);
            NPC.defense = (int)(NPC.defense * 1.2f);
        }

        public int runonce = 0;
        public int FrameHeight = 130;

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
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
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        bool selectPoint = false;
        Vector2 MovePoint;
        bool soundPlayed = false;

        public override void AI()
        {
            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }
            Player player = Main.player[NPC.target];
            if (runonce == 0)
            {
                StartSandstorm();
                runonce += 1;
            }
	        if (internalAI[0] == 2 && NPC.ai[3] < 60)
            {
					NPC.velocity.X *= 0.97f;
			}
		
            if (internalAI[0] == 2 && NPC.ai[3] > 120)
            {
                if (NPC.velocity.X > 0)
                {
                    NPC.direction = 1;
                }
                else
                {
                    NPC.direction = -1;
                }
            }
            else
            {
                if (player.Center.X > NPC.Center.X)
                {
                    NPC.direction = 1;
                }
                else
                {
                    NPC.direction = -1;
                }
            }

            if (player.dead || !player.active)
            {
                NPC.TargetClosest(true);
                if (player.dead || !player.active)
                {
                    FuriousFlexing();
                    return;
                }
            }
            else if (!player.ZoneDesert)
            {
                if (!soundPlayed)
                {
                    soundPlayed = true;
                    SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                }
                NPC.damage = 200 * (Main.expertMode ? (int)(NPC.damage * 1.6f) : 1);
                NPC.defense = 1000;
                NPC.ai[3]++;
                BaseAI.AIFlier(NPC, ref NPC.ai, true, 0.3f, 0.3f, 16f, 16f, false, 300);

                if (NPC.localAI[0]++ > 50)
                {
                    NPC.localAI[0] = 0;
                    Projectile.NewProjectile(NPC.Center.X + Main.rand.Next(-200, 200), NPC.Center.Y + Main.rand.Next(-100, 100), 0, 0, ModContent.ProjectileType<Menacing>(), 0, 0, Main.myPlayer);
                }
                return;
            }
            else
            {
                soundPlayed = false;
                NPC.defense = 15;
                Sandstorm.TimeLeft = 10;
                if (NPC.alpha <= 0)
                {
                    NPC.alpha = 0;
                }
                else
                {
                    NPC.alpha -= 2;
                }
            }


            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.damage = 30 * (Main.expertMode ? (int)(NPC.damage * 1.6f) : 1); ;
                internalAI[1]++;
                if (internalAI[1] >= 300)
                {
					
                    selectPoint = true; ;
                    internalAI[0] = Main.rand.Next(3);
                    internalAI[1] = 0;
                    NPC.ai[3] = 0;
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
            }

            if (internalAI[0] == 0)
            {
                NPC.damage = 30 * (Main.expertMode ? (int)(NPC.damage * 1.6f) : 1); ;
                NPC.ai[3]++;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
                if (NPC.ai[3] == 9 || NPC.ai[3] == 36 || NPC.ai[3] == 72)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && AAGlobalProjectile.CountProjectiles(658) < 5)
                    {
                        FireProjectile();
                        NPC.netUpdate = true;
                    }
                }
                if (NPC.ai[3] > 90 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI[0] = 10;
                    internalAI[1] = 0;
                    NPC.ai = new float[4];
                    NPC.ai[3] = 0;
                    NPC.netUpdate = true;
                }
            }
            else if (internalAI[0] == 1)
            {
                NPC.damage = 60 * (Main.expertMode ? (int)(NPC.damage * 1.6f) : 1); ;
                NPC.ai[3]++;
                BaseAI.AIFlier(NPC, ref NPC.ai, true, 0.1f, 0.1f, 6f, 6f, false, 300);
                NPC.damage = 40;

                if (NPC.ai[3] % 30 == 0)
                {
                    Projectile.NewProjectile(NPC.position + new Vector2(Main.rand.Next(70), Main.rand.Next(80)), Vector2.Zero, ModContent.ProjectileType<Menacing>(), 0, 0, Main.myPlayer);
                }
                if (NPC.ai[3] > 200 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI[0] = 10;
                    internalAI[1] = 0;
                    NPC.ai = new float[4];
                    NPC.ai[3] = 0;
                    NPC.netUpdate = true;
                }
            }
            else if (internalAI[0] == 2)
            {
                NPC.ai[3]++;
				
                NPC.damage = 50 * (Main.expertMode ? (int)(NPC.damage * 1.6f) : 1); ;
                if (NPC.ai[3] < 120 && NPC.ai[3] > 60)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (selectPoint)
                        {
							
							 
                            float point = 700 * NPC.direction;
                            MovePoint = player.Center + new Vector2(point, 0);
							MoveToPoint(MovePoint, 10f);
                            selectPoint = false;
                            NPC.netUpdate = true;
                        }
                        NPC.damage = 20 * (Main.expertMode ? (int)(NPC.damage * 1.6f) : 1); ;
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (NPC.ai[3] == 120)
                        {
							 
                            //float point = 500 * npc.direction;
                            MovePoint = new Vector2(player.Center.X, NPC.position.Y);
							MoveToPoint(MovePoint, 10f);
                            NPC.netUpdate = true;
                        }
                        NPC.damage = 40 * (Main.expertMode ? (int)(NPC.damage * 1.6f) : 1); ;
                        NPC.netUpdate = true;
                    }
                }

               

                if (NPC.ai[3] > 160 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.damage = 30;
                    internalAI[0] = 10;
                    internalAI[1] = 0;
                    NPC.ai = new float[4];
                    NPC.ai[3] = 0;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                NPC.damage = 30;
                BaseAI.AIFlier(NPC, ref NPC.ai, true, 0.1f, 0.04f, 4f, 2f, false, 300);
            }
        }

        int Frame = 0;
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (internalAI[0] == 0)
            {
                if (Frame < 6 || Frame > 14)
                {
                    Frame = 6;
                }
                if (NPC.ai[3] > 0)
                {
                    Frame = 6;
                }
                if (NPC.ai[3] > 9)
                {
                    Frame = 7;
                }
                if (NPC.ai[3] > 18)
                {
                    Frame = 8;
                }
                if (NPC.ai[3] > 27)
                {
                    Frame = 9;
                }
                if (NPC.ai[3] > 36)
                {
                    Frame = 10;
                }
                if (NPC.ai[3] > 45)
                {
                    Frame = 11;
                }
                if (NPC.ai[3] > 54)
                {
                    Frame = 12;
                }
                if (NPC.ai[3] > 63)
                {
                    Frame = 13;
                }
                if (NPC.ai[3] > 72)
                {
                    Frame = 14;
                }
                NPC.frame.Y = Frame * frameHeight;
                return;
            }
            else if (internalAI[0] == 1 || !Main.player[NPC.target].ZoneDesert)
            {
                if (NPC.frameCounter > 5)
                {
                    NPC.frame.Y += frameHeight;
                    NPC.frameCounter = 0;
                }
                if (NPC.frame.Y > FrameHeight * 5)
                {
                    NPC.frame.Y = 0;
                }
                return;
            }
            else if (internalAI[0] == 2)
            {
                if (NPC.ai[3] < 60)
                {
					
                    if (NPC.frameCounter > 9)
                    {
                        NPC.frame.Y += frameHeight;
                        NPC.frameCounter = 0;
                    }
                    if (NPC.frame.Y > FrameHeight * 3)
                    {
                        NPC.frame.Y = 0;
                    }
                }
                else
                {
					
                    if (NPC.frame.Y < FrameHeight * 4)
                    {
                        NPC.frame.Y = FrameHeight * 4;
                    }
                    if (NPC.frameCounter > 9)
                    {
                        NPC.frame.Y += frameHeight;
                        NPC.frameCounter = 0;
                    }
			    if (NPC.frame.Y > FrameHeight * 7)
                {
					
                     NPC.frame.Y = FrameHeight * 5;
                }
                }
               
                return;
            }
            else
            {
                if (NPC.frameCounter > 9)
                {
                    NPC.frame.Y += 130;
                    NPC.frameCounter = 0;
                }
                if (NPC.frame.Y > FrameHeight * 5)
                {
                    NPC.frame.Y = 0;
                }
                return;
            }
        }

        public void FuriousFlexing()
        {
            NPC.velocity.X *= .85f;
            NPC.velocity.Y *= .85f;
            NPC.alpha += 2;
            if (NPC.alpha >= 255)
            {
                NPC.active = false;
            }
            if (NPC.ai[3] < 300)
            {
                NPC.ai[3] = 300;
            }
            if (NPC.frameCounter > 5)
            {
                NPC.frame.Y += 130;
                NPC.frameCounter = 0;
                if (NPC.ai[3] > 381)
                {
                    NPC.ai[3] = 300;
                }
            }
        }

        public void FireProjectile()
        {
            List<Point> list4 = new List<Point>();
            Vector2 vec5 = Main.player[NPC.target].Center + new Vector2(Main.player[NPC.target].velocity.X * 30f, 0f);
            Point point14 = vec5.ToTileCoordinates();
            int num1468 = 0;
            while (num1468 < 1000 && list4.Count < 1)
            {
                bool flag118 = false;
                int num1469 = Main.rand.Next(point14.X - 30, point14.X + 30 + 1);
                foreach (Point current in list4)
                {
                    if (Math.Abs(current.X - num1469) < 10)
                    {
                        flag118 = true;
                        break;
                    }
                }
                if (!flag118)
                {
                    int startY = point14.Y - 20;
                    Collision.ExpandVertically(num1469, startY, out int num1470, out int num1471, 1, 51);
                    if (StrayMethods.CanSpawnSandstormHostile(new Vector2(num1469, num1471 - 15) * 16f, 15, 15))
                    {
                        list4.Add(new Point(num1469, num1471 - 15));
                    }
                }
                num1468++;
            }
            foreach (Point current2 in list4)
            {
                Projectile.NewProjectile(current2.X * 16, current2.Y * 16, 0f, 0f, 658, damage, 0f, Main.myPlayer, 0f, 0f);
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            NPC.position.X = NPC.position.X + NPC.width / 2;
            NPC.position.Y = NPC.position.Y + NPC.height / 2;
            NPC.position.X = NPC.position.X - NPC.width / 2;
            NPC.position.Y = NPC.position.Y - NPC.height / 2;
            int dust = ModContent.DustType<Dusts.SandDust>();
            for (int Loop = 0; Loop < 5; Loop++)
            {
                int d = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust, 0f, 0f, 0);
                Main.dust[d].velocity.Y = hitDirection * 0.1F;
                Main.dust[d].noGravity = false;
            }
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/DjinnGore1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/DjinnGore2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/DjinnGore3"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/DjinnGore4"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/DjinnGore5"), 1f);
                for (int Loop = 0; Loop < 60; Loop++)
                {
                    int d = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust, 0f, 0f, 0);
                    Main.dust[d].velocity.X *= 0f;
                    Main.dust[d].noGravity = false;
                }
            }
        }

        public void MoveToPoint(Vector2 point, float moveSpeed)
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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }


        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
            AAWorld.downedDjinn = true;
        }

        public override void OnKill()
        {
            Sandstorm.TimeLeft = 0;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("DjinnTrophy").Type);
            }
            if (!Main.expertMode)
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("DjinnMask").Type);
                }
                NPC.DropLoot(Mod.Find<ModItem>("DesertMana").Type, 10, 15);
                string[] lootTable = { "Djinnerang", "SandLamp", "SandScepter", "SandstormCrossbow", "SultanScimitar" };
                int loot = Main.rand.Next(lootTable.Length);
                NPC.DropLoot(Items.Vanity.Mask.DjinnMask.type, 1f / 7);
                if (Main.rand.Next(6) == 0)
                {
                    NPC.DropLoot(Mod.Find<ModItem>("Sandagger").Type, 90, 120);
                }
                else
                {
                    NPC.DropLoot(Mod.Find<ModItem>(lootTable[loot]).Type);
                }
            }
            else
            {
                NPC.DropBossBags();
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D CurrentTex;
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D MudaMuda = Mod.GetTexture("NPCs/Bosses/Djinn/DesertDjinnMudaMuda");
            Texture2D Punch = Mod.GetTexture("NPCs/Bosses/Djinn/DesertDjinnPunch");

            if (internalAI[0] == 1 || !Main.player[NPC.target].ZoneDesert)
            {
                CurrentTex = MudaMuda;
            }
            else if (internalAI[0] == 2)
            {
                CurrentTex = Punch;
            }
            else
            {
                CurrentTex = texture;
            }

            var effects = NPC.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            if (!Main.player[NPC.target].ZoneDesert)
            {
                drawColor = Color.Goldenrod;
                BaseDrawing.DrawAfterimage(spriteBatch, CurrentTex, 0, NPC, 1, 1, 7, false, 0, 0, drawColor, NPC.frame, 15);
            }

            spriteBatch.Draw(CurrentTex, NPC.Center - Main.screenPosition, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);

            return false;
        }

        private static void StartSandstorm()
        {
            Sandstorm.Happening = true;
            Sandstorm.TimeLeft = (int)(3600f * (8f + Main.rand.NextFloat() * 16f));
            ChangeSeverityIntentions();
        }

        private static void ChangeSeverityIntentions()
        {
            if (Sandstorm.Happening)
            {
                Sandstorm.IntendedSeverity = 0.4f + Main.rand.NextFloat();
            }
            else if (Main.rand.Next(3) == 0)
            {
                Sandstorm.IntendedSeverity = 0f;
            }
            else
            {
                Sandstorm.IntendedSeverity = Main.rand.NextFloat() * 0.3f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
            }
        }
    }
}

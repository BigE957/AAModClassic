
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Athena
{
    public class AthenaDefeat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 15;
        }

        public override void SetDefaults()
        {
            NPC.width = 132;
            NPC.height = 104;
            NPC.npcSlots = 1000;
            NPC.aiStyle = -1;
            NPC.defense = 1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.boss = true;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.damage = 0;
            NPC.value = 0;
            NPC.noTileCollide = true;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
        }

        public override void AI()
        {
            Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;
            Vector2 Acropolis = new Vector2(Origin.X + (80 * 16), Origin.Y + (79 * 16));
            NPC.TargetClosest();
            if (Main.netMode != 1)
            {
                if (Vector2.Distance(NPC.Center, Acropolis) < 5 && Main.netMode != 1)
                {
                    NPC.velocity.X *= 0;
                    NPC.ai[1] = 1;
                    NPC.noTileCollide = false;
                    NPC.noGravity = false;
                    NPC.netUpdate = true;
                }
                if (NPC.ai[1] == 0)
                {
                    MoveToPoint(Acropolis);
                }
                else
                {
                    NPC.ai[0]++;
                    if (Main.netMode != 1)
                    {
                        if (NPC.ai[2] == 0)
                        {
                            if (NPC.ai[0] == 120)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat1"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else
                            if (NPC.ai[0] == 240)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat2"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else
                            if (NPC.ai[0] == 360)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat("...", Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else
                            if (NPC.ai[0] == 480)
                            {
                                Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AthenaA");
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat3"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else
                            if (NPC.ai[0] == 600)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat4"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else
                            if (NPC.ai[0] == 720)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat5"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else
                            if (NPC.ai[0] == 840)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat6"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else
                            if (NPC.ai[0] == 960)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat7"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else
                            if (NPC.ai[0] == 1080)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat8"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else
                            if (NPC.ai[0] >= 1200)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat9"), Color.CornflowerBlue);
                                AAModGlobalNPC.SpawnBoss(Main.player[NPC.target], ModContent.NPCType<Olympian.AthenaA>(), false, NPC.Center);

                                int b = Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ShockwaveBoom").Type, 0, 1, Main.myPlayer);
                                Main.projectile[b].Center = NPC.Center;

                                NPC.active = false;
                                NPC.netUpdate = true;
                            }
                        }
                        else
                        {
                            if (NPC.ai[0] == 120)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat("...", Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 240)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat1"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 360)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat2"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 480)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat("...", Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 600)
                            {
                                string s = "";
                                if (Main.ActivePlayersCount > 1)
                                {
                                    s = Lang.BossChat("Athena2Defeat4");
                                }
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat3") + s + "...", Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 720)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat5"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 840)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat6"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 960)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat7"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 1080)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat8"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] >= 1200)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat9"), Color.CornflowerBlue);
                                AAModGlobalNPC.SpawnBoss(Main.player[NPC.target], ModContent.NPCType<AthenaFlee>(), false, NPC.Center);
                                NPC.active = false;
                                NPC.netUpdate = true;
                            }
                        }
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.ai[1] == 0)
            {
                if (NPC.frameCounter >= 6)
                {
                    NPC.frame.Y += frameHeight;
                    NPC.frameCounter = 0;
                    if (NPC.frame.Y >= frameHeight * 7)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
            else
            {
                if (NPC.ai[2] == 0)
                {
                    if (NPC.ai[0] < 480)
                    {
                        if (NPC.frameCounter >= 15)
                        {
                            NPC.frame.Y += frameHeight;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y >= frameHeight * 10 || NPC.frame.Y < frameHeight * 7)
                            {
                                NPC.frame.Y = frameHeight * 7;
                            }
                        }
                    }
                    else if (NPC.ai[0] >= 480 && NPC.ai[0] < 720)
                    {
                        NPC.frame.Y = frameHeight * 10;
                    }
                    else if (NPC.ai[0] >= 720)
                    {
                        if (NPC.frameCounter >= 15)
                        {
                            NPC.frame.Y += frameHeight;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y < frameHeight * 11 || NPC.frame.Y >= frameHeight * 15)
                            {
                                NPC.frame.Y = frameHeight * 11;
                            }
                        }
                    }
                }
                else
                {
                    if (NPC.ai[0] < 270)
                    {
                        if (NPC.frameCounter >= 15)
                        {
                            NPC.frame.Y += frameHeight;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y >= frameHeight * 10 || NPC.frame.Y < frameHeight * 7)
                            {
                                NPC.frame.Y = frameHeight * 7;
                            }
                        }
                    }
                    else if (NPC.ai[0] >= 270 && NPC.ai[0] < 450)
                    {
                        NPC.frame.Y = frameHeight * 10;
                    }
                    else if (NPC.ai[0] >= 450)
                    {
                        if (NPC.frameCounter >= 15)
                        {
                            NPC.frame.Y += frameHeight;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y < frameHeight * 11 || NPC.frame.Y >= frameHeight * 15)
                            {
                                NPC.frame.Y = frameHeight * 11;
                            }
                        }
                    }
                }
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 14f;
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
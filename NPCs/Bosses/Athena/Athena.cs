using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Effects;
using AAModClassic.Items.Boss.Athena;
using AAModClassic.Items.Vanity.Mask;
using AAModClassic.NPCs.Enemies.Sky;
using AAModClassic.UI.Titles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Athena
{
    [AutoloadBossHead]
    public class Athena : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 7;
        }

        public static Point CloudPoint = new Point((int)(Main.maxTilesX * 0.65f), 100);
        public Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;
        public int damage = 0;

        public override void SetDefaults()
        {
            NPC.width = 152;
            NPC.height = 114;
            NPC.value = Item.sellPrice(0, 10, 0, 0);
            NPC.npcSlots = 1000;
            NPC.aiStyle = -1;
            NPC.lifeMax = 40000;
            NPC.defense = 20;
            NPC.damage = 90;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.boss = true;
            Music = MusicLoader.GetMusicSlot("AAModClassic/Music/Athena");
            NPC.alpha = 255;
            NPC.noTileCollide = true;
            //bossBag/* tModPorter Note: _Unreleased. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ModContent.ItemType<AthenaBag>();
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        public float[] internalAI = new float[5];
        public float[] FlyAI = new float[2];
        public Vector2 MoveVector2;
        public bool Seen = false;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
                writer.Write(FlyAI[0]);
                writer.Write(FlyAI[1]);
                writer.Write(MoveVector2.X);
                writer.Write(MoveVector2.Y);
                writer.Write(Seen);
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
                internalAI[3] = reader.ReadSingle();
                internalAI[4] = reader.ReadSingle();
                FlyAI[0] = reader.ReadSingle();
                FlyAI[1] = reader.ReadSingle();
                MoveVector2.X = reader.ReadSingle();
                MoveVector2.Y = reader.ReadSingle();
                Seen = reader.ReadBoolean();
            }
        }
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
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            Vector2 Acropolis = new Vector2(Origin.X + (80 * 16), Origin.Y + (79 * 16));

            //Preamble Shite 

            if (internalAI[2] != 1)
            {
                NPC.dontTakeDamage = true;
                Music = MusicLoader.GetMusicSlot("AAModClassic/Music/silence");
                if (Vector2.Distance(NPC.Center, Acropolis) < 10)
                {
                    NPC.velocity *= 0;

                    if (Seen)
                    {
                        if (player.Center.X < NPC.Center.X + 32)
                        {
                            NPC.direction = -1;
                        }
                        else
                        {
                            NPC.direction = 1;
                        }
                    }

                    if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && internalAI[3] < 180)
                    {
                        Seen = true;
                        NPC.netUpdate = true;
                    }

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (!Seen)
                        {
                            internalAI[4]++; 
                            if (internalAI[4] == 60)
                            {
                                CombatText.NewText(NPC.Hitbox, Color.CadetBlue, "...");
                            }

                            if (internalAI[4] == 180)
                            {
                                CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.AthenaChat1"));
                            }

                            if (internalAI[4] >= 300)
                            {
                                CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.AthenaChat2"));
                                NPC.active = false;
                                int p = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AthenaFlee>());
                                Main.npc[p].Center = NPC.Center;
                            }
                            return;
                        }

                        if (internalAI[3]++ < 420)
                        {
                            if (!AAWorld.downedAthena)
                            {

                                if (internalAI[3] == 60)
                                {
                                    CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena1"));
                                }

                                if (internalAI[3] == 180)
                                {
                                    string s = "";
                                    int activePlayers = 0;
                                    foreach (Player p in Main.ActivePlayers)
                                        activePlayers++;
                                    if (activePlayers > 1)
                                    {
                                        s = Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena2");
                                    }
                                    CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena3") + s + "!");
                                }

                                if (internalAI[3] == 300)
                                {
                                    CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena4"));
                                }

                                if (internalAI[3] == 420)
                                {
                                    CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena5"));
                                }

                                if (internalAI[3] >= 420)
                                {
                                    CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena6"));
                                    NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;
                                    internalAI[2] = 1;

                                    NPC.netUpdate = true;
                                }
                            }
                            else if (AAWorld.AthenaHerald && !AAWorld.downedAthenaA)
                            {
                                if (internalAI[3] == 60)
                                {
                                    CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena12"));
                                }

                                if (internalAI[3] == 180)
                                {
                                    CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena13"));
                                }

                                if (internalAI[3] == 300)
                                {
                                    CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena14"));
                                    NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;
                                    internalAI[2] = 1;
                                    NPC.netUpdate = true;
                                }
                            }
                            else
                            {
                                if (internalAI[3] == 60)
                                {
                                    CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena7"));
                                }

                                if (internalAI[3] >= 180)
                                {
                                    CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena8"));
                                    NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;
                                    internalAI[2] = 1;
                                    NPC.netUpdate = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    NPC.spriteDirection = NPC.direction = NPC.velocity.X > 0 ? 1 : -1;
                    MoveToVector2(Acropolis);
                }
            }
            else
            {
                if (player.Center.X < NPC.Center.X + 32)
                {
                    NPC.direction = -1;
                }
                else
                {
                    NPC.direction = 1;
                }

                NPC.dontTakeDamage = false;
                if (player.dead || !player.active || Vector2.Distance(NPC.position, player.position) > 5000 || !modPlayer.ZoneAcropolis)
                {
                    NPC.TargetClosest();
                    if (player.dead || !player.active || Math.Abs(Vector2.Distance(NPC.position, player.position)) > 5000 || !modPlayer.ZoneAcropolis)
                    {
                        CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena9"));
                        int p = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AthenaFlee>());
                        Main.npc[p].Center = NPC.Center;
                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                }

                Music = MusicLoader.GetMusicSlot("AAModClassic/Music/Athena");

                if (internalAI[0]++ > 300 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int pChoice = Main.rand.Next(2);
                    if (pChoice == 0)
                    {
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<OwlRune>());
                    }
                    internalAI[0] = 0;
                }

                if (internalAI[1] == 0) //Acropolis Phase
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[3]++;
                    }

                    if (Vector2.Distance(player.Center, Acropolis) > 1280)
                    {
                        if (NPC.ai[2] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            NPC.ai[2] = 1;
                            NPC.netUpdate = true;
                        }
                        MoveToVector2(Acropolis);
                    }
                    else
                    {
                        if (NPC.ai[2] == 1 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            NPC.ai[2] = 0;
                            NPC.netUpdate = true;
                        }
                        BaseAI.AISpaceOctopus(NPC, ref FlyAI, Main.player[NPC.target].Center, 0.1f, 8f, 220f, 70f, ShootFeather);
                    }

                    if (NPC.ai[3] > 600)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            internalAI[1] = 1;
                            NPC.ai[0] = 0;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                            MoveVector2 = CloudPick();
                            NPC.netUpdate = true;
                        }
                    }
                }
                else //Cloud Phase
                {
                    if (MoveVector2 == new Vector2(0, 0) && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        MoveVector2 = CloudPick();
                        NPC.netUpdate = true;
                    }
                    NPC.ai[1]++;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (NPC.ai[1] == 300)
                        {
                            if (Main.rand.Next(5) == 0)
                            {
                                internalAI[1] = 0;
                                NPC.ai[0] = 0;
                                NPC.ai[1] = 0;
                                NPC.ai[2] = 0;
                                NPC.ai[3] = 0;
                                NPC.netUpdate = true;
                                return;
                            }
                            NPC.ai[0] = 0;
                            MoveVector2 = CloudPick();
                            NPC.netUpdate = true;
                        }
                    }
                    if (Vector2.Distance(NPC.Center, MoveVector2) < 10)
                    {
                        if (NPC.ai[2] == 1 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.netUpdate = true;
                        }
                        NPC.velocity *= 0;

                        if (NPC.ai[1] % 200 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int Choice = Main.rand.Next(2);
                            if (Choice == 0)
                            {
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 100, (int)NPC.Center.Y, ModContent.NPCType<OlympianDragon>());
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X - 100, (int)NPC.Center.Y, ModContent.NPCType<OlympianDragon>());
                            }
                            else
                            {
                                NPC Seraph1 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y + 100, ModContent.NPCType<SeraphA>())];
                                for (int i = 0; i < 3; i++)
                                {
                                   Dust d = Main.dust[Dust.NewDust(Seraph1.position, Seraph1.height, Seraph1.width, ModContent.DustType<Feather>(), Main.rand.Next(-1, 2), 1, 0)];
                                }
                                NPC Seraph2 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 100, (int)NPC.Center.Y - 50, ModContent.NPCType<SeraphA>())];
                                for (int i = 0; i < 3; i++)
                                {
                                    Dust d = Main.dust[Dust.NewDust(Seraph2.position, Seraph2.height, Seraph2.width, ModContent.DustType<Feather>(), Main.rand.Next(-1, 2), 1, 0)];
                                }
                                NPC Seraph3 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 100, (int)NPC.Center.Y - 50, ModContent.NPCType<SeraphA>())];
                                for (int i = 0; i < 3; i++)
                                {
                                    Dust d = Main.dust[Dust.NewDust(Seraph3.position, Seraph3.height, Seraph3.width, ModContent.DustType<Feather>(), Main.rand.Next(-1, 2), 1, 0)];
                                }
                            }
                            NPC.netUpdate = true;
                        }

                        if (NPC.ai[1] % 60 == 0)
                        {
                            if (Vector2.Distance(player.Center, NPC.Center) < 900)
                            {
                                ShootFeather(NPC, NPC.velocity);
                            }
                        }
                    }
                    else
                    {
                        if (NPC.ai[2] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            NPC.ai[2] = 1;
                            NPC.netUpdate = true;
                        }
                        MoveToVector2(MoveVector2);
                    }
                }
            }

            NPC.rotation = 0;
        }

        public Vector2 CloudPick()
        {
            int CloudChoice = Main.rand.Next(12);
            Vector2 Cloud1 = new Vector2(Origin.X + (79 * 16), Origin.Y + (10 * 16));
            Vector2 Cloud2 = new Vector2(Origin.X + (112 * 16), Origin.Y + (19 * 16));
            Vector2 Cloud3 = new Vector2(Origin.X + (135 * 16), Origin.Y + (40 * 16));
            Vector2 Cloud4 = new Vector2(Origin.X + (140 * 16), Origin.Y + (69 * 16));
            Vector2 Cloud5 = new Vector2(Origin.X + (135 * 16), Origin.Y + (99 * 16));
            Vector2 Cloud6 = new Vector2(Origin.X + (112 * 16), Origin.Y + (120 * 16));
            Vector2 Cloud7 = new Vector2(Origin.X + (79 * 16), Origin.Y + (129 * 16));
            Vector2 Cloud8 = new Vector2(Origin.X + (46 * 16), Origin.Y + (120 * 16));
            Vector2 Cloud9 = new Vector2(Origin.X + (23 * 16), Origin.Y + (99 * 16));
            Vector2 Cloud10 = new Vector2(Origin.X + (18 * 16), Origin.Y + (69 * 16));
            Vector2 Cloud11 = new Vector2(Origin.X + (23 * 16), Origin.Y + (40 * 16));
            Vector2 Cloud12 = new Vector2(Origin.X + (46 * 16), Origin.Y + (19 * 16));
            if (CloudChoice == 1)
            {
                return Cloud2;
            }
            else if (CloudChoice == 2)
            {
                return Cloud3;
            }
            else if (CloudChoice == 3)
            {
                return Cloud4;
            }
            else if (CloudChoice == 4)
            {
                return Cloud5;
            }
            else if (CloudChoice == 5)
            {
                return Cloud6;
            }
            else if (CloudChoice == 6)
            {
                return Cloud7;
            }
            else if (CloudChoice == 7)
            {
                return Cloud8;
            }
            else if (CloudChoice == 8)
            {
                return Cloud9;
            }
            else if (CloudChoice == 9)
            {
                return Cloud10;
            }
            else if (CloudChoice == 10)
            {
                return Cloud11;
            }
            else if (CloudChoice == 11)
            {
                return Cloud12;
            }
            else
            {
                return Cloud1;
            }

        }

        public void ShootFeather(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            int projType = ModContent.ProjectileType<SeraphFeather>();
            float spread = 30f * 0.0174f;
            Vector2 dir = Vector2.Normalize(player.Center - npc.Center);
            dir *= 14f;
            float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
            double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
            double deltaAngle = spread / 6f;
            for (int i = 0; i < 3; i++)
            {
                double offsetAngle = startAngle + (deltaAngle * i);
                int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), projType, damage, 2, Main.myPlayer);
                Main.projectile[p].tileCollide = false;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 6)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y >= frameHeight * 7)
            {
                NPC.frame.Y = 0;
            }
        }

        public void MoveToVector2(Vector2 p)
        {
            float moveSpeed = 25f;
            if (internalAI[2] != 1)
            {
                moveSpeed = 14f;
            }
            float velMultiplier = 1f;
            Vector2 dist = p - NPC.Center;
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
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void OnKill()
        {
            AAWorld.downedAthena = true;

            if (NPC.downedMoonlord)
            {
                if (!AAWorld.downedAthenaA)
                {
                    int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AthenaDefeat>());
                    Main.npc[a].Center = NPC.Center;
                }
                else
                {
                    int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Olympian.AthenaA>());
                    Main.npc[a].Center = NPC.Center;
                    int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 1, Main.myPlayer, 0, 0);
                    Main.projectile[b].Center = NPC.Center;
                    CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena10"));

                    Main.projectile[b].netUpdate = true;
                }
                return;
            }

            if (Main.expertMode)
            {
                NPC.DropLoot(ModContent.ItemType<AthenaBag>());       
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    NPC.DropLoot(ModContent.ItemType<AthenaMask>());
                }
                NPC.DropLoot(ModContent.ItemType<GoddessFeather>(), Main.rand.Next(20, 25));
                string[] lootTable = { "DivineWindCharm", "GaleOfWings", "RazorwindLongbow", "SkycutterKopis", "OlympianWings"};
                int loot = Main.rand.Next(lootTable.Length);
                NPC.DropLoot(Mod.Find<ModItem>(lootTable[loot]).Type);
            }


            CombatText.NewText(NPC.Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena11"));
            int p = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AthenaFlee>());
            Main.npc[p].Center = NPC.Center;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = internalAI[2] != 1 ? AAMod.GetTexture("NPCs/Bosses/Athena/SassyBitch") : TextureAssets.Npc[NPC.type].Value;
            Color lightColor = BaseDrawing.GetLightColor(NPC.Center);

            if (NPC.ai[2] == 1)
            {
                BaseDrawing.DrawAfterimage(spriteBatch, tex, 0, NPC.position, NPC.width, NPC.height, NPC.oldPos, NPC.scale, NPC.rotation, NPC.direction, 7, NPC.frame, 1f, 1f, 5, false, 0f, 0f);
            }
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 7, NPC.frame, lightColor);
            return false;
        }
    }

    public class AthenaFlee : ModNPC
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Athena/Athena";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Athena");
            Main.npcFrameCount[NPC.type] = 7;
        }
        public override void SetDefaults()
        {
            NPC.width = 152;
            NPC.height = 114;
            NPC.npcSlots = 1000;
            NPC.aiStyle = -1;
            NPC.defense = 1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = true;
            NPC.damage = 0;
            NPC.value = 0;
        }

        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0]++ >= 120)
            {
                if (NPC.ai[0] >= 120 && NPC.ai[0] < 130)
                {
                    NPC.velocity.Y += 1f;
                    NPC.netUpdate = true;
                }
                else if (NPC.ai[0] == 130)
                {
                    NPC.netUpdate = true;
                }
                else if (NPC.ai[0] >= 130)
                {
                    NPC.velocity.Y -= 0.5f;
                    if (NPC.velocity.Y < -8f) NPC.velocity.Y = -8f;
                }
                if (NPC.position.Y + NPC.velocity.Y <= 0f && Main.netMode != NetmodeID.MultiplayerClient) { BaseAI.KillNPC(NPC); NPC.netUpdate = true; }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 6)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
            {
                NPC.frame.Y = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.oldPos, NPC.scale, NPC.rotation, NPC.direction, 7, NPC.frame, 1f, 1f, 5, false, 0f, 0f);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 7, NPC.frame, NPC.GetAlpha(drawColor), false);
            return false;
        }
    }
}
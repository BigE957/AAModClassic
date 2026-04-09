using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Items.Boss.Akuma;
using AAModClassic.Items.Vanity.Mask;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic.NPCs.Bosses.Akuma
{
    [AutoloadBossHead]
    public class Akuma : ModNPC
    {
        public override string Texture { get { return "AAModClassic/NPCs/Bosses/Akuma/Akuma"; } }

        public bool loludided;
        private bool weakness;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Akuma; Draconian Demon");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        public override void SetDefaults()
        {
            NPC.noTileCollide = true;
            NPC.height = 80;
            NPC.width = 80;
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.knockBackResist = 0f;
            NPC.damage = 140;
            NPC.defense = 80;
            NPC.lifeMax = 400000;
            if (Main.expertMode)
            {
                NPC.value = Item.sellPrice(0, 0, 0, 0);
            }
            else
            {
                NPC.value = Item.sellPrice(0, 30, 0, 0);
            }
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            Music = MusicManagementSystem.MusicSlots["Akuma"];
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/AkumaRoar");
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.buffImmune[103] = false;
            NPC.alpha = 255;
            SceneEffectPriority = SceneEffectPriority.BossHigh;
        }

        private bool fireAttack;
        private int attackFrame;
        private int attackCounter;
        private int attackTimer;
        public static int MinionCount = 0;
        public int MaxMinons = Main.expertMode ? 3 : 4;
        public int damage = 0;

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
                internalAI[0] = reader.ReadSingle();
                internalAI[1] = reader.ReadSingle();
                internalAI[2] = reader.ReadSingle();
                internalAI[3] = reader.ReadSingle();
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override bool PreAI()
        {
            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

            Player player = Main.player[NPC.target];
            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }

            if (fireAttack == true || internalAI[0] >= 450)
            {
                attackCounter++;
                if (attackCounter > 10)
                {
                    attackFrame++;
                    attackCounter = 0;
                }
                if (attackFrame >= 3)
                {
                    attackFrame = 2;
                }
            }
            float dist = NPC.Distance(player.Center);
            internalAI[0]++;
            if (internalAI[0] == 350)
            {
                QuoteSaid = false;
                Roar(roarTimerMax, false);
                internalAI[1] = Main.rand.Next(3);
            }
            if (internalAI[0] > 300)
            {
                Attack(NPC);
            }
            if (internalAI[0] >= 400)
            {
                internalAI[0] = 0;
            }

            if (dist > 300 & Main.rand.Next(20) == 1 && fireAttack == false && internalAI[0] < 500)
            {
                fireAttack = true;
            }

            if (fireAttack == true)
            {
                attackTimer++;
                if ((attackTimer % 20 == 0) && NPC.HasBuff(BuffID.Wet))
                {
                    for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                    {
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.MireBubbleDust>(), 0f, 0f, 90, default, 2f);
                        Main.dust[num935].noGravity = true;
                        Main.dust[num935].velocity.Y -= 1f;
                    }
                    if (weakness == false)
                    {
                        weakness = true;
                        if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma1"), new Color(180, 41, 32));
                    }
                }
                else if (!NPC.HasBuff(BuffID.Wet))
                {
                    AAAI.BreatheFire(NPC, true, ModContent.ProjectileType<AkumaBreath>(), 2, 4);
                }
                if (attackTimer >= 80)
                {
                    fireAttack = false;
                    attackTimer = 0;
                    attackFrame = 0;
                    attackCounter = 0;
                }
            }
            AAAI.DustOnNPCSpawn(NPC, ModContent.DustType<Dusts.AkumaDust>(), 2, 12);

            NPC.spriteDirection = NPC.velocity.X > 0 ? -1 : 1;
            NPC.ai[1]++;
            if (NPC.ai[1] >= 1200)
                NPC.ai[1] = 0;
            NPC.TargetClosest(true);
            if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
                {
                    NPC.ai[3]++;
                    NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                    if (NPC.ai[3] >= 300)
                    {
                        NPC.active = false;
                    }
                }
                else
                    NPC.ai[3] = 0;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 0)
                {
                    NPC.realLife = NPC.whoAmI;
                    int latestNPC = NPC.whoAmI;
                    int[] Frame = { 1, 2, 0, 1, 2, 1, 2, 0, 1, 2, 1, 2, 0, 1, 2, 3, 4 };
                    for (int i = 0; i < Frame.Length; ++i)
                    {
                        latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AkumaBody>(), NPC.whoAmI, 0, latestNPC);
                        Main.npc[latestNPC].realLife = NPC.whoAmI;
                        Main.npc[latestNPC].ai[3] = NPC.whoAmI;
                        Main.npc[latestNPC].netUpdate = true;
                        Main.npc[latestNPC].ai[2] = Frame[i];
                    }
                    NPC.ai[0] = 1;
                    NPC.netUpdate2 = true;
                }
            }

            bool collision = true;

            float speed = 12f;
            float acceleration = 0.13f;

            Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float targetXPos = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2);
            float targetYPos = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2);

            float targetRoundedPosX = (int)(targetXPos / 16.0) * 16;
            float targetRoundedPosY = (int)(targetYPos / 16.0) * 16;
            npcCenter.X = (int)(npcCenter.X / 16.0) * 16;
            npcCenter.Y = (int)(npcCenter.Y / 16.0) * 16;
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;

            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            if (!collision)
            {
                NPC.TargetClosest(true);
                NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                if (NPC.velocity.Y > speed)
                    NPC.velocity.Y = speed;
                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.4)
                {
                    if (NPC.velocity.X < 0.0)
                        NPC.velocity.X = NPC.velocity.X - acceleration * 1.1f;
                    else
                        NPC.velocity.X = NPC.velocity.X + acceleration * 1.1f;
                }
                else if (NPC.velocity.Y == speed)
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                }
                else if (NPC.velocity.Y > 4.0)
                {
                    if (NPC.velocity.X < 0.0)
                        NPC.velocity.X = NPC.velocity.X + acceleration * 0.9f;
                    else
                        NPC.velocity.X = NPC.velocity.X - acceleration * 0.9f;
                }
            }
            else
            {
                if (NPC.soundDelay == 0)
                {
                    float num1 = length / 40f;
                    if (num1 < 10.0)
                        num1 = 10f;
                    if (num1 > 20.0)
                        num1 = 20f;
                    NPC.soundDelay = (int)num1;
                }
                float absDirX = Math.Abs(dirX);
                float absDirY = Math.Abs(dirY);
                float newSpeed = speed / length;
                dirX *= newSpeed;
                dirY *= newSpeed;
                if (NPC.velocity.X > 0.0 && dirX > 0.0 || NPC.velocity.X < 0.0 && dirX < 0.0 || NPC.velocity.Y > 0.0 && dirY > 0.0 || NPC.velocity.Y < 0.0 && dirY < 0.0)
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                    if (NPC.velocity.Y < dirY)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration;
                    else if (NPC.velocity.Y > dirY)
                        NPC.velocity.Y = NPC.velocity.Y - acceleration;
                    if (Math.Abs(dirY) < speed * 0.2 && (NPC.velocity.X > 0.0 && dirX < 0.0 || NPC.velocity.X < 0.0 && dirX > 0.0))
                    {
                        if (NPC.velocity.Y > 0.0)
                            NPC.velocity.Y = NPC.velocity.Y + acceleration * 2f;
                        else
                            NPC.velocity.Y = NPC.velocity.Y - acceleration * 2f;
                    }
                    if (Math.Abs(dirX) < speed * 0.2 && (NPC.velocity.Y > 0.0 && dirY < 0.0 || NPC.velocity.Y < 0.0 && dirY > 0.0))
                    {
                        if (NPC.velocity.X > 0.0)
                            NPC.velocity.X = NPC.velocity.X + acceleration * 2f;
                        else
                            NPC.velocity.X = NPC.velocity.X - acceleration * 2f;
                    }
                }
                else if (absDirX > absDirY)
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration * 1.1f;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration * 1.1f;
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                    {
                        if (NPC.velocity.Y > 0.0)
                            NPC.velocity.Y = NPC.velocity.Y + acceleration;
                        else
                            NPC.velocity.Y = NPC.velocity.Y - acceleration;
                    }
                }
                else
                {
                    if (NPC.velocity.Y < dirY)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration * 1.1f;
                    else if (NPC.velocity.Y > dirY)
                        NPC.velocity.Y = NPC.velocity.Y - acceleration * 1.1f;
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                    {
                        if (NPC.velocity.X > 0.0)
                            NPC.velocity.X = NPC.velocity.X + acceleration;
                        else
                            NPC.velocity.X = NPC.velocity.X - acceleration;
                    }
                }
            }
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;

            if (!Main.dayTime)
            {
                if (loludided == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma2"), new Color(180, 41, 32));
                    loludided = true;
                }
                NPC.velocity.Y = NPC.velocity.Y + 1f;
                if (NPC.position.Y - NPC.height - NPC.velocity.Y >= Main.maxTilesY && Main.netMode != NetmodeID.MultiplayerClient) { BaseAI.KillNPC(NPC); NPC.netUpdate2 = true; }
            }

            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                if (loludided == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma3"), new Color(180, 41, 32));
                    loludided = true;
                }
                NPC.velocity.Y = NPC.velocity.Y - 1f;
                if (NPC.position.Y < 0)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 1f;
                }
                if (NPC.position.Y < 0)
                {
                    for (int num957 = 0; num957 < 200; num957++)
                    {
                        if (Main.npc[num957].aiStyle == NPC.aiStyle)
                        {
                            Main.npc[num957].active = false;
                        }
                    }
                }
            }

            if (collision)
            {
                if (NPC.localAI[0] != 1)
                    NPC.netUpdate = true;
                NPC.localAI[0] = 1f;
            }
            else
            {
                if (NPC.localAI[0] != 0.0)
                    NPC.netUpdate = true;
                NPC.localAI[0] = 0.0f;
            }
            if ((NPC.velocity.X > 0.0 && NPC.oldVelocity.X < 0.0 || NPC.velocity.X < 0.0 && NPC.oldVelocity.X > 0.0 || NPC.velocity.Y > 0.0 && NPC.oldVelocity.Y < 0.0 || NPC.velocity.Y < 0.0 && NPC.oldVelocity.Y > 0.0) && !NPC.justHit)
                NPC.netUpdate = true;

            return false;
        }

        public bool Quote1;
        public bool Quote2;
        public bool Quote3;
        public bool Quote4;
        public bool Quote5;
        public bool QuoteSaid;

        public void Attack(NPC npc)
        {
            bool sayQuote = Main.rand.Next(4) == 0;
            if (internalAI[1] == 0)
            {
                if (!QuoteSaid && sayQuote)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat((!Quote1) ? Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma4") : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma5"), new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote1 = true;
                }
                if (internalAI[0] == 320 || internalAI[0] == 340 || internalAI[0] == 360 || internalAI[0] == 380)
                {
                    int Fireballs = Main.expertMode ? 10 : 8;
                    for (int Loops = 0; Loops < Fireballs; Loops++)
                    {
                        AkumaAttacks.Dragonfire(npc, Mod, false);
                    }
                }

            }
            else if (internalAI[1] == 1)
            {
                if (!QuoteSaid && sayQuote)
                {
                    if (!Quote3 || Main.rand.Next(4) == 0)
                        if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat((!Quote3) ? Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma7") : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma8"), new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote3 = true;
                }
                if (internalAI[0] == 350)
                {
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, npc.velocity.X * 2, npc.velocity.Y, ModContent.ProjectileType<AkumaFireProj>(), damage, 3, Main.myPlayer);
                }
            }
            else
            {
                if (!QuoteSaid && sayQuote)
                {
                    if (!Quote5 || Main.rand.Next(4) == 0)
                        if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat((!Quote5) ? Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma13") : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma14"), new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote5 = true;
                }
                if (internalAI[0] == 350)
                {
                    int Fireballs = Main.expertMode ? 6 : 10;
                    float spread = 70f * 0.0174f;
                    float baseSpeed = (float)Math.Sqrt((npc.velocity.X * npc.velocity.X) + (npc.velocity.Y * npc.velocity.Y));
                    double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - .1d;
                    double deltaAngle = spread / 6f;
                    double offsetAngle;
                    for (int i = 0; i < Fireballs; i++)
                    {
                        offsetAngle = startAngle + (deltaAngle * i);
                        Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle) * 2, baseSpeed * (float)Math.Cos(offsetAngle) * 2, ModContent.ProjectileType<AkumaBomb>(), damage, 3, Main.myPlayer);
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D attackAni = Mod.GetTexture("NPCs/Bosses/Akuma/Akuma");
            if (fireAttack == false)
            {
                spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (fireAttack == true)
            {
                Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
                int num214 = attackAni.Height / 3;
                int y6 = num214 * attackFrame;
                Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, attackAni.Width, num214)), drawColor, NPC.rotation, new Vector2(attackAni.Width / 2f, num214 / 2f), NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            return false;
        }

        public override void OnKill()
        {
            NPC.DropLoot(Items.Vanity.Mask.AkumaMask.type, 1f / 7);
            if (!Main.expertMode)
            {
                if (!AAWorld.downedAkuma)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma11"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                }
                string[] lootTable = { "AkumaTerratool", "DayStorm", "LungStaff", "MorningGlory", "RadiantDawn", "Solar", "SunSpear", "ReignOfFire", "DaybreakArrow", "Daycrusher", "Dawnstrike", "SunStorm", "SunStaff", "DragonSlasher" };
                AAWorld.downedAkuma = true;

                NPC.DropLoot(ModContent.ItemType<AkumaTrophy>(), 1 / 10f);

                if (Main.expertMode)
                {
                    NPC.DropLoot(ModContent.ItemType<AkumaBag>());
                }
                else
                {
                    NPC.DropLoot(ModContent.ItemType<CrucibleScale>(), 20, 30);
                    NPC.DropLoot(ModContent.ItemType<AkumaMask>(), 1 / 10f);
                    int loot = Main.rand.Next(lootTable.Length);
                    NPC.DropLoot(Mod.Find<ModItem>(lootTable[loot]).Type);
                }
                if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma12"), new Color(180, 41, 32));

            }
            if (Main.expertMode)
            {
                int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AkumaTransition>(), 0, 0, 0, 0, 0, NPC.target);
                Main.npc[npcID].Center = NPC.Center;
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
            }
            NPC.value = 0f;
            NPC.boss = false;
        }

        public override void BossLoot(ref int potionType)
        {
            if (Main.expertMode)
            {
                potionType = 0;
            }
            else
            {
                AAWorld.downedAkuma = true;
                potionType = ItemID.SuperHealingPotion;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.AkumaDust>();
                int dust2 = ModContent.DustType<Dusts.AkumaDust>();
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }


        public int roarTimer = 0; //if this is > 0, then use the roaring frame.
        public int roarTimerMax = 120; //default roar timer. only changed for fire breath as it's longer.
        public bool Roaring //wether or not he is roaring. only used clientside for frame visuals.
        {
            get
            {
                return roarTimer > 0;
            }
        }

        public void Roar(int timer, bool fireSound)
        {
            roarTimer = timer;
            if (fireSound)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath60, NPC.Center);
            }
            else
            {
                SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/AkumaRoar"), NPC.Center);
            }
        }


        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }

    [AutoloadBossHead]
    public class AkumaBody : Akuma
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Akuma/AkumaBody";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Akuma, Draconian Demon");
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.boss = false;
            NPC.width = 40;
            NPC.height = 40;
            NPC.dontCountMe = true;
            NPC.chaseable = false;
        }

        public override bool PreAI()
        {
            Vector2 chasePosition = Main.npc[(int)NPC.ai[1]].Center;
            Vector2 directionVector = chasePosition - NPC.Center;
            NPC.spriteDirection = (directionVector.X > 0f) ? 1 : -1;
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }


            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active || Main.npc[(int)NPC.ai[3]].type != ModContent.NPCType<Akuma>())
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, NPC.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - npcCenter.Y;
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    NPC.spriteDirection = 1;

                }
                else
                {
                    NPC.spriteDirection = -1;
                }

                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
            }

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.netUpdate = true;
            return false;
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.TargetDamageMultiplier *= .1f;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameHeight * (int)NPC.ai[2];
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Akuma>()))
            {
                return false;
            }
            NPC.active = false;
            return true;
        }
    }
}


using System;
using System.IO;
using AAModClassic.Backgrounds;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Pets;
using AAModClassic.Items.Vanity.Mask;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Bosses.Zero
{
    [AutoloadBossHead]
    public class Zero : ModNPC
    {
        public int damage = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zero");
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            NPC.damage = 50;
            NPC.defense = 150;
            NPC.lifeMax = 350000;
            if (Main.expertMode)
            {
                NPC.value = 0;
            }
            else
            {
                NPC.value = Item.sellPrice(0, 30, 0, 0);
            }
            NPC.width = 206;
            NPC.height = 208;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCHit4;
            NPC.noGravity = true;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Zero");
            NPC.noTileCollide = true;
            NPC.knockBackResist = -1f;
            NPC.boss = true;
            NPC.friendly = false;
            NPC.npcSlots = 100;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.lavaImmune = true;
            NPC.netAlways = true;
            SceneEffectPriority = SceneEffectPriority.BossHigh;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * .7f);
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= (int)(NPC.lifeMax * .66f) && !RespawnArms1 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                WeaponCount += 1;
                NPC.ai[1] = 0;
                RespawnArms1 = true;

                RespawnArms();
                if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ZeroBoss10"), Color.Red, false);
                NPC.netUpdate = true;
            }
            if (NPC.life <= (int)(NPC.lifeMax * .33f) && !RespawnArms2 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                WeaponCount += 1;
                NPC.ai[1] = 0;
                RespawnArms2 = true;
                RespawnArms();
                if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ZeroBoss10"), Color.Red, false);
                NPC.netUpdate = true;
            }

            if (NPC.life <= 0 && NPC.type == ModContent.NPCType<Zero>())
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore3").Type, 1f);
                if (!Main.expertMode)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ZeroBoss2"), Color.Red.R, Color.Red.G, Color.Red.B);
                }
            }
        }

        bool hasArms = false;
        public void RespawnArms()
        {
            hasArms = NPC.AnyNPCs(ModContent.NPCType<VoidStar>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<Taser>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<RealityCannon>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<RiftShredder>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<Neutralizer>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<OmegaVolley>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<NovaFocus>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<GenocideCannon>());

            if (Main.netMode != NetmodeID.MultiplayerClient && !hasArms)
            {
                NPC.ai[0] = 10f;

                for (int m = 0; m < WeaponCount; m++)
                {
                    int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>(ArmChoice()).Type, 0, m);
                    Main.npc[npcID].Center = NPC.Center;
                    Main.npc[npcID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                    Main.npc[npcID].velocity *= 8f;
                    Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
                }

                internalAI[3] = 1;
                Distance = 0;
                NPC.netUpdate = true;
            }
        }

        public override void OnKill()
        {
            if (Main.expertMode)
            {
                NPC.DropLoot(Mod.Find<ModItem>("ApocalyptitePlate").Type, 2, 4);

                if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("ZeroBoss1"), Color.Red.R, Color.Red.G, Color.Red.B);
                if (AAWorld.downedZero)
                {
                    int z = NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, Mod.Find<ModNPC>("ZeroProtocol").Type, 0, 0, 0, 0, 0, NPC.target);
                    Main.npc[z].Center = NPC.Center;

                    int b = Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ShockwaveBoom").Type, 0, 1, Main.myPlayer, 0, 0);
                    Main.projectile[b].Center = NPC.Center;
                }
                else
                {
                    int z = NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, Mod.Find<ModNPC>("ZeroTransition").Type, 0, 0, 0, 0, 0, NPC.target);
                    Main.npc[z].Center = NPC.Center;
                }

                NPC.netUpdate = true;
            }
            else
            {
                if (!AAWorld.downedZero)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) 
                        BaseUtility.Chat(Lang.BossChat("ZeroBoss3"), Color.PaleVioletRed);
                    VoidSky.Alpha = 0f;
                }
                AAWorld.downedZero = true;
                NPC.DropLoot(Mod.Find<ModItem>("ApocalyptitePlate").Type, 2, 4);
                NPC.DropLoot(Mod.Find<ModItem>("UnstableSingularity").Type, 25, 35);
                string[] lootTable =
                {
                    "Battery",
                    "ZeroArrow",
                    "Vortex",
                    "EventHorizon",
                    "RealityCannon",
                    "RiftShredder",
                    "VoidStar",
                    "TeslaHand",
                    "ZeroStar",
                    "Neutralizer",
                    "ZeroTerratool",
                    "DoomPortal",
                    "Gigataser",
                    "OmegaVolley",
                    "GenocideCannon"
                };
                int loot = Main.rand.Next(lootTable.Length);
                NPC.DropLoot(Mod.Find<ModItem>(lootTable[loot]).Type);
                NPC.DropLoot(ModContent.ItemType<ZeroCore>(), 1f / 10f);
                NPC.DropLoot(ModContent.ItemType<ZeroMask>(), 1f / 7f);
                NPC.DropLoot(ModContent.ItemType<Items.Boss.Zero.ZeroTrophy>(), 1f / 10f);
                if (Main.rand.Next(50) == 0 && AAWorld.downedAllAncients)
                {
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("RealityStone").Type);
                }
            }
        }

        public override void BossLoot(ref int potionType)
        {
            if (!Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;   //boss drops
            }
            else
            {
                potionType = 0;
            }
        }

        public static Color GetGlowAlpha()
        {
            return AAColor.ZeroShield * (Main.mouseTextColor / 255f);
        }

        public int frameCounters;
        public int normalFrame;
        public int switchOneFrame;
        public int openFrame;
        public int switchTwoFrame;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Mod.GetTexture("Glowmasks/Zero_Glow");
            Texture2D Shield = Mod.GetTexture("NPCs/Bosses/Zero/ZeroShield");
            Texture2D Ring = Mod.GetTexture("NPCs/Bosses/Zero/ZeroShieldRing");
            Texture2D RingGlow = Mod.GetTexture("Glowmasks/ZeroShieldRing_Glow");
            Texture2D normalAni = Mod.GetTexture("NPCs/Bosses/Zero/Zer01");
            Texture2D normalGlow = Mod.GetTexture("NPCs/Bosses/Zero/Zer01_Glow");
            Texture2D switchOneAni = Mod.GetTexture("NPCs/Bosses/Zero/Zer01to2");
            Texture2D switchOneGlow = Mod.GetTexture("NPCs/Bosses/Zero/Zer01to2_Glow");
            Texture2D openAni = Mod.GetTexture("NPCs/Bosses/Zero/Zer02");
            Texture2D openGlow = Mod.GetTexture("NPCs/Bosses/Zero/Zer02_Glow");
            Texture2D switchTwoAni = Mod.GetTexture("NPCs/Bosses/Zero/Zer02to1");
            Texture2D switchTwoGlow = Mod.GetTexture("NPCs/Bosses/Zero/Zer02to1_Glow");
            Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
            if (NPC.ai[1] == 0)
            {
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, drawColor);
                BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC, AAColor.COLOR_WHITEFADE1);
            }
            else if (NPC.ai[1] == 1)
            {
                int num214 = normalAni.Height / 5;
                int y6 = num214 * normalFrame;
                Main.spriteBatch.Draw(normalAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, normalAni.Width, num214)), drawColor * ((255 - NPC.alpha) / 255f), NPC.rotation, new Vector2(normalAni.Width / 2f, num214 / 2f), NPC.scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(normalGlow, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, normalAni.Width, num214)), AAColor.COLOR_WHITEFADE1, NPC.rotation, new Vector2(normalAni.Width / 2f, num214 / 2f), NPC.scale, NPC.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            else if (NPC.ai[1] == 2)
            {
                int num214 = switchOneAni.Height / 5;
                int y6 = num214 * switchOneFrame;
                Main.spriteBatch.Draw(switchOneAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, switchOneAni.Width, num214)), drawColor * ((255 - NPC.alpha) / 255f), NPC.rotation, new Vector2(switchOneAni.Width / 2f, num214 / 2f), NPC.scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(switchOneGlow, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, switchOneAni.Width, num214)), AAColor.COLOR_WHITEFADE1, NPC.rotation, new Vector2(switchOneAni.Width / 2f, num214 / 2f), NPC.scale, NPC.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            else if (NPC.ai[1] == 3)
            {
                int num214 = openAni.Height / 5;
                int y6 = num214 * openFrame;
                Main.spriteBatch.Draw(openAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, openAni.Width, num214)), drawColor * ((255 - NPC.alpha) / 255f), NPC.rotation, new Vector2(openAni.Width / 2f, num214 / 2f), NPC.scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(openGlow, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, openAni.Width, num214)), AAColor.COLOR_WHITEFADE1, NPC.rotation, new Vector2(openAni.Width / 2f, num214 / 2f), NPC.scale, NPC.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            else if (NPC.ai[1] == 4)
            {
                int num214 = switchTwoAni.Height / 5;
                int y6 = num214 * switchTwoFrame;
                Main.spriteBatch.Draw(switchTwoAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, switchTwoAni.Width, num214)), drawColor * ((255 - NPC.alpha) / 255f), NPC.rotation, new Vector2(switchTwoAni.Width / 2f, num214 / 2f), NPC.scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(switchTwoGlow, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, switchTwoAni.Width, num214)), AAColor.COLOR_WHITEFADE1, NPC.rotation, new Vector2(switchTwoAni.Width / 2f, num214 / 2f), NPC.scale, NPC.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }


            if (ShieldScale > 0)
            {
                BaseDrawing.DrawTexture(spriteBatch, Shield, 0, NPC.position, NPC.width, NPC.height, ShieldScale, 0, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), GetGlowAlpha(), true);
                BaseDrawing.DrawTexture(spriteBatch, Ring, 0, NPC.position, NPC.width, NPC.height, ShieldScale * 2, RingRoatation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), drawColor, true);
                BaseDrawing.DrawTexture(spriteBatch, RingGlow, 0, NPC.position, NPC.width, NPC.height, ShieldScale * 2, RingRoatation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), AAColor.COLOR_WHITEFADE1, true);
            }
            return false;
        }

        public int MinionTimer = 0;
        public float Distance = 0;
        public float[] internalAI = new float[5];

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
                writer.Write(Distance);
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
                internalAI[4] = reader.ReadFloat();
                Distance = reader.ReadFloat();
            }
        }


        public bool saythelinezero = false;
        public float ShieldScale = 0.5f;
        public float RingRoatation = 0;
        public int WeaponCount = Main.expertMode ? 6 : 4;
        bool RespawnArms1;
        bool RespawnArms2;

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

            if (NPC.ai[0] > 0)
            {
                NPC.ai[0]--;
            }

            NPC.TargetClosest();

            if (Main.netMode != NetmodeID.MultiplayerClient && internalAI[3] == 0 && NPC.ai[1] == 0)
            {
                RespawnArms();
                NPC.netUpdate = true;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<VoidStar>()) ||
                NPC.AnyNPCs(ModContent.NPCType<Taser>()) ||
                NPC.AnyNPCs(ModContent.NPCType<RealityCannon>()) ||
                NPC.AnyNPCs(ModContent.NPCType<RiftShredder>()) ||
                NPC.AnyNPCs(ModContent.NPCType<Neutralizer>()) ||
                NPC.AnyNPCs(ModContent.NPCType<OmegaVolley>()) ||
                NPC.AnyNPCs(ModContent.NPCType<NovaFocus>()) ||
                NPC.AnyNPCs(ModContent.NPCType<GenocideCannon>()))
            {
                NPC.ai[1] = 0;
            }
            else
            {
                if (NPC.ai[1] == 0)
                {
                    NPC.ai[1] = 1;
                }
            }

            if (Distance < 160f)
            {
                Distance += 5f;
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Distance = 160f;
                    NPC.netUpdate = true;
                }
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                AAWorld.zeroUS = false;
            }

            Player player = Main.player[NPC.target];

            RingRoatation += 0.03f;

            if (player.dead || !player.active || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                NPC.TargetClosest();
                if (player.dead || !player.active || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
                {
                    NPC.Transform(ModContent.NPCType<ZeroDeactivated>());
                }
                return;
            }
            if (ShieldScale < .5f)
            {
                ShieldScale += .05f;
            }

            if (ShieldScale > .5f)
            {
                ShieldScale = .5f;
            }

            if (internalAI[1] == 0)
            {
                NPC.velocity.Y += 0.003f;
                if (NPC.velocity.Y > .3f)
                {
                    internalAI[1] = 1f;
                    NPC.netUpdate = true;
                }
            }
            else if (internalAI[1] == 1)
            {
                NPC.velocity.Y -= 0.003f;
                if (NPC.velocity.Y < -.3f)
                {
                    internalAI[1] = 0f;
                    NPC.netUpdate = true;
                }
            }

            if (NPC.ai[1] == 0)
            {
                NPC.dontTakeDamage = true;
                NPC.chaseable = false;
                NPC.damage = 0;
                saythelinezero = false;
            }
            else
            {
                NPC.dontTakeDamage = false;
                NPC.chaseable = true;
                NPC.damage = 160;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[2]++;
                }

                if (NPC.ai[3] == 3)
                {
                    NPC.defense = 75;
                }
                else
                {
                    NPC.defense = 150;
                }

                if (NPC.ai[3] == 0)
                {
                    if (NPC.ai[2] % 20 == 0)
                    {
                        float Speed = 16f;
                        Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                        int type = Mod.Find<ModProjectile>("ZeroBeam1").Type;
                        SoundEngine.PlaySound(SoundID.Item33, NPC.position);
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), vector8.X, vector8.Y, (float)(Math.Cos(rotation) * Speed * -1), (float)(Math.Sin(rotation) * Speed * -1), type, damage, 0f, 0);
                    }
                    if (NPC.ai[2] >= 141 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 3;
                    }
                }
                else if (NPC.ai[3] == 1)
                {
                    if (NPC.ai[2] % 30 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item74, NPC.position);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0 + Main.rand.Next(-14, 14), 0 + Main.rand.Next(-14, 14), Mod.Find<ModProjectile>("ZeroRocket").Type, damage, 3); //Originally 85 damage
                    }
                    if (NPC.ai[2] >= 151 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 3;
                    }
                }
                else if (NPC.ai[3] == 2)
                {
                    if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.Center, player.width, player.height))
                    {
                        int[] array4 = new int[5];
                        Vector2[] array5 = new Vector2[5];
                        int num838 = 0;
                        float num839 = 2000f;
                        for (int num840 = 0; num840 < 255; num840++)
                        {
                            if (Main.player[num840].active && !Main.player[num840].dead)
                            {
                                Vector2 center9 = Main.player[num840].Center;
                                float num841 = Vector2.Distance(center9, NPC.Center);
                                if (num841 < num839 && Collision.CanHit(NPC.Center, 1, 1, center9, 1, 1))
                                {
                                    array4[num838] = num840;
                                    array5[num838] = center9;
                                    if (++num838 >= array5.Length)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if (Main.rand.Next(10) == 10)
                        {
                            for (int num842 = 0; num842 < num838; num842++)
                            {
                                Vector2 vector82 = array5[num842] - NPC.Center;
                                float ai = Main.rand.Next(100);
                                Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 14f;
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector83.X, vector83.Y, ModContent.ProjectileType<ZeroShock>(), damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                            }
                        }
                    }
                    if (NPC.ai[2] >= 180 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 3;
                    }
                }
                else
                {
                    if (NPC.ai[2] == 5)
                    {
                        int TeleportPos = Main.rand.Next(5);
                        int VoidHeight = 140;
                        Point spawnTilePos = new Point((Main.maxTilesX / 15 * 14) + (Main.maxTilesX / 15 / 2) - 100, VoidHeight);
                        Vector2 Origin = new Vector2(spawnTilePos.X * 16, spawnTilePos.Y * 16);

                        switch (TeleportPos)
                        {
                            case 0:
                                NPC.position = Origin;
                                break;
                            case 1:
                                NPC.position = Origin + new Vector2(0, 640);
                                break;
                            case 2:
                                NPC.position = Origin + new Vector2(0, -640);
                                break;
                            case 3:
                                NPC.position = Origin + new Vector2(640, 0);
                                break;
                            case 4:
                                NPC.position = Origin + new Vector2(-640, 0);
                                break;
                        }
                    }
                    if (NPC.life > NPC.lifeMax * (2 / 3))
                    {
                        if (NPC.ai[2] == 80 || NPC.ai[2] == 240) // + lasers
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                        }
                        if (NPC.ai[2] == 160 || NPC.ai[2] == 320) // x lasers
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                        }
                    }
                    else if (NPC.life > NPC.lifeMax / 3)
                    {
                        if (NPC.ai[2] == 80) // + lasers
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                        }
                        else if (NPC.ai[2] == 160)
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                        }
                        else if (NPC.ai[2] == 240)
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                        }
                        else if (NPC.ai[2] == 320)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), Mod.Find<ModProjectile>("ZeroBlast").Type, damage, 3);
                        }
                    }
                    else
                    {
                        if (NPC.ai[2] == 80) // + lasers
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                        }
                        else if (NPC.ai[2] == 160)
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                        }
                        else if (NPC.ai[2] == 240)
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), Mod.Find<ModProjectile>("ZeroLaser").Type, damage, 3);
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (NPC.ai[2] >= 320)
                            {
                                NPC.ai[3] = Main.rand.Next(3);
                                NPC.ai[2] = 0;
                                NPC.netUpdate = true;
                            }
                        }
                    }

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (NPC.ai[2] >= 400)
                        {
                            NPC.ai[3] = Main.rand.Next(3);
                            NPC.ai[2] = 0;
                            NPC.netUpdate = true;
                        }
                    }
                }

                if (ShieldScale > 0)
                {
                    ShieldScale -= .07f;
                }
                if (ShieldScale < 0)
                {
                    ShieldScale = 0;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[1] == 0)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.ai[1] == 1)
            {
                if (NPC.ai[3] == 3)
                {
                    frameCounters = 0;
                    NPC.ai[1]++;
                }
                else
                {
                    frameCounters++;
                    if (frameCounters > 4)
                    {
                        normalFrame++;
                        frameCounters = 0;
                    }
                    if (normalFrame >= 5)
                    {
                        normalFrame = 0;
                    }
                }
            }
            else if (NPC.ai[1] == 2)
            {
                frameCounters++;
                if (frameCounters > 4)
                {
                    switchOneFrame++;
                    frameCounters = 0;
                }
                if (switchOneFrame >= 5)
                {
                    switchOneFrame = 0;
                    NPC.ai[1]++;
                }
            }
            else if (NPC.ai[1] == 3)
            {
                if (NPC.ai[3] == 3)
                {
                    frameCounters++;
                    if (frameCounters > 4)
                    {
                        openFrame++;
                        frameCounters = 0;
                    }
                    if (openFrame >= 5)
                    {
                        openFrame = 0;
                    }
                }
                else
                {
                    frameCounters++;
                    if (frameCounters > 4)
                    {
                        switchTwoFrame++;
                        frameCounters = 0;
                    }
                    if (switchTwoFrame >= 5)
                    {
                        switchTwoFrame = 0;
                        NPC.ai[1] = 1;
                    }
                }
            }
        }

        public string ArmChoice()
        {
            string Choice = null;
            while (Choice == null)
            {
                int Arms = Main.rand.Next(8);
                switch (Arms)
                {
                    case 0:
                        Choice = "GenocideCannon";
                        break;
                    case 1:
                        Choice = "Neutralizer";
                        break;
                    case 2:
                        Choice = "NovaFocus";
                        break;
                    case 3:
                        Choice = "OmegaVolley";
                        break;
                    case 4:
                        Choice = "RealityCannon";
                        break;
                    case 5:
                        Choice = "RiftShredder";
                        break;
                    case 6:
                        Choice = "Taser";
                        break;
                    case 7:
                        Choice = "VoidStar";
                        break;
                }

                if (NPC.AnyNPCs(Mod.Find<ModNPC>(Choice).Type))
                {
                    Choice = null;
                }
            }
            return Choice;
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 16f;
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

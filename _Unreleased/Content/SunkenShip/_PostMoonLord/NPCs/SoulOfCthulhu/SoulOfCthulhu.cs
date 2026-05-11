using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityBrain;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEater;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEye;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityLeviathan;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityRose;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeitySkull;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu
{
    [AutoloadBossHead]
    public class SoulOfCthulhu : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul of Cthulhu");
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 100;
            NPC.width = 54;
            NPC.height = 54;
            NPC.aiStyle = -1;
            NPC.damage = 100;
            NPC.defense = 150;
            NPC.lifeMax = 1000000;
            NPC.value = Item.buyPrice(35, 0, 0, 0);
            NPC.DeathSound = SoundID.Item88;// new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["SoC"];
            NPC.noGravity = true;
            NPC.netAlways = true;
            for (int m = 0; m < NPC.buffImmune.Length; m++) NPC.buffImmune[m] = true;
        }

        public bool LeaveLine = false;
        public bool Leviathan = false;
        public bool Summon = false;

        public float Rotation = 0;
        public float AlphaTimer = 0;
        public float alpha = 255;
        public float scale = 0;
        public float RingRotation = 0;
        public float morphTimer = 0;
        public float RiftSpin = 0;
        public bool Morphed = false;
        public static bool ComeBack = false;
        public int ReturnTimer = 100;

        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == NetmodeID.Server || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                customAI[0] = reader.ReadSingle();
                customAI[1] = reader.ReadSingle();
                customAI[2] = reader.ReadSingle();
                customAI[3] = reader.ReadSingle();
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void OnKill()
        {
            if (Main.expertMode)
            {
                NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<CthulhuPortal>(), 0, 0);
            }
            else
            {
                NPC.DropLoot(Mod.Find<ModItem>("RealityBar").Type, 25, 35);
                string[] lootTable =
                {
                    "RealityAnchor",
                    "SquidStorm",
                    "CthulhuCannon",
                    "GalacticStormspike",
                };
                AAWorld_Unreleased.downedSoC = true;
                int loot = Main.rand.Next(lootTable.Length);
                NPC.DropLoot(Mod.Find<ModItem>(lootTable[loot]).Type);
            }
        }

        int oneTime = 0;

        public int EnemyTimer = 0;

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            AAPlayer modPlayer = Main.player[NPC.target].GetModPlayer<AAPlayer>();
            modPlayer.Leave = false;
            NPC.rotation = NPC.velocity.X / 15f;
            Vector2 spawnAt = NPC.Center + new Vector2(0f, NPC.height / 2f);
            float EyeSummon = NPC.lifeMax * .85f;
            float EaterSummon = NPC.lifeMax * .70f;
            float BrainSummon = NPC.lifeMax * .55f;
            float SkullSummon = NPC.lifeMax * .40f;
            float RoseSummon = NPC.lifeMax * .25f;
            float LeviathanSummon = NPC.lifeMax * .10f;
            bool BossAlive = NPC.AnyNPCs(ModContent.NPCType<DeityEye>()) || NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) || NPC.AnyNPCs(ModContent.NPCType<DeityBrain>()) || NPC.AnyNPCs(ModContent.NPCType<DeitySkull>()) || NPC.AnyNPCs(ModContent.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(ModContent.NPCType<DeityRose>());
            NPC.ai[3]++;
            customAI[3]++;
            if (NPC.ai[3] >= 600 && !BossAlive)
            {
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<Portal>(), 0, -NPC.velocity.X, -NPC.velocity.Y);
                NPC.ai[3] = 0;
            }


            if (oneTime == 0)
            {
                RainStart();
                oneTime++;
            }

            if (BossAlive)
            {
                NPC.alpha += 12;
                if (NPC.alpha >= 140)
                {
                    NPC.alpha = 140;
                }
                NPC.dontTakeDamage = true;
                NPC.Center = new Vector2(player.Center.X, player.Center.Y - 60);
                return;
            }
            else
            {
                NPC.alpha -= 30;
                if (NPC.alpha <= 0)
                {
                    NPC.alpha = 0;
                }
                NPC.dontTakeDamage = false;
            }

            if (NPC.life < EyeSummon && customAI[2] == 0) //Spawn Eye boi
            {
                customAI[2] = 1;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }
            else if (NPC.life < EaterSummon && customAI[2] == 1)
            {
                customAI[2] = 2;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }
            else if (NPC.life < BrainSummon && customAI[2] == 2)
            {
                customAI[2] = 3;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }
            else if (NPC.life < SkullSummon && customAI[2] == 3)
            {
                customAI[2] = 4;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }
            else if (NPC.life < RoseSummon && customAI[2] == 4)
            {
                customAI[2] = 5;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }
            else if (NPC.life < LeviathanSummon && customAI[2] == 5)
            {
                customAI[2] = 6;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }

            if (Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].dead)
                {
                    NPC.ai[1] = 3f;
                }
            }
            if (Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].dead)
                {
                    NPC.ai[1] = 4f;
                }
            }
            if (NPC.ai[1] == 0f)
            {
                NPC.damage = 100;

                Rotation += NPC.velocity.X * .01f;
                RiftSpin -= NPC.velocity.X * .01f;


                NPC.rotation = NPC.velocity.X / 15f;
                if (NPC.position.Y > Main.player[NPC.target].position.Y - 200f)
                {
                    if (NPC.velocity.Y > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y * 0.98f;
                    }
                    NPC.velocity.Y = NPC.velocity.Y - 0.1f;
                    if (NPC.velocity.Y > 2f)
                    {
                        NPC.velocity.Y = 2f;
                    }
                }
                else if (NPC.position.Y < Main.player[NPC.target].position.Y - 300f)
                {
                    if (NPC.velocity.Y < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y * 0.98f;
                    }
                    NPC.velocity.Y = NPC.velocity.Y + 0.1f;
                    if (NPC.velocity.Y < -2f)
                    {
                        NPC.velocity.Y = -2f;
                    }
                }
                if (NPC.position.X + (float)(NPC.width / 2) > Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + 100f)
                {
                    if (NPC.velocity.X > 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.98f;
                    }
                    NPC.velocity.X = NPC.velocity.X - 0.1f;
                    if (NPC.velocity.X > 8f)
                    {
                        NPC.velocity.X = 8f;
                    }
                }
                if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - 100f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.98f;
                    }
                    NPC.velocity.X = NPC.velocity.X + 0.1f;
                    if (NPC.velocity.X < -8f)
                    {
                        NPC.velocity.X = -8f;
                        return;
                    }
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= 600f)
                {
                    NPC.ai[2] = 0f;
                    NPC.ai[1] = 1f;
                    NPC.TargetClosest(true);
                    NPC.netUpdate = true;
                }
            }
            else
            {
                if (NPC.ai[1] == 1f)
                {
                    NPC.defense = 180;
                    NPC.damage = 200;
                    NPC.ai[2] += 1f;
                    if (NPC.ai[2] == 2f)
                    {
                        SoundEngine.PlaySound(SoundID.Zombie92, NPC.Center);
                    }
                    if (NPC.ai[2] >= 400f)
                    {
                        NPC.ai[2] = 0f;
                        NPC.ai[1] = 0f;
                    }
                    NPC.rotation += NPC.direction * 0.7f;
                    Vector2 vector44 = new Vector2(NPC.position.X + ((float)NPC.width * 0.5f), NPC.position.Y + ((float)NPC.height * 0.5f));
                    float num441 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector44.X;
                    float num442 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector44.Y;
                    float num443 = (float)Math.Sqrt((double)((num441 * num441) + (num442 * num442)));
                    float num4 = 5f + num443 / 100f;
                    if (num4 < 8.0)
                        num4 = 8f;
                    if (num4 > 32.0)
                        num4 = 32f;
                    float num5 = num4 / num443;
                    NPC.velocity.X = num441 * num5;
                    NPC.velocity.Y = num442 * num5;
                    Rotation += NPC.velocity.X * .08f;
                    RiftSpin -= NPC.velocity.X * .08f;
                    return;

                }
                if (NPC.ai[1] == 2f)
                {
                    Summon = true;
                    NPC.velocity *= .8f;
                    if (NPC.velocity.X < .5f || NPC.velocity.X > -.5f)
                    {
                        NPC.velocity.X = 0;
                    }
                    if (NPC.velocity.Y < .5f || NPC.velocity.Y > -.5f)
                    {
                        NPC.velocity.Y = 0;
                    }

                    if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
                    {

                        Rotation += .2f;
                        RiftSpin -= .2f;
                        customAI[1]++;

                        if (customAI[1] > 300)
                        {
                            if (customAI[0] == 0)
                            {
                                Summon = true;
                                customAI[0] = 1;
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityEye>());
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 1)
                            {
                                Summon = true;
                                customAI[0] = 2;
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityEater>());
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 2)
                            {
                                Summon = true;
                                customAI[0] = 3;
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityBrain>());
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 3)
                            {
                                Summon = true;
                                customAI[0] = 4;
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeitySkull>(), 0, 0, 1);
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 4)
                            {
                                Summon = true;
                                customAI[0] = 5;
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityRose>());
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 5)
                            {
                                Summon = true;
                                customAI[0] = 6;
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityLeviathan>());
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                        }
                    }
                    return;
                }
                if (NPC.ai[1] == 3f)
                {
                    Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Killed"), Color.DarkCyan);
                    NPC.ai[1] = 5f;
                }
                if (NPC.ai[1] == 4f)
                {
                    Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Despawn"), Color.DarkCyan);
                    NPC.ai[1] = 5f;
                }
                if (NPC.ai[1] == 5f)
                {
                    NPC.alpha += 5;
                    {
                        if (NPC.alpha >= 255)
                        {
                            NPC.active = false;
                        }
                    }
                }
            }
        }

        /*
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (AAWorld.Anticheat)
            {
                if (damage > NPC.lifeMax / 8)
                {
        
                    Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Anticheat"), Color.DarkCyan);
                    damage = 0;
                }
            }
        }
        */

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Vector2 baseVelocity = NPC.velocity * Main.rand.NextFloat();
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, baseVelocity + Vector2.UnitX * 2f, Mod.Find<ModGore>("SoCGore1").Type, 1.4f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, baseVelocity + Vector2.UnitX * -2f, Mod.Find<ModGore>("SoCGore1").Type, 1.4f);
                for (int i = 0; i < 8; i++)
                {
                    int num = 3 + i;
                    Vector2 extraVelo = Vector2.UnitY.RotatedBy(MathHelper.TwoPi / 8f * i) * -2f;
                    Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, baseVelocity + extraVelo, Mod.Find<ModGore>("SoCGore" + num).Type, 1.4f);
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture2D13 = TextureAssets.Npc[NPC.type].Value;
            Texture2D WheelTex = ModContent.Request<Texture2D>(Texture + "_Wheel").Value;
            Texture2D RingTex = ModContent.Request<Texture2D>(Texture + "_DeityCircle").Value;
            Texture2D RitualTex = ModContent.Request<Texture2D>(Texture + "_DeityRitual").Value;
            Texture2D Rift = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/NPCs/SoulOfCthulhu/UDUNFUKED_Rift").Value;
            Texture2D GlowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Vector2 vector38 = NPC.position + new Vector2(NPC.width, NPC.height) / 2f + Vector2.UnitY * NPC.gfxOffY - Main.screenPosition;
            Vector2 origin8 = new Vector2((float)RitualTex.Width, (float)RitualTex.Height) / 2f;
            int num214 = TextureAssets.Npc[NPC.type].Value.Height;
            Color color25 = Lighting.GetColor((int)(NPC.position.X + NPC.width * 0.5) / 16, (int)((NPC.position.Y + NPC.height * 0.5) / 16.0));
            Color? alpha4 = GetAlpha(color25);
            Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
            bool BossAlive = NPC.AnyNPCs(ModContent.NPCType<DeityEye>()) || NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) || NPC.AnyNPCs(ModContent.NPCType<DeityBrain>()) || NPC.AnyNPCs(ModContent.NPCType<DeitySkull>()) || NPC.AnyNPCs(ModContent.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(ModContent.NPCType<DeityRose>());
            if (Summon)
            {
                Rotation += .2f;
                RiftSpin -= .2f;
                if (customAI[3] < 300f)
                {
                    alpha -= 5;
                }
                else
                {
                    alpha += 12;
                }
                if (alpha < 0)
                {
                    alpha = 0;
                }
                if (alpha > 255)
                {
                    alpha = 255;
                }
                scale = 1f - alpha / 255f;
                RingRotation += 0.0149599658f;
                Main.spriteBatch.Draw(RingTex, vector38, null, AAColor.Cthulhu, -RingRotation, RingTex.Size() / 2f, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(RitualTex, vector38, null, AAColor.Cthulhu, RingRotation, origin8, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(RingTex, vector38, null, AAColor.Cthulhu, -RingRotation, RingTex.Size() / 2f, scale * 0.42f, SpriteEffects.None, 0f);
            }

            int shader = 0;

            if (BossAlive)
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            else
                shader = 0;

            BaseDrawing.DrawTexture(spriteBatch, Rift, 0, NPC.position, NPC.width, NPC.height, 1.5f, RiftSpin, 0, 1, new Rectangle(0, 0, Rift.Width, Rift.Height), AAColor.Cthulhu, true);

            BaseDrawing.DrawTexture(spriteBatch, WheelTex, shader, NPC.position, NPC.width, NPC.height, NPC.scale, Rotation, 0, 1, new Rectangle(0, 0, WheelTex.Width, WheelTex.Height), drawColor, true);

            BaseDrawing.DrawTexture(spriteBatch, texture2D13, shader, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, 0, 1, new Rectangle(0, 0, texture2D13.Width, texture2D13.Height), drawColor, true);

            if (BossAlive || Summon)
            {
                BaseDrawing.DrawTexture(spriteBatch, GlowTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, 0, 1, new Rectangle(0, 0, GlowTex.Width, GlowTex.Height), Color.White, true);

                BaseDrawing.DrawAfterimage(spriteBatch, GlowTex, 0, NPC, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);
            }

            return false;
        }

        private static void RainStart()
        {
            if (!Main.raining)
            {
                int num = 86400;
                int num2 = num / 24;
                Main.rainTime = Main.rand.Next(num2 * 8, num);
                if (Main.rand.Next(3) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 3);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 4);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 5);
                }
                float num3 = 1f;
                if (Main.rand.Next(2) == 0)
                {
                    num3 += 0.05f;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num3 += 0.1f;
                }
                if (Main.rand.Next(4) == 0)
                {
                    num3 += 0.15f;
                }
                if (Main.rand.Next(5) == 0)
                {
                    num3 += 0.2f;
                }
                Main.rainTime = (int)((float)Main.rainTime * num3);
                Main.raining = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                }
            }
        }
    }
}
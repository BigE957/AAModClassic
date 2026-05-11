using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEye;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEater;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeitySkull;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityLeviathan;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityRose;
using AAModClassic.Music;

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
            NPC.DeathSound = SoundID.Item88;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["SoC"];
            NPC.noGravity = true;
            NPC.netAlways = true;
            for (int m = 0; m < NPC.buffImmune.Length; m++) NPC.buffImmune[m] = true;
        }

        public bool LeaveLine = false;
        public bool Pinch = false;
        public bool Eye = false;
        public bool Eater = false;
        public bool Skull = false;
        public bool Rose = false;
        public bool Leviathan = false;
        public bool Summon = false;
        public bool Boss1 = false;
        public bool Boss2 = false;
        public bool Boss3 = false;
        public bool Boss4 = false;

        public float Rotation = 0;
        public float AlphaTimer = 0;
        public float alpha = 255;
        public float scale = 0;
        public float RingRotation = 0;
        public float morphTimer = 0;
        public bool Morph = false;
        public float RiftSpin = 0;
        public bool Morphed = false;
        public static bool ComeBack = false;
        public int ReturnTimer = 100;


        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
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
            float EyeSummon = NPC.lifeMax * .8f;
            float EaterSummon = NPC.lifeMax * .6f;
            float SkullSummon = NPC.lifeMax * .4f;
            float LeviathanSummon = NPC.lifeMax * .2f;
            bool BossAlive = NPC.AnyNPCs(ModContent.NPCType<DeityEye>()) || NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) || NPC.AnyNPCs(ModContent.NPCType<DeitySkull>()) || NPC.AnyNPCs(ModContent.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(ModContent.NPCType<DeityRose>());
            EnemyTimer++;

            if (EnemyTimer >= 600)
            {
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<Portal>(), 0, -NPC.velocity.X, -NPC.velocity.Y);
                EnemyTimer = 0;
            }


            if (oneTime == 0)
            {
                RainStart();
                oneTime++;
            }

            if (BossAlive)
            {
                Morphed = true;
                return;
            }
            else
            {
                Morphed = false;
            }

            if (Morphed)
            {
                NPC.alpha += 12;
                if (NPC.alpha >= 140)
                {
                    NPC.alpha = 140;
                }
                NPC.dontTakeDamage = true;

                NPC.netUpdate = true;
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

                NPC.netUpdate = true;
            }

            if (NPC.ai[1] == 1f || NPC.ai[1] == 0f)
            {
                NPC.dontTakeDamage = false;
            }
            else
            {
                NPC.dontTakeDamage = true;
            }

            if (NPC.life < EyeSummon && !Boss1) //Spawn Eye boi
            {
                Boss1 = true;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                morphTimer = 0;
            }
            else if (NPC.life < EaterSummon && !Boss2)
            {
                Boss2 = true;
                NPC.ai[1] = 3f;
                NPC.dontTakeDamage = true;
                morphTimer = 0;
            }
            else if (NPC.life < SkullSummon && !Boss3)
            {
                Boss3 = true;
                NPC.ai[1] = 4f;
                NPC.dontTakeDamage = true;
                morphTimer = 0;
            }
            else if (NPC.life < LeviathanSummon && !Boss4)
            {
                Boss4 = true;
                NPC.ai[1] = 7f;
                NPC.dontTakeDamage = true;
                morphTimer = 0;
            }

            if (NPC.life <= NPC.lifeMax / 10)
            {
                Music = MusicManagementSystem.MusicSlots["Superancients_Pinch"];
                if (!Pinch)
                {
                    Pinch = true;
                    Main.NewText("YOU", Color.DarkCyan);
                    Main.NewText("WILL", Color.DarkCyan);
                    Main.NewText("PERISH", Color.DarkCyan);
                }
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
                if (NPC.position.X + NPC.width / 2 > Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 + 100f)
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
                if (NPC.position.X + NPC.width / 2 < Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - 100f)
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
                    Vector2 vector44 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                    float num441 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector44.X;
                    float num442 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector44.Y;
                    float num443 = (float)Math.Sqrt((double)(num441 * num441 + num442 * num442));
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
                        morphTimer++;

                        if (morphTimer > 300)
                        {

                            if (Eye == false)
                            {
                                Eye = true;
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityEye>());
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                            }
                            
                        }
                    }
                    return;
                }
                if (NPC.ai[1] == 3f)
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
                        morphTimer++;
                        if (morphTimer > 300)
                        {
                            if (Eater == false)
                            {
                                Eater = true;
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityEater>());
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                            }
                        }

                    }

                    return;
                }
                if (NPC.ai[1] == 4f)
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
                        morphTimer++;
                        if (morphTimer > 300)
                        {
                            if (Skull == false)
                            {
                                Skull = true;
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeitySkull>(), 0, 0, 1);
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                            }
                        }

                    }


                    return;
                }

                if (NPC.ai[1] == 6f)
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
                        morphTimer++;
                        if (morphTimer > 300)
                        {
                            if (Rose == false)
                            {
                                Rose = true;
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityRose>());
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                            }
                        }
                    }


                    return;
                }

                if (NPC.ai[1] == 7f)
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
                        morphTimer++;
                        if (morphTimer > 300)
                        {
                            if (Leviathan == false)
                            {
                                Leviathan = true;
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityLeviathan>());
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                            }
                        }
                    }


                    return;
                }
                if (NPC.ai[1] == 8f)
                {
                    Main.NewText("...good riddance...", Color.DarkCyan);
                    NPC.ai[1] = 9f;
                }
                if (NPC.ai[1] == 9f)
                {
                    Main.NewText("...do not return...", Color.DarkCyan);
                    NPC.ai[1] = 9F;
                }
                if (NPC.ai[1] == 10f)
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

        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (!AAConfigClient.Instance.DisableAnticheat)
            {
                if (modifiers.GetDamage(item.damage, true) > NPC.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    modifiers.TargetDamageMultiplier *= 0;
                }
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (!AAConfigClient.Instance.DisableAnticheat)
            {
                if (modifiers.GetDamage(projectile.damage, true) > NPC.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    modifiers.TargetDamageMultiplier *= 0;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if(NPC.life <= 0)
            {
                Vector2 baseVelocity = NPC.velocity * Main.rand.NextFloat();
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, baseVelocity + Vector2.UnitX * 2f, Mod.Find<ModGore>("SoCGore1").Type, 1.4f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, baseVelocity + Vector2.UnitX * -2f, Mod.Find<ModGore>("SoCGore1").Type, 1.4f);
                for(int i = 0; i < 8; i++)
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
            Vector2 vector38 = NPC.position + new Vector2(NPC.width, NPC.height) / 2f + Vector2.UnitY * NPC.gfxOffY - Main.screenPosition;
            Vector2 origin8 = new Vector2(RitualTex.Width, RitualTex.Height) / 2f;
            int y6 = 0;
            Color color25 = Lighting.GetColor((int)(NPC.position.X + NPC.width * 0.5) / 16, (int)((NPC.position.Y + NPC.height * 0.5) / 16.0));
            Color? alpha4 = GetAlpha(color25);
            Color color;
            Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
            if (Summon)
            {
                Rotation += .2f;
                RiftSpin -= .2f;
                if (morphTimer < 300f)
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
                Main.spriteBatch.Draw(RitualTex, vector38, null, AAColor.Cthulhu, RingRotation, origin8, scale * 0.42f, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(RingTex, vector38, null, AAColor.Cthulhu, -RingRotation, RingTex.Size() / 2f, scale * 0.42f, SpriteEffects.None, 0f);
            }
            if (NPC.alpha > 0)
            {
                color = AAColor.Cthulhu;
            }
            else
            {
                color = drawColor;
            }
            Main.spriteBatch.Draw(Rift, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, Rift.Width, Rift.Height)), AAColor.Cthulhu, RiftSpin, new Vector2(Rift.Width / 2f, Rift.Height / 2f), 1.5f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(WheelTex, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, WheelTex.Width, WheelTex.Height)), color, Rotation, new Vector2(texture2D13.Width / 2f, texture2D13.Height / 2f), NPC.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture2D13, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, texture2D13.Width, texture2D13.Height)), color, NPC.rotation, new Vector2(texture2D13.Width / 2f, texture2D13.Height / 2f), NPC.scale, SpriteEffects.None, 0f);
            return false;
        }

        private static void RainStart()
        {
            if (!Main.raining)
            {
                int num = 86400;
                int num2 = num / 24;
                Main.rainTime = Main.rand.Next(num2 * 8, num);
                if (Main.rand.NextBool(3))
                {
                    Main.rainTime += Main.rand.Next(0, num2);
                }
                if (Main.rand.NextBool(4))
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.NextBool(5))
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.NextBool(6))
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 3);
                }
                if (Main.rand.NextBool(7))
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 4);
                }
                if (Main.rand.NextBool(8))
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 5);
                }
                float num3 = 1f;
                if (Main.rand.NextBool(2))
                {
                    num3 += 0.05f;
                }
                if (Main.rand.NextBool(3))
                {
                    num3 += 0.1f;
                }
                if (Main.rand.NextBool(4))
                {
                    num3 += 0.15f;
                }
                if (Main.rand.NextBool(5))
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
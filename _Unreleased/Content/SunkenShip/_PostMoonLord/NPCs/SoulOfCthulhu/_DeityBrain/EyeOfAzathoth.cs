using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEye;
using AAModClassic._Unreleased.Content.SunkenShip.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityBrain
{
    public class EyeOfAzathoth : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 78;
            NPC.height = 120;
            NPC.value = 0;
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 5000;
            NPC.defense = 130;
            NPC.damage = 60;
            NPC.HitSound = SoundID.NPCHit31;
            NPC.DeathSound = SoundID.NPCDeath35;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = true;
            NPC.defense = 130; //keep defense at 130
            NPC.noGravity = true;
            SpawnModBiomes = [ModContent.GetInstance<SunkenShipBiome>().Type];
        }

        public int body = -1;
        public float rotValue = -1f;
        public bool spawnedDust = false;
        public bool fireAttack = false;

        public override void AI()
        {
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, ModContent.NPCType<DeityBrain>(), 500f, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1)
            {
                BaseAI.KillNPCWithLoot(NPC);
                return;
            }
            NPC brain = Main.npc[body];
            NPC.target = brain.target;
            Player targetPlayer = Main.player[NPC.target];

            if (brain == null || brain.life <= 0 || !brain.active || brain.type != ModContent.NPCType<DeityBrain>()) 
            {
                BaseAI.KillNPCWithLoot(NPC); 
                return;
            }

            if (Main.netMode != NetmodeID.Server && !spawnedDust)
            {
                spawnedDust = true;
                for (int m = 0; m < 20; m++)
                {
                    int dustID = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), 0, -1f, 0, default(Color), 1f);
                    Main.dust[dustID].noGravity = true;
                    Main.dust[dustID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                    Main.dust[dustID].velocity *= 2f;
                }
            }
            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            int EoACount = ((DeityBrain)brain.ModNPC).EyeCount;
            bool outer = NPC.ai[0] % 2 == 0;
            rotValue = (NPC.ai[0] * (MathHelper.TwoPi / EoACount)) + (NPC.ai[1] * (outer ? -0.025f : 0.04f));
            //rotValue += 0.05f;
            while (rotValue > MathHelper.TwoPi) 
                rotValue -= (float)Math.PI * 2f;
            int dist = outer ? 280 : 180;
            NPC.Center = BaseUtility.RotateVector(brain.Center, brain.Center + new Vector2(dist, 0f), rotValue);
            NPC.position.Y -= 48;

            NPC.spriteDirection = (NPC.position.X - NPC.oldPos[1].X) < 0 ? 1 : -1;
            NPC.rotation = 0;// (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;

            NPC.ai[1]++;
            int aiTimerFire = (NPC.ai[0] % 3 == 0 ? 50 : NPC.ai[0] % 2 == 0 ? 150 : 100); //aiTimerFire is different per head by using whoAmI (which is usually different) 

            if (targetPlayer != null && NPC.ai[1] % aiTimerFire == 0)
            {
                //fireAttack = true;
                for (int i = 0; i < 5; ++i)
                {
                    Vector2 dir = Vector2.Normalize(targetPlayer.Center - NPC.Center);
                    dir *= 5f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, dir.X * 3, dir.Y * 3, ModContent.ProjectileType<DeityEye_DeityFlames>(), (int)(NPC.damage * .8f), 0f, Main.myPlayer);
                }
            }

            if (Main.netMode != NetmodeID.Server && Main.LocalPlayer.miscTimer % 2 == 0)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), 0, -1f, 0, default(Color), 1f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color dColor)
        {
            Color lightColor = BaseDrawing.GetNPCColor(NPC, null);
            if (!NPC.IsABestiaryIconDummy && Main.player[NPC.target] != null && Main.player[NPC.target].active && !Main.player[NPC.target].dead)
            {
                BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 2f, 0.9f, 2, true, 0f, 0f, lightColor);
            }

            Texture2D texture8 = TextureAssets.Npc[NPC.type].Value;
            Texture2D PupilTex = ModContent.Request<Texture2D>(Texture + "_Pupil").Value;
            Texture2D Glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Vector2 origin15 = new(40f, 40f);
            Vector2 value33 = new(30f, 30f);
            spriteBatch.Draw(texture8, NPC.Center - screenPos, new Microsoft.Xna.Framework.Rectangle?(NPC.frame), dColor, NPC.rotation, origin15, 1f, SpriteEffects.None, 0f);
            Vector2 value34 = Utils.Vector2FromElipse(NPC.DirectionTo(NPC.IsABestiaryIconDummy ? Main.MouseScreen : Main.player[NPC.target].Center), value33);
            value34 *= MathHelper.Clamp((NPC.Distance(NPC.IsABestiaryIconDummy ? Main.MouseScreen : Main.player[NPC.target].Center)) / 100f, 0f, 1f);
            spriteBatch.Draw(Glow, NPC.Center - screenPos, new Microsoft.Xna.Framework.Rectangle?(NPC.frame), AAColor.Cthulhu2, NPC.rotation, origin15, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(PupilTex, NPC.Center - screenPos + value34, null, AAColor.Cthulhu2, NPC.rotation, PupilTex.Size() / 2f, 1f, SpriteEffects.None, 0f);
            return false;
        }

        /* Old Version, Kept cause the new version might suck shit
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eye of Azathoth");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.defense = 40;
            NPC.damage = 90;
            NPC.lifeMax = 3000;
            //NPC.aiStyle = NPCAIStyleID.TrueEyeOfCthulhu;
            NPC.aiStyle = -1;
            NPC.width = 60;
            NPC.height = 60;
            NPC.value = 0f;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AnimationType = NPCID.MoonLordFreeEye;
            NPC.npcSlots = 0f;
            NPC.noGravity = true;
            NPC.dontTakeDamage = false;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {

            if (Main.rand.NextBool(420))
            {
                //SoundEngine.PlaySound(29, (int)NPC.Center.X, (int)NPC.Center.Y, Main.rand.Next(100, 101), 1f, 0f);
                SoundEngine.PlaySound(SoundID.Zombie100, NPC.Center, null);
            }
            Vector2 value31 = new Vector2(30f);
            float num1194 = 0f;
            float num1195 = NPC.ai[0];
            NPC.ai[1] += 1f;
            int num1196 = 0;
            int num1197 = 0;
            while (num1196 < 10)
            {
                num1194 = NPC.MoonLordAttacksArray2[1, num1196];
                if (num1194 + num1197 > NPC.ai[1])
                {
                    break;
                }
                num1197 += (int)num1194;
                num1196++;
            }
            if (num1196 == 10)
            {
                num1196 = 0;
                NPC.ai[1] = 0f;
                num1194 = NPC.MoonLordAttacksArray2[1, num1196];
                num1197 = 0;
            }
            NPC.ai[0] = NPC.MoonLordAttacksArray2[0, num1196];
            float num1198 = (int)NPC.ai[1] - num1197;
            if (NPC.ai[0] != num1195)
            {
                NPC.netUpdate = true;
            }
            if (NPC.ai[0] == -1f)
            {
                NPC.ai[1] += 1f;
                if (NPC.ai[1] > 180f)
                {
                    NPC.ai[1] = 0f;
                }
                float value32;
                if (NPC.ai[1] < 60f)
                {
                    value32 = 0.75f;
                    NPC.localAI[0] = 0f;
                    NPC.localAI[1] = (float)Math.Sin((double)(NPC.ai[1] * 6.28318548f / 15f)) * 0.35f;
                    if (NPC.localAI[1] < 0f)
                    {
                        NPC.localAI[0] = 3.14159274f;
                    }
                }
                else if (NPC.ai[1] < 120f)
                {
                    value32 = 1f;
                    if (NPC.localAI[1] < 0.5f)
                    {
                        NPC.localAI[1] += 0.025f;
                    }
                    NPC.localAI[0] += 0.209439516f;
                }
                else
                {
                    value32 = 1.15f;
                    NPC.localAI[1] -= 0.05f;
                    if (NPC.localAI[1] < 0f)
                    {
                        NPC.localAI[1] = 0f;
                    }
                }
                NPC.localAI[2] = MathHelper.Lerp(NPC.localAI[2], value32, 0.3f);
            }
            if (NPC.ai[0] == 0f)
            {
                NPC.TargetClosest(false);
                Vector2 v8 = Main.player[NPC.target].Center + Main.player[NPC.target].velocity * 20f - NPC.Center;
                NPC.localAI[0] = NPC.localAI[0].AngleLerp(v8.ToRotation(), 0.5f);
                NPC.localAI[1] += 0.05f;
                if (NPC.localAI[1] > 0.7f)
                {
                    NPC.localAI[1] = 0.7f;
                }
                NPC.localAI[2] = MathHelper.Lerp(NPC.localAI[2], 1f, 0.2f);
                float scaleFactor9 = 24f;
                Vector2 center23 = NPC.Center;
                Vector2 center24 = Main.player[NPC.target].Center;
                Vector2 value33 = center24 - center23;
                Vector2 vector187 = value33 - Vector2.UnitY * 200f;
                vector187 = Vector2.Normalize(vector187) * scaleFactor9;
                int num1199 = 30;
                NPC.velocity.X = (NPC.velocity.X * (num1199 - 1) + vector187.X) / num1199;
                NPC.velocity.Y = (NPC.velocity.Y * (num1199 - 1) + vector187.Y) / num1199;
                float num1200 = 0.25f;
                for (int num1201 = 0; num1201 < 200; num1201++)
                {
                    if (num1201 != NPC.whoAmI && Main.npc[num1201].active && Main.npc[num1201].type == ModContent.NPCType<EyeOfAzathoth>() && Vector2.Distance(NPC.Center, Main.npc[num1201].Center) < 150f)
                    {
                        if (NPC.position.X < Main.npc[num1201].position.X)
                        {
                            NPC.velocity.X = NPC.velocity.X - num1200;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X + num1200;
                        }
                        if (NPC.position.Y < Main.npc[num1201].position.Y)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num1200;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num1200;
                        }
                    }
                }
                return;
            }
            if (NPC.ai[0] == 1f)
            {
                if (num1198 == 0f)
                {
                    NPC.TargetClosest(false);
                    NPC.netUpdate = true;
                }
                NPC.velocity *= 0.95f;
                if (NPC.velocity.Length() < 1f)
                {
                    NPC.velocity = Vector2.Zero;
                }
                Vector2 v9 = Main.player[NPC.target].Center + Main.player[NPC.target].velocity * 20f - NPC.Center;
                NPC.localAI[0] = NPC.localAI[0].AngleLerp(v9.ToRotation(), 0.5f);
                NPC.localAI[1] += 0.05f;
                if (NPC.localAI[1] > 1f)
                {
                    NPC.localAI[1] = 1f;
                }
                if (num1198 < 20f)
                {
                    NPC.localAI[2] = MathHelper.Lerp(NPC.localAI[2], 1.1f, 0.2f);
                }
                else
                {
                    NPC.localAI[2] = MathHelper.Lerp(NPC.localAI[2], 0.4f, 0.2f);
                }
                if (num1198 == num1194 - 35f)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath6, NPC.position);
                }
                if ((num1198 == num1194 - 14f || num1198 == num1194 - 7f || num1198 == num1194) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 vector188 = Utils.Vector2FromElipse(NPC.localAI[0].ToRotationVector2(), value31 * NPC.localAI[1]);
                    Vector2 vector189 = Vector2.Normalize(v9) * 8f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X + vector188.X, NPC.Center.Y + vector188.Y, vector189.X, vector189.Y, ProjectileID.PhantasmalBolt, 35, 0f, Main.myPlayer, 0f, 0f);
                    return;
                }
            }
            else if (NPC.ai[0] == 2f)
            {
                if (num1198 < 15f)
                {
                    NPC.localAI[1] -= 0.07f;
                    if (NPC.localAI[1] < 0f)
                    {
                        NPC.localAI[1] = 0f;
                    }
                    NPC.localAI[2] = MathHelper.Lerp(NPC.localAI[2], 0.4f, 0.2f);
                    NPC.velocity *= 0.8f;
                    if (NPC.velocity.Length() < 1f)
                    {
                        NPC.velocity = Vector2.Zero;
                        return;
                    }
                }
                else if (num1198 < 75f)
                {
                    float num1202 = (num1198 - 15f) / 10f;
                    int num1203 = 0;
                    int num1204 = 0;
                    switch ((int)num1202)
                    {
                        case 0:
                            num1203 = 0;
                            num1204 = 2;
                            break;
                        case 1:
                            num1203 = 2;
                            num1204 = 5;
                            break;
                        case 2:
                            num1203 = 5;
                            num1204 = 3;
                            break;
                        case 3:
                            num1203 = 3;
                            num1204 = 1;
                            break;
                        case 4:
                            num1203 = 1;
                            num1204 = 4;
                            break;
                        case 5:
                            num1203 = 4;
                            num1204 = 0;
                            break;
                    }
                    Vector2 spinningpoint8 = Vector2.UnitY * -30f;
                    Vector2 value34 = spinningpoint8.RotatedBy((double)(num1203 * 6.28318548f / 6f), default);
                    Vector2 value35 = spinningpoint8.RotatedBy((double)(num1204 * 6.28318548f / 6f), default);
                    Vector2 vector190 = Vector2.Lerp(value34, value35, num1202 - (int)num1202);
                    float value36 = vector190.Length() / 30f;
                    NPC.localAI[0] = vector190.ToRotation();
                    NPC.localAI[1] = MathHelper.Lerp(NPC.localAI[1], value36, 0.5f);
                    for (int num1205 = 0; num1205 < 2; num1205++)
                    {
                        int num1206 = Dust.NewDust(NPC.Center + vector190 - Vector2.One * 4f, 0, 0, DustID.Vortex, 0f, 0f, 0, default, 1f);
                        Main.dust[num1206].velocity += vector190 / 15f;
                        Main.dust[num1206].noGravity = true;
                    }
                    if ((num1198 - 15f) % 10f == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 vec4 = Vector2.Normalize(vector190);
                        if (vec4.HasNaNs())
                        {
                            vec4 = Vector2.UnitY * -1f;
                        }
                        vec4 *= 4f;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X + vector190.X, NPC.Center.Y + vector190.Y, vec4.X, vec4.Y, ProjectileID.PhantasmalSphere, 55, 0f, Main.myPlayer, 30f, NPC.whoAmI);
                        return;
                    }
                }
                else
                {
                    if (num1198 < 105f)
                    {
                        NPC.localAI[0] = NPC.localAI[0].AngleLerp(NPC.ai[2] - 1.57079637f, 0.2f);
                        NPC.localAI[2] = MathHelper.Lerp(NPC.localAI[2], 0.75f, 0.2f);
                        if (num1198 == 75f)
                        {
                            NPC.TargetClosest(false);
                            NPC.netUpdate = true;
                            NPC.velocity = Vector2.UnitY * -7f;
                            for (int num1207 = 0; num1207 < 1000; num1207++)
                            {
                                Projectile projectile7 = Main.projectile[num1207];
                                if (projectile7.active && projectile7.type == ProjectileID.PhantasmalSphere && projectile7.ai[1] == NPC.whoAmI && projectile7.ai[0] != -1f)
                                {
                                    projectile7.velocity += NPC.velocity;
                                    projectile7.netUpdate = true;
                                }
                            }
                        }
                        NPC.velocity.Y = NPC.velocity.Y * 0.96f;
                        NPC.ai[2] = (Main.player[NPC.target].Center - NPC.Center).ToRotation() + 1.57079637f;
                        NPC.rotation = NPC.rotation.AngleTowards(NPC.ai[2], 0.104719758f);
                        return;
                    }
                    if (num1198 < 120f)
                    {
                        SoundEngine.PlaySound(SoundID.Zombie102, NPC.Center);
                        if (num1198 == 105f)
                        {
                            NPC.netUpdate = true;
                        }
                        Vector2 velocity8 = (NPC.ai[2] - 1.57079637f).ToRotationVector2() * 12f;
                        NPC.velocity = velocity8 * 2f;
                        for (int num1208 = 0; num1208 < 1000; num1208++)
                        {
                            Projectile projectile8 = Main.projectile[num1208];
                            if (projectile8.active && projectile8.type == ProjectileID.PhantasmalSphere && projectile8.ai[1] == NPC.whoAmI && projectile8.ai[0] != -1f)
                            {
                                projectile8.ai[0] = -1f;
                                projectile8.velocity = velocity8;
                                projectile8.netUpdate = true;
                            }
                        }
                        return;
                    }
                    NPC.velocity *= 0.92f;
                    NPC.rotation = NPC.rotation.AngleLerp(0f, 0.2f);
                    return;
                }
            }
            else if (NPC.ai[0] == 3f)
            {
                if (num1198 < 15f)
                {
                    NPC.localAI[1] -= 0.07f;
                    if (NPC.localAI[1] < 0f)
                    {
                        NPC.localAI[1] = 0f;
                    }
                    NPC.localAI[2] = MathHelper.Lerp(NPC.localAI[2], 0.4f, 0.2f);
                    NPC.velocity *= 0.9f;
                    if (NPC.velocity.Length() < 1f)
                    {
                        NPC.velocity = Vector2.Zero;
                        return;
                    }
                }
                else if (num1198 < 45f)
                {
                    NPC.localAI[0] = 0f;
                    NPC.localAI[1] = (float)Math.Sin((double)((num1198 - 15f) * 6.28318548f / 15f)) * 0.5f;
                    if (NPC.localAI[1] < 0f)
                    {
                        NPC.localAI[0] = 3.14159274f;
                        return;
                    }
                }
                else
                {
                    if (num1198 >= 185f)
                    {
                        NPC.velocity *= 0.88f;
                        NPC.rotation = NPC.rotation.AngleLerp(0f, 0.2f);
                        NPC.localAI[1] -= 0.07f;
                        if (NPC.localAI[1] < 0f)
                        {
                            NPC.localAI[1] = 0f;
                        }
                        NPC.localAI[2] = MathHelper.Lerp(NPC.localAI[2], 1f, 0.2f);
                        return;
                    }
                    if (num1198 == 45f)
                    {
                        NPC.ai[2] = (Main.rand.NextBool(2)).ToDirectionInt() * 6.28318548f / 40f;
                        NPC.netUpdate = true;
                    }
                    if ((num1198 - 15f - 30f) % 40f == 0f)
                    {
                        NPC.ai[2] *= 0.95f;
                    }
                    NPC.localAI[0] += NPC.ai[2];
                    NPC.localAI[1] += 0.05f;
                    if (NPC.localAI[1] > 1f)
                    {
                        NPC.localAI[1] = 1f;
                    }
                    Vector2 vector191 = NPC.localAI[0].ToRotationVector2() * value31 * NPC.localAI[1];
                    float scaleFactor10 = MathHelper.Lerp(8f, 20f, (num1198 - 15f - 30f) / 140f);
                    NPC.velocity = Vector2.Normalize(vector191) * scaleFactor10;
                    NPC.rotation = NPC.rotation.AngleLerp(NPC.velocity.ToRotation() + 1.57079637f, 0.2f);
                    if ((num1198 - 15f - 30f) % 10f == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 vector192 = NPC.Center + Vector2.Normalize(vector191) * value31.Length() * 0.4f;
                        Vector2 vector193 = Vector2.Normalize(vector191) * 8f;
                        float ai3 = (6.28318548f * (float)Main.rand.NextDouble() - 3.14159274f) / 30f + 0.0174532924f * NPC.ai[2];
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), vector192.X, vector192.Y, vector193.X, vector193.Y, ProjectileID.PhantasmalEye, 35, 0f, Main.myPlayer, 0f, ai3);
                        return;
                    }
                }
            }
            else if (NPC.ai[0] == 4f)
            {
                if (num1198 == 0f)
                {
                    NPC.TargetClosest(false);
                    NPC.netUpdate = true;
                }
                if (num1198 < 180f)
                {
                    NPC.localAI[2] = MathHelper.Lerp(NPC.localAI[2], 1f, 0.2f);
                    NPC.localAI[1] -= 0.05f;
                    if (NPC.localAI[1] < 0f)
                    {
                        NPC.localAI[1] = 0f;
                    }
                    NPC.velocity *= 0.95f;
                    if (NPC.velocity.Length() < 1f)
                    {
                        NPC.velocity = Vector2.Zero;
                    }
                    if (num1198 >= 60f)
                    {
                        Vector2 center25 = NPC.Center;
                        int num1209 = 0;
                        if (num1198 >= 120f)
                        {
                            num1209 = 1;
                        }
                        for (int num1210 = 0; num1210 < 1 + num1209; num1210++)
                        {
                            int num1211 = 229;
                            float num1212 = 0.8f;
                            if (num1210 % 2 == 1)
                            {
                                num1211 = 229;
                                num1212 = 1.65f;
                            }
                            Vector2 vector194 = center25 + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * value31 / 2f;
                            int num1213 = Dust.NewDust(vector194 - Vector2.One * 8f, 16, 16, num1211, NPC.velocity.X / 2f, NPC.velocity.Y / 2f, 0, default, 1f);
                            Main.dust[num1213].velocity = Vector2.Normalize(center25 - vector194) * 3.5f * (10f - num1209 * 2f) / 10f;
                            Main.dust[num1213].noGravity = true;
                            Main.dust[num1213].scale = num1212;
                            Main.dust[num1213].customData = this;
                        }
                        return;
                    }
                }
                else
                {
                    if (num1198 < num1194 - 15f)
                    {
                        if (num1198 == 180f && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            NPC.TargetClosest(false);
                            Vector2 vector195 = Main.player[NPC.target].Center - NPC.Center;
                            vector195.Normalize();
                            float num1214 = -1f;
                            if (vector195.X < 0f)
                            {
                                num1214 = 1f;
                            }
                            vector195 = vector195.RotatedBy((double)(-(double)num1214 * 6.28318548f / 6f), default);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector195.X, vector195.Y, ProjectileID.PhantasmalDeathray, 50, 0f, Main.myPlayer, num1214 * 6.28318548f / 540f, NPC.whoAmI);
                            NPC.ai[2] = (vector195.ToRotation() + 9.424778f) * num1214;
                            NPC.netUpdate = true;
                        }
                        NPC.localAI[1] += 0.05f;
                        if (NPC.localAI[1] > 1f)
                        {
                            NPC.localAI[1] = 1f;
                        }
                        float num1215 = (NPC.ai[2] >= 0f).ToDirectionInt();
                        float num1216 = NPC.ai[2];
                        if (num1216 < 0f)
                        {
                            num1216 *= -1f;
                        }
                        num1216 += -9.424778f;
                        num1216 += num1215 * 6.28318548f / 540f;
                        NPC.localAI[0] = num1216;
                        NPC.ai[2] = (num1216 + 9.424778f) * num1215;
                        return;
                    }
                    NPC.localAI[1] -= 0.07f;
                    if (NPC.localAI[1] < 0f)
                    {
                        NPC.localAI[1] = 0f;
                        return;
                    }
                }
            }
        }

        public override void OnKill()
        {
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, 1, ModContent.DustType<Dusts.CthulhuDust>(), -NPC.velocity.X * 0.2f,
                    -NPC.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.CthulhuDust>(), -NPC.velocity.X * 0.2f,
                    -NPC.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture8 = TextureAssets.Npc[NPC.type].Value;
            Texture2D texture2D30 = ModContent.Request<Texture2D>(Texture + "_Pupil").Value;
            Vector2 origin15 = new Vector2(40f, 40f);
            Vector2 value33 = new Vector2(30f, 30f);
            Vector2 arg_A019_0 = NPC.Center;
            Point point4 = NPC.Center.ToTileCoordinates();
            Color alpha11 = NPC.GetAlpha(Color.Lerp(Lighting.GetColor(point4.X, point4.Y), Color.White, 0.3f));
            Main.spriteBatch.Draw(texture8, NPC.Center - screenPos, new Rectangle?(NPC.frame), alpha11, NPC.rotation, origin15, 1f, SpriteEffects.None, 0f);
            Vector2 value34 = Utils.Vector2FromElipse(NPC.localAI[0].ToRotationVector2(), value33 * NPC.localAI[1]);
            Main.spriteBatch.Draw(texture2D30, NPC.Center - screenPos + value34, null, alpha11, NPC.rotation, texture2D30.Size() / 2f, NPC.localAI[2], SpriteEffects.None, 0f);
            return false;
        }
        */
    }
}
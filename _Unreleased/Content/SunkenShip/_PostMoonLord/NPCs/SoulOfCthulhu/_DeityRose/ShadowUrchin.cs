using AAModClassic._Unreleased.Content.SunkenShip.World.Biomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityRose
{
    public class ShadowUrchin : ModNPC
    {

        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Shadow Urchin");
        }
        public override void SetDefaults()
        {
            NPC.width = 22;
            NPC.height = 22;
            NPC.aiStyle = -1;
            NPC.damage = 130;
            NPC.defense = 80;
            NPC.lifeMax = 4000;
            NPC.HitSound = SoundID.NPCHit34;
            NPC.DeathSound = SoundID.NPCDeath37;
            NPC.value = 0;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[39] = true;
            NPC.knockBackResist = 0.1f;
            NPC.noGravity = true;
            SpawnModBiomes = [ModContent.GetInstance<SunkenShipBiome>().Type];
        }

        
        public override void AI()
        {
            NPC.noTileCollide = false;
            if (NPC.ai[0] == 0f)
            {
                NPC.TargetClosest(true);
                if (Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1))
                {
                    NPC.ai[0] = 1f;
                }
                else
                {
                    Vector2 value41 = Main.player[NPC.target].Center - NPC.Center;
                    value41.Y -= (float)(Main.player[NPC.target].height / 4);
                    float num1262 = value41.Length();
                    if (num1262 > 800f)
                    {
                        NPC.ai[0] = 2f;
                    }
                    else
                    {
                        Vector2 center26 = NPC.Center;
                        center26.X = Main.player[NPC.target].Center.X;
                        Vector2 vector200 = center26 - NPC.Center;
                        if (vector200.Length() > 8f && Collision.CanHit(NPC.Center, 1, 1, center26, 1, 1))
                        {
                            NPC.ai[0] = 3f;
                            NPC.ai[1] = center26.X;
                            NPC.ai[2] = center26.Y;
                            Vector2 center27 = NPC.Center;
                            center27.Y = Main.player[NPC.target].Center.Y;
                            if (vector200.Length() > 8f && Collision.CanHit(NPC.Center, 1, 1, center27, 1, 1) && Collision.CanHit(center27, 1, 1, Main.player[NPC.target].position, 1, 1))
                            {
                                NPC.ai[0] = 3f;
                                NPC.ai[1] = center27.X;
                                NPC.ai[2] = center27.Y;
                            }
                        }
                        else
                        {
                            center26 = NPC.Center;
                            center26.Y = Main.player[NPC.target].Center.Y;
                            if ((center26 - NPC.Center).Length() > 8f && Collision.CanHit(NPC.Center, 1, 1, center26, 1, 1))
                            {
                                NPC.ai[0] = 3f;
                                NPC.ai[1] = center26.X;
                                NPC.ai[2] = center26.Y;
                            }
                        }
                        if (NPC.ai[0] == 0f)
                        {
                            NPC.localAI[0] = 0f;
                            value41.Normalize();
                            value41 *= 0.5f;
                            NPC.velocity += value41;
                            NPC.ai[0] = 4f;
                            NPC.ai[1] = 0f;
                        }
                    }
                }
            }
            else if (NPC.ai[0] == 1f)
            {
                NPC.rotation += (float)NPC.direction * 0.3f;
                Vector2 value42 = Main.player[NPC.target].Center - NPC.Center;
                float num1263 = value42.Length();
                float num1264 = 5.5f;
                num1264 += num1263 / 100f;
                int num1265 = 50;
                value42.Normalize();
                value42 *= num1264;
                NPC.velocity = (NPC.velocity * (float)(num1265 - 1) + value42) / (float)num1265;
                if (!Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1))
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                }
            }
            else if (NPC.ai[0] == 2f)
            {
                NPC.rotation = NPC.velocity.X * 0.1f;
                NPC.noTileCollide = true;
                Vector2 value43 = Main.player[NPC.target].Center - NPC.Center;
                float num1267 = value43.Length();
                float scaleFactor11 = 3f;
                int num1268 = 3;
                value43.Normalize();
                value43 *= scaleFactor11;
                NPC.velocity = (NPC.velocity * (float)(num1268 - 1) + value43) / (float)num1268;
                if (num1267 < 600f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.ai[0] = 0f;
                }
            }
            else if (NPC.ai[0] == 3f)
            {
                NPC.rotation = NPC.velocity.X * 0.1f;
                Vector2 value44 = new Vector2(NPC.ai[1], NPC.ai[2]);
                Vector2 value45 = value44 - NPC.Center;
                float num1269 = value45.Length();
                float num1270 = 2f;
                float num1271 = 3f;
                value45.Normalize();
                value45 *= num1270;
                NPC.velocity = (NPC.velocity * (num1271 - 1f) + value45) / num1271;
                if (NPC.collideX || NPC.collideY)
                {
                    NPC.ai[0] = 4f;
                    NPC.ai[1] = 0f;
                }
                if (num1269 < num1270 || num1269 > 800f || Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1))
                {
                    NPC.ai[0] = 0f;
                }
            }
            else if (NPC.ai[0] == 4f)
            {
                NPC.rotation = NPC.velocity.X * 0.1f;
                if (NPC.collideX)
                {
                    NPC.velocity.X = NPC.velocity.X * -0.8f;
                }
                if (NPC.collideY)
                {
                    NPC.velocity.Y = NPC.velocity.Y * -0.8f;
                }
                Vector2 value46;
                if (NPC.velocity.X == 0f && NPC.velocity.Y == 0f)
                {
                    value46 = Main.player[NPC.target].Center - NPC.Center;
                    value46.Y -= (float)(Main.player[NPC.target].height / 4);
                    value46.Normalize();
                    NPC.velocity = value46 * 0.1f;
                }
                float scaleFactor12 = 2f;
                float num1272 = 20f;
                value46 = NPC.velocity;
                value46.Normalize();
                value46 *= scaleFactor12;
                NPC.velocity = (NPC.velocity * (num1272 - 1f) + value46) / num1272;
                NPC.ai[1] += 1f;
                if (NPC.ai[1] > 180f)
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                }
                if (Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1))
                {
                    NPC.ai[0] = 0f;
                }
                NPC.localAI[0] += 1f;
                if (NPC.localAI[0] >= 5f && !Collision.SolidCollision(NPC.position - new Vector2(10f, 10f), NPC.width + 20, NPC.height + 20))
                {
                    NPC.localAI[0] = 0f;
                    Vector2 center28 = NPC.Center;
                    center28.X = Main.player[NPC.target].Center.X;
                    if (Collision.CanHit(NPC.Center, 1, 1, center28, 1, 1) && Collision.CanHit(NPC.Center, 1, 1, center28, 1, 1) && Collision.CanHit(Main.player[NPC.target].Center, 1, 1, center28, 1, 1))
                    {
                        NPC.ai[0] = 3f;
                        NPC.ai[1] = center28.X;
                        NPC.ai[2] = center28.Y;
                    }
                    else
                    {
                        center28 = NPC.Center;
                        center28.Y = Main.player[NPC.target].Center.Y;
                        if (Collision.CanHit(NPC.Center, 1, 1, center28, 1, 1) && Collision.CanHit(Main.player[NPC.target].Center, 1, 1, center28, 1, 1))
                        {
                            NPC.ai[0] = 3f;
                            NPC.ai[1] = center28.X;
                            NPC.ai[2] = center28.Y;
                        }
                    }
                }
            }
            else if (NPC.ai[0] == 5f)
            {
                Player player7 = Main.player[NPC.target];
                if (!player7.active || player7.dead)
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                    NPC.netUpdate = true;
                }
                else
                {
                    NPC.Center = ((player7.gravDir == 1f) ? player7.Top : player7.Bottom) + new Vector2((float)(player7.direction * 4), 0f);
                    NPC.gfxOffY = player7.gfxOffY;
                    NPC.velocity = Vector2.Zero;
                    player7.AddBuff(BuffID.Obstructed, 59, true);
                }
            }
            
        }
    }
}
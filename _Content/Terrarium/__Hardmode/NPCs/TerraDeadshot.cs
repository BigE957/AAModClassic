using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic._Content.Terrarium.World.Biomes;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;

namespace AAModClassic._Content.Terrarium.__Hardmode.NPCs
{
    public class TerraDeadshot : ModNPC, IBannerNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Deadshot");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.VortexRifleman];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Velocity = -2
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 56;
            NPC.aiStyle = -1;
            NPC.damage = 80;
            NPC.defense = 30;
            NPC.lifeMax = 700;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.knockBackResist = 0.4f;
            NPC.buffImmune[31] = false;
            AnimationType = NPCID.VortexRifleman;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.TerraDeadshotBanner>();
            SpawnModBiomes = [ModContent.GetInstance<TerrariumBiome>().Type];
        }
        
        public override void AI()
        {
            bool flag4 = false;
            if (NPC.velocity.X == 0f)
            {
                flag4 = true;
            }
            if (NPC.justHit)
            {
                flag4 = false;
            }

            int num36 = 60;

            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = true;
            bool flag8 = true;
            if (!flag7 && flag8)
            {
                if (NPC.velocity.Y == 0f && (NPC.velocity.X > 0f && NPC.direction < 0 || NPC.velocity.X < 0f && NPC.direction > 0))
                {
                    flag5 = true;
                }
                if (NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= num36 || flag5)
                {
                    NPC.ai[3] += 1f;
                }
                else if (Math.Abs(NPC.velocity.X) > 0.9 && NPC.ai[3] > 0f)
                {
                    NPC.ai[3] -= 1f;
                }
                if (NPC.ai[3] > num36 * 10)
                {
                    NPC.ai[3] = 0f;
                }
                if (NPC.justHit)
                {
                    NPC.ai[3] = 0f;
                }
                if (NPC.ai[3] == num36)
                {
                    NPC.netUpdate = true;
                }
            }

            if (NPC.ai[3] < num36)
            {

                NPC.TargetClosest(true);
            }

            float num57 = 6f;

            if (NPC.velocity.X < -num57 || NPC.velocity.X > num57)
            {
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity *= 0.8f;
                }
            }
            else if (NPC.velocity.X < num57 && NPC.direction == 1)
            {
                NPC.velocity.X = NPC.velocity.X + 0.07f;
                if (NPC.velocity.X > num57)
                {
                    NPC.velocity.X = num57;
                }
            }
            else if (NPC.velocity.X > -num57 && NPC.direction == -1)
            {
                NPC.velocity.X = NPC.velocity.X - 0.07f;
                if (NPC.velocity.X < -num57)
                {
                    NPC.velocity.X = -num57;
                }
            }
            if (NPC.velocity.Y == 0f)
            {
                NPC.ai[2] = 0f;
            }
            if (NPC.velocity.Y != 0f && NPC.ai[2] == 1f)
            {
                NPC.TargetClosest(true);
                NPC.spriteDirection = -NPC.direction;
                if (Collision.CanHit(NPC.Center, 0, 0, Main.player[NPC.target].Center, 0, 0))
                {
                    float num82 = Main.player[NPC.target].Center.X - NPC.direction * 400 - NPC.Center.X;
                    float num83 = Main.player[NPC.target].Bottom.Y - NPC.Bottom.Y;
                    if (num82 < 0f && NPC.velocity.X > 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.9f;
                    }
                    else if (num82 > 0f && NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.9f;
                    }
                    if (num82 < 0f && NPC.velocity.X > -5f)
                    {
                        NPC.velocity.X = NPC.velocity.X - 0.1f;
                    }
                    else if (num82 > 0f && NPC.velocity.X < 5f)
                    {
                        NPC.velocity.X = NPC.velocity.X + 0.1f;
                    }
                    if (NPC.velocity.X > 6f)
                    {
                        NPC.velocity.X = 6f;
                    }
                    if (NPC.velocity.X < -6f)
                    {
                        NPC.velocity.X = -6f;
                    }
                    if (num83 < -20f && NPC.velocity.Y > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y * 0.8f;
                    }
                    else if (num83 > 20f && NPC.velocity.Y < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y * 0.8f;
                    }
                    if (num83 < -20f && NPC.velocity.Y > -5f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - 0.3f;
                    }
                    else if (num83 > 20f && NPC.velocity.Y < 5f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + 0.3f;
                    }
                }
                if (Main.rand.NextBool(3))
                {
                    Vector2 position = NPC.Center + new Vector2(NPC.direction * -14, -8f) - Vector2.One * 4f;
                    Vector2 velocity = new Vector2(NPC.direction * -6, 12f) * 0.2f + Utils.RandomVector2(Main.rand, -1f, 1f) * 0.1f;
                    Dust dust6 = Main.dust[Dust.NewDust(position, 8, 8, DustID.Vortex, velocity.X, velocity.Y, 100, Color.Transparent, 1f + Main.rand.NextFloat() * 0.5f)];
                    dust6.noGravity = true;
                    dust6.velocity = velocity;
                    dust6.customData = this;
                }
                for (int num84 = 0; num84 < 200; num84++)
                {
                    if (num84 != NPC.whoAmI && Main.npc[num84].active && Main.npc[num84].type == NPC.type && Math.Abs(NPC.position.X - Main.npc[num84].position.X) + Math.Abs(NPC.position.Y - Main.npc[num84].position.Y) < NPC.width)
                    {
                        if (NPC.position.X < Main.npc[num84].position.X)
                        {
                            NPC.velocity.X = NPC.velocity.X - 0.05f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X + 0.05f;
                        }
                        if (NPC.position.Y < Main.npc[num84].position.Y)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - 0.05f;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y + 0.05f;
                        }
                    }
                }
            }
            else if (Main.player[NPC.target].Center.Y + 100f < NPC.position.Y && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
            {
                NPC.velocity.Y = -5f;
                NPC.ai[2] = 1f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.localAI[2] += 1f;
                if (NPC.localAI[2] >= 360 + Main.rand.Next(360) && NPC.Distance(Main.player[NPC.target].Center) < 400f && Math.Abs(NPC.DirectionTo(Main.player[NPC.target].Center).Y) < 0.5f && Collision.CanHitLine(NPC.Center, 0, 0, Main.player[NPC.target].Center, 0, 0))
                {
                    NPC.localAI[2] = 0f;
                    Vector2 vector13 = NPC.Center + new Vector2(NPC.direction * 30, 2f);
                    Vector2 vector14 = NPC.DirectionTo(Main.player[NPC.target].Center) * 7f;
                    if (vector14.HasNaNs())
                    {
                        vector14 = new Vector2(NPC.direction * 8, 0f);
                    }
                    int num85 = Main.expertMode ? 50 : 75;
                    for (int num86 = 0; num86 < 4; num86++)
                    {
                        Vector2 vector15 = vector14 + Utils.RandomVector2(Main.rand, -0.8f, 0.8f);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), vector13.X, vector13.Y, vector15.X - 5, vector15.Y - 5, ModContent.ProjectileType<TerraDeadshot_Deadshot>(), num85, 1f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            bool flag23 = false;
            if (NPC.velocity.Y == 0f)
            {
                int num167 = (int)(NPC.position.Y + NPC.height + 7f) / 16;
                int num168 = (int)NPC.position.X / 16;
                int num169 = (int)(NPC.position.X + NPC.width) / 16;
                for (int num170 = num168; num170 <= num169; num170++)
                {
                    if (Main.tile[num170, num167] == null)
                    {
                        return;
                    }
                    if (Main.tile[num170, num167].HasUnactuatedTile && Main.tileSolid[Main.tile[num170, num167].TileType])
                    {
                        flag23 = true;
                        break;
                    }
                }
            }

            if (NPC.velocity.Y >= 0f)
            {
                int num171 = 0;
                if (NPC.velocity.X < 0f)
                {
                    num171 = -1;
                }
                if (NPC.velocity.X > 0f)
                {
                    num171 = 1;
                }
                Vector2 position2 = NPC.position;
                position2.X += NPC.velocity.X;
                int num172 = (int)((position2.X + NPC.width / 2 + (NPC.width / 2 + 1) * num171) / 16f);
                int num173 = (int)((position2.Y + NPC.height - 1f) / 16f);
                if (!(Main.tile[num172, num173] == null &&
                    Main.tile[num172, num173 - 1] == null &&
                    Main.tile[num172, num173 - 2] == null &&
                    Main.tile[num172, num173 - 3] == null &&
                    Main.tile[num172, num173 + 1] == null &&
                    Main.tile[num172 - num171, num173 - 3] == null))
                {
                    if (num172 * 16 < position2.X + NPC.width && num172 * 16 + 16 > position2.X && (Main.tile[num172, num173].HasUnactuatedTile && !Main.tile[num172, num173].TopSlope && !Main.tile[num172, num173 - 1].TopSlope && Main.tileSolid[Main.tile[num172, num173].TileType] && !Main.tileSolidTop[Main.tile[num172, num173].TileType] || Main.tile[num172, num173 - 1].IsHalfBlock && Main.tile[num172, num173 - 1].HasUnactuatedTile) && (!Main.tile[num172, num173 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 1].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 1].TileType] || Main.tile[num172, num173 - 1].IsHalfBlock && (!Main.tile[num172, num173 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 4].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 4].TileType])) && (!Main.tile[num172, num173 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 2].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 2].TileType]) && (!Main.tile[num172, num173 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 3].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 3].TileType]) && (!Main.tile[num172 - num171, num173 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172 - num171, num173 - 3].TileType]))
                    {
                        float num174 = num173 * 16;
                        if (Main.tile[num172, num173].IsHalfBlock)
                        {
                            num174 += 8f;
                        }
                        if (Main.tile[num172, num173 - 1].IsHalfBlock)
                        {
                            num174 -= 8f;
                        }
                        if (num174 < position2.Y + NPC.height)
                        {
                            float num175 = position2.Y + NPC.height - num174;
                            float num176 = 16.1f;
                            if (num175 <= num176)
                            {
                                NPC.gfxOffY += NPC.position.Y + NPC.height - num174;
                                NPC.position.Y = num174 - NPC.height;
                                if (num175 < 9f)
                                {
                                    NPC.stepSpeed = 1f;
                                }
                                else
                                {
                                    NPC.stepSpeed = 2f;
                                }
                            }
                        }
                    }
                }
            }
            if (flag23)
            {
                int num177 = (int)((NPC.position.X + NPC.width / 2 + (NPC.width / 2 + 16) * NPC.direction) / 16f);
                int num178 = (int)((NPC.position.Y + NPC.height - 15f) / 16f);


                bool nullcheck = Main.tile[num177, num178] == null &&
                    Main.tile[num177, num178 - 1] == null &&
                    Main.tile[num177, num178 - 2] == null &&
                    Main.tile[num177, num178 - 3] == null &&
                    Main.tile[num177, num178 + 1] == null &&
                    Main.tile[num177 + NPC.direction, num178 - 1] == null &&
                    Main.tile[num177 + NPC.direction, num178 + 1] == null &&
                    Main.tile[num177 - NPC.direction, num178 + 1] == null;

                if (nullcheck && Main.tile[num177, num178 - 1].HasUnactuatedTile && (Main.tile[num177, num178 - 1].TileType == TileID.ClosedDoor || Main.tile[num177, num178 - 1].TileType == TileID.TallGateClosed) && flag6)
                {
                    NPC.ai[2] += 1f;
                    NPC.ai[3] = 0f;
                    if (NPC.ai[2] >= 60f)
                    {

                        NPC.velocity.X = 0.5f * -NPC.direction;
                        int num179 = 5;
                        if (Main.tile[num177, num178 - 1].TileType == TileID.TallGateClosed)
                        {
                            num179 = 2;
                        }
                        NPC.ai[1] += num179;

                        NPC.ai[2] = 0f;
                        if (NPC.ai[1] >= 10f)
                        {
                            NPC.ai[1] = 10f;
                        }

                        WorldGen.KillTile(num177, num178 - 1, true, false, false);
                    }
                }
                else
                {
                    int num180 = NPC.spriteDirection;

                    num180 *= -1;
                    if (NPC.velocity.X < 0f && num180 == -1 || NPC.velocity.X > 0f && num180 == 1)
                    {
                        if (NPC.height >= 32 && Main.tile[num177, num178 - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[num177, num178 - 2].TileType])
                        {
                            if (Main.tile[num177, num178 - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[num177, num178 - 3].TileType])
                            {
                                NPC.velocity.Y = -8f;
                                NPC.netUpdate = true;
                            }
                            else
                            {
                                NPC.velocity.Y = -7f;
                                NPC.netUpdate = true;
                            }
                        }
                        else if (Main.tile[num177, num178 - 1].HasUnactuatedTile && Main.tileSolid[Main.tile[num177, num178 - 1].TileType])
                        {
                            NPC.velocity.Y = -6f;
                            NPC.netUpdate = true;
                        }
                        else if (NPC.position.Y + NPC.height - num178 * 16 > 20f && Main.tile[num177, num178].HasUnactuatedTile && !Main.tile[num177, num178].TopSlope && Main.tileSolid[Main.tile[num177, num178].TileType])
                        {
                            NPC.velocity.Y = -5f;
                            NPC.netUpdate = true;
                        }
                        else if (NPC.directionY < 0 && (!Main.tile[num177, num178 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num177, num178 + 1].TileType]) && (!Main.tile[num177 + NPC.direction, num178 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num177 + NPC.direction, num178 + 1].TileType]))
                        {
                            NPC.velocity.Y = -8f;
                            NPC.velocity.X = NPC.velocity.X * 1.5f;
                            NPC.netUpdate = true;
                        }
                        else if (flag6)
                        {
                            NPC.ai[1] = 0f;
                            NPC.ai[2] = 0f;
                        }
                        if (NPC.velocity.Y == 0f && flag4 && NPC.ai[3] == 1f)
                        {
                            NPC.velocity.Y = -5f;
                        }
                    }

                }
            }
            else if (flag6)
            {
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
            }

        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (!Main.dedServ)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraDeadshotGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraDeadshotGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraDeadshotGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraDeadshotGore4").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraDeadshotGore5").Type, 1f);
                }
                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.RangedDust>();
                int dust2 = ModContent.DustType<Dusts.RangedDust>();
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

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notUnreleasedRule = new(new NotUnreleasedAndIsUnofficial());

            notUnreleasedRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<TerraPrism>(), 40));

            npcLoot.Add(notUnreleasedRule);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy)
            {
                NPC.spriteDirection = -1;
            }
            return true;
        }
    }
}

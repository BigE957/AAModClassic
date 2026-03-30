using AAModClassic.Items.Materials;
using AAModClassic.Items.Throwing;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Mire
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Kappa : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Kappa");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.CreatureFromTheDeep];
		}

		public override void SetDefaults()
		{
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 90;
			NPC.defense = 16;
			NPC.lifeMax = 300;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 450f;
			NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
			AnimationType = NPCID.CreatureFromTheDeep;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("KappaBanner").Type;
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HydraToxin>()));
        }

        public override void AI()
        {
            if (NPC.wet)
            {
                NPC.knockBackResist = 0f;
                NPC.ai[3] = -0.10101f;
                NPC.noGravity = true;
                Vector2 center = NPC.Center;
                NPC.width = 34;
                NPC.height = 24;
                NPC.position.X = center.X - NPC.width / 2;
                NPC.position.Y = center.Y - NPC.height / 2;
                NPC.TargetClosest(true);
                if (NPC.collideX)
                {
                    NPC.velocity.X = -NPC.oldVelocity.X;
                }
                if (NPC.velocity.X < 0f)
                {
                    NPC.direction = -1;
                }
                if (NPC.velocity.X > 0f)
                {
                    NPC.direction = 1;
                }
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].Center, 1, 1))
                {
                    Vector2 value = Main.player[NPC.target].Center - NPC.Center;
                    value.Normalize();
                    value *= 5f;
                    NPC.velocity = (NPC.velocity * 19f + value) / 20f;
                    return;
                }
                float num2 = 5f;
                if (NPC.velocity.Y > 0f)
                {
                    num2 = 3f;
                }
                if (NPC.velocity.Y < 0f)
                {
                    num2 = 8f;
                }
                Vector2 value2 = new Vector2(NPC.direction, -1f);
                value2.Normalize();
                value2 *= num2;
                if (num2 < 5f)
                {
                    NPC.velocity = (NPC.velocity * 24f + value2) / 25f;
                        return;
                }
                NPC.velocity = (NPC.velocity * 9f + value2) / 10f;
                return;
            }
            else
            {
                NPC.knockBackResist = 0.4f * Main.GameModeInfo.KnockbackToEnemiesMultiplier;
                NPC.noGravity = false;
                Vector2 center2 = NPC.Center;
                NPC.width = 18;
                NPC.height = 40;
                NPC.position.X = center2.X - NPC.width / 2;
                NPC.position.Y = center2.Y - NPC.height / 2;
                if (NPC.ai[3] == -0.10101f)
                {
                    NPC.ai[3] = 0f;
                    float num3 = NPC.velocity.Length();
                    num3 *= 2f;
                    if (num3 > 10f)
                    {
                        num3 = 10f;
                    }
                    NPC.velocity.Normalize();
                    NPC.velocity *= num3;
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.direction = -1;
                    }
                    if (NPC.velocity.X > 0f)
                    {
                            NPC.direction = 1;
                    }
                    NPC.spriteDirection = NPC.direction;
                }
            }
            
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
            bool flag6 = true;
            bool flag7 = false;
            bool flag8 = true;
            if (!flag7 && flag8)
            {
                if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0)))
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
            if (NPC.ai[3] < num36 && (!Main.dayTime || NPC.position.Y > Main.worldSurface * 16.0))
            {
                
                NPC.TargetClosest(true);
            }
            else if (NPC.ai[2] <= 0f)
            {
                if (Main.dayTime && NPC.position.Y / 16f < Main.worldSurface && NPC.timeLeft > 10)
                {
                    NPC.timeLeft = 10;
                }
                if (NPC.velocity.X == 0f)
                {
                    if (NPC.velocity.Y == 0f)
                    {
                        NPC.ai[0] += 1f;
                        if (NPC.ai[0] >= 2f)
                        {
                            NPC.direction *= -1;
                            NPC.spriteDirection = NPC.direction;
                            NPC.ai[0] = 0f;
                        }
                    }
                }
                else
                {
                    NPC.ai[0] = 0f;
                }
                if (NPC.direction == 0)
                {
                    NPC.direction = 1;
                }
            }
            if (NPC.velocity.X < -2f || NPC.velocity.X > 2f)
            {
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity *= 0.8f;
                }
            }
            else if (NPC.velocity.X < 2f && NPC.direction == 1)
            {
                NPC.velocity.X = NPC.velocity.X + 0.07f;
                if (NPC.velocity.X > 2f)
                {
                    NPC.velocity.X = 2f;
                }
            }
            else if (NPC.velocity.X > -2f && NPC.direction == -1)
            {
                NPC.velocity.X = NPC.velocity.X - 0.07f;
                if (NPC.velocity.X < -2f)
                {
                    NPC.velocity.X = -2f;
                }
            }

            float num79 = 1f;
            if (NPC.velocity.X < -num79 || NPC.velocity.X > num79)
            {
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity *= 0.8f;
                }
            }
            else if (NPC.velocity.X < num79 && NPC.direction == 1)
            {
                NPC.velocity.X = NPC.velocity.X + 0.07f;
                if (NPC.velocity.X > num79)
                {
                    NPC.velocity.X = num79;
                }
            }
            else if (NPC.velocity.X > -num79 && NPC.direction == -1)
            {
                NPC.velocity.X = NPC.velocity.X - 0.07f;
                if (NPC.velocity.X < -num79)
                {
                    NPC.velocity.X = -num79;
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
                    if (num172 * 16 < position2.X + NPC.width && num172 * 16 + 16 > position2.X && ((Main.tile[num172, num173].HasUnactuatedTile && !Main.tile[num172, num173].TopSlope && !Main.tile[num172, num173 - 1].TopSlope && Main.tileSolid[Main.tile[num172, num173].TileType] && !Main.tileSolidTop[Main.tile[num172, num173].TileType]) || (Main.tile[num172, num173 - 1].IsHalfBlock && Main.tile[num172, num173 - 1].HasUnactuatedTile)) && (!Main.tile[num172, num173 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 1].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 1].TileType] || (Main.tile[num172, num173 - 1].IsHalfBlock && (!Main.tile[num172, num173 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 4].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 4].TileType]))) && (!Main.tile[num172, num173 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 2].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 2].TileType]) && (!Main.tile[num172, num173 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 3].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 3].TileType]) && (!Main.tile[num172 - num171, num173 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172 - num171, num173 - 3].TileType]))
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
                int num177 = (int)((NPC.position.X + NPC.width / 2 + 15 * NPC.direction) / 16f);
                int num178 = (int)((NPC.position.Y + NPC.height - 15f) / 16f);

                bool nullcheck = !(Main.tile[num177, num178] == null &&
                    Main.tile[num177, num178 - 1] == null &&
                    Main.tile[num177, num178 - 2] == null &&
                    Main.tile[num177, num178 - 3] == null &&
                    Main.tile[num177, num178 + 1] == null &&
                    Main.tile[num177 + NPC.direction, num178 - 1] == null &&
                    Main.tile[num177 + NPC.direction, num178 + 1] == null &&
                    Main.tile[num177 - NPC.direction, num178 + 1] == null);

                if (nullcheck && (Main.tile[num177, num178 - 1].HasUnactuatedTile && (Main.tile[num177, num178 - 1].TileType == TileID.ClosedDoor || Main.tile[num177, num178 - 1].TileType == TileID.TallGateClosed) && flag6))
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
                    if ((NPC.velocity.X < 0f && num180 == -1) || (NPC.velocity.X > 0f && num180 == 1))
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
    }
}

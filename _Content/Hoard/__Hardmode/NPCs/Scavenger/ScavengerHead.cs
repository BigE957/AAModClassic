using Microsoft.Xna.Framework;

using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Content.Hoard.__Hardmode.Items.Materials;
using AAModClassic.Items.Banners;
using AAModClassic.Items.Usable;

namespace AAModClassic._Content.Hoard.__Hardmode.NPCs.Scavenger
{
    public class ScavengerHead : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scavenger");
        }

        public override void SetDefaults()
        {
            NPC.damage = 40;
            NPC.width = 50;
            NPC.height = 50;
            NPC.defense = 0;
            NPC.lifeMax = 1000;
            NPC.aiStyle = NPCAIStyleID.Worm;
            AIType = -1;
            AnimationType = NPCID.GiantWormHead;
            NPC.knockBackResist = 0f;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.behindTiles = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.Item14;
            NPC.netAlways = true;
            Banner = ModContent.NPCType<ScavengerHead>();
			BannerItem = ModContent.ItemType<ScavengerBanner>();
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            if (NPC.ai[3] > 0f)
            {
                NPC.realLife = (int)NPC.ai[3];
            }
            if (NPC.target < 0 || NPC.target == 255 || player.dead)
            {
                NPC.TargetClosest(true);
            }
            NPC.velocity.Length();
            if (NPC.ai[2] != 1)
            {
                int Previous = NPC.whoAmI;
                for (int num36 = 0; num36 < 6; num36++)
                {
                    int a;
                    if (num36 >= 0 && num36 < 5)
                    {
                        a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + NPC.width / 2, (int)NPC.position.Y + NPC.height / 2, ModContent.NPCType<ScavengerBody>(), NPC.whoAmI);
                    }
                    else
                    {
                        a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + NPC.width / 2, (int)NPC.position.Y + NPC.height / 2, ModContent.NPCType<ScavengerTail>(), NPC.whoAmI);
                    }
                    Main.npc[a].realLife = NPC.whoAmI;
                    Main.npc[a].ai[2] = NPC.whoAmI;
                    Main.npc[a].ai[1] = Previous;
                    Main.npc[Previous].ai[0] = a;
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, a, 0f, 0f, 0f, 0);
                    Previous = a;
                }
                NPC.ai[2] = 1;
            }
            int num50 = (int)(NPC.position.X / 16f) - 1;
            int num51 = (int)((NPC.position.X + NPC.width) / 16f) + 2;
            int num52 = (int)(NPC.position.Y / 16f) - 1;
            int num53 = (int)((NPC.position.Y + NPC.height) / 16f) + 2;
            if (num50 < 0)
            {
                num50 = 0;
            }
            if (num51 > Main.maxTilesX)
            {
                num51 = Main.maxTilesX;
            }
            if (num52 < 0)
            {
                num52 = 0;
            }
            if (num53 > Main.maxTilesY)
            {
                num53 = Main.maxTilesY;
            }
            bool flies = false;
            if (!flies)
            {
                for (int num952 = num50; num952 < num51; num952++)
                {
                    for (int num953 = num52; num953 < num53; num953++)
                    {
                        if (Main.tile[num952, num953] != null && (Main.tile[num952, num953].HasUnactuatedTile && (Main.tileSolid[Main.tile[num952, num953].TileType] || Main.tileSolidTop[Main.tile[num952, num953].TileType] && Main.tile[num952, num953].TileFrameY == 0) || Main.tile[num952, num953].LiquidAmount > 64))
                        {
                            Vector2 vector105;
                            vector105.X = num952 * 16;
                            vector105.Y = num953 * 16;
                            if (NPC.position.X + NPC.width > vector105.X && NPC.position.X < vector105.X + 16f && NPC.position.Y + NPC.height > vector105.Y && NPC.position.Y < vector105.Y + 16f)
                            {
                                flies = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!flies)
            {
                NPC.localAI[1] = 1f;
                Rectangle rectangle12 = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
                bool flag95 = true;
                if (NPC.position.Y > player.position.Y)
                {
                    for (int num955 = 0; num955 < 255; num955++)
                    {
                        if (Main.player[num955].active)
                        {
                            Rectangle rectangle13 = new Rectangle((int)Main.player[num955].position.X - 1000, (int)Main.player[num955].position.Y - 1000, 2000, 2000);
                            if (rectangle12.Intersects(rectangle13))
                            {
                                flag95 = false;
                                break;
                            }
                        }
                    }
                    if (flag95)
                    {
                        flies = true;
                    }
                }
            }
            else
            {
                NPC.localAI[1] = 0f;
            }
            if (player.dead)
            {
                flies = false;
                NPC.velocity.Y = NPC.velocity.Y + 1f;
                if (NPC.position.Y > Main.worldSurface * 16.0)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 1f;
                }
                if (NPC.position.Y > Main.rockLayer * 16.0)
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
            float speed = 12.5f;
            float turnSpeed = 0.125f;
            float num58 = speed;
            float num59 = turnSpeed;
            Vector2 vector18 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float num61 = player.position.X + player.width / 2;
            float num62 = player.position.Y + player.height / 2;
            num61 = (int)(num61 / 16f) * 16;
            num62 = (int)(num62 / 16f) * 16;
            vector18.X = (int)(vector18.X / 16f) * 16;
            vector18.Y = (int)(vector18.Y / 16f) * 16;
            num61 -= vector18.X;
            num62 -= vector18.Y;
            float num63 = (float)System.Math.Sqrt(num61 * num61 + num62 * num62);
            if (NPC.ai[1] > 0f && NPC.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                    num61 = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - vector18.X;
                    num62 = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - vector18.Y;
                }
                catch
                {
                }
                NPC.rotation = (float)System.Math.Atan2(num62, num61) + 1.57f;
                num63 = (float)System.Math.Sqrt(num61 * num61 + num62 * num62);
                int num64 = NPC.width;
                num63 = (num63 - num64) / num63;
                num61 *= num63;
                num62 *= num63;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + num61;
                NPC.position.Y = NPC.position.Y + num62;
            }
            else
            {
                if (!flies)
                {
                    NPC.TargetClosest(true);
                    NPC.velocity.Y = NPC.velocity.Y + turnSpeed * 0.75f;
                    if (NPC.velocity.Y > num58)
                    {
                        NPC.velocity.Y = num58;
                    }
                    if (System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y) < num58 * 0.4)
                    {
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X - num59 * 1.1f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X + num59 * 1.1f;
                        }
                    }
                    else if (NPC.velocity.Y == num58)
                    {
                        if (NPC.velocity.X < num61)
                        {
                            NPC.velocity.X = NPC.velocity.X + num59;
                        }
                        else if (NPC.velocity.X > num61)
                        {
                            NPC.velocity.X = NPC.velocity.X - num59;
                        }
                    }
                    else if (NPC.velocity.Y > 4f)
                    {
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num59 * 0.9f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num59 * 0.9f;
                        }
                    }
                }
                else
                {
                    if (!flies && NPC.behindTiles && NPC.soundDelay == 0)
                    {
                        float num65 = num63 / 40f;
                        if (num65 < 10f)
                        {
                            num65 = 10f;
                        }
                        if (num65 > 20f)
                        {
                            num65 = 20f;
                        }
                        NPC.soundDelay = (int)num65;
                        SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
                    }
                    num63 = (float)System.Math.Sqrt(num61 * num61 + num62 * num62);
                    float num66 = System.Math.Abs(num61);
                    float num67 = System.Math.Abs(num62);
                    float num68 = num58 / num63;
                    num61 *= num68;
                    num62 *= num68;
                    bool flag21 = false;
                    if (!flag21)
                    {
                        if (NPC.velocity.X > 0f && num61 > 0f || NPC.velocity.X < 0f && num61 < 0f || NPC.velocity.Y > 0f && num62 > 0f || NPC.velocity.Y < 0f && num62 < 0f)
                        {
                            if (NPC.velocity.X < num61)
                            {
                                NPC.velocity.X = NPC.velocity.X + num59;
                            }
                            else
                            {
                                if (NPC.velocity.X > num61)
                                {
                                    NPC.velocity.X = NPC.velocity.X - num59;
                                }
                            }
                            if (NPC.velocity.Y < num62)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + num59;
                            }
                            else
                            {
                                if (NPC.velocity.Y > num62)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - num59;
                                }
                            }
                            if (System.Math.Abs(num62) < num58 * 0.2 && (NPC.velocity.X > 0f && num61 < 0f || NPC.velocity.X < 0f && num61 > 0f))
                            {
                                if (NPC.velocity.Y > 0f)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y + num59 * 2f;
                                }
                                else
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - num59 * 2f;
                                }
                            }
                            if (System.Math.Abs(num61) < num58 * 0.2 && (NPC.velocity.Y > 0f && num62 < 0f || NPC.velocity.Y < 0f && num62 > 0f))
                            {
                                if (NPC.velocity.X > 0f)
                                {
                                    NPC.velocity.X = NPC.velocity.X + num59 * 2f;
                                }
                                else
                                {
                                    NPC.velocity.X = NPC.velocity.X - num59 * 2f;
                                }
                            }
                        }
                        else
                        {
                            if (num66 > num67)
                            {
                                if (NPC.velocity.X < num61)
                                {
                                    NPC.velocity.X = NPC.velocity.X + num59 * 1.1f;
                                }
                                else if (NPC.velocity.X > num61)
                                {
                                    NPC.velocity.X = NPC.velocity.X - num59 * 1.1f;
                                }
                                if (System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y) < num58 * 0.5)
                                {
                                    if (NPC.velocity.Y > 0f)
                                    {
                                        NPC.velocity.Y = NPC.velocity.Y + num59;
                                    }
                                    else
                                    {
                                        NPC.velocity.Y = NPC.velocity.Y - num59;
                                    }
                                }
                            }
                            else
                            {
                                if (NPC.velocity.Y < num62)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y + num59 * 1.1f;
                                }
                                else if (NPC.velocity.Y > num62)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - num59 * 1.1f;
                                }
                                if (System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y) < num58 * 0.5)
                                {
                                    if (NPC.velocity.X > 0f)
                                    {
                                        NPC.velocity.X = NPC.velocity.X + num59;
                                    }
                                    else
                                    {
                                        NPC.velocity.X = NPC.velocity.X - num59;
                                    }
                                }
                            }
                        }
                    }
                }
                NPC.rotation = (float)System.Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
                if (flies)
                {
                    if (NPC.localAI[0] != 1f)
                    {
                        NPC.netUpdate = true;
                    }
                    NPC.localAI[0] = 1f;
                }
                else
                {
                    if (NPC.localAI[0] != 0f)
                    {
                        NPC.netUpdate = true;
                    }
                    NPC.localAI[0] = 0f;
                }
                if ((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f || NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f || NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f || NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f) && !NPC.justHit)
                {
                    NPC.netUpdate = true;
                    return;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Gold, hit.HitDirection, -1f, 0, default, 1f);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 10; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Gold, hit.HitDirection, -1f, 0, default, 1f);
                }
            }
        }

        public override void OnKill()
        {
            if(Main.expertMode)
                DropItem(NPC, ModContent.ItemType<CovetiteCrystal>(), 1 + Main.rand.Next(1), 5, 40, true);
            else
                DropItem(NPC, ModContent.ItemType<CovetiteCrystal>(), 1, 5, 30, true);

            NPC.DropLoot(ModContent.ItemType<GreedKey>(), .05f);
        }

        /*
         * Drops an item from a codable, and returns the item's whoAmI. Mostly convenience for mp support.
         * If it drops more then one item it will return the last item dropped's whoAmI.
         * 
         * amt : the amount of the item to drop.
         * maxStack : The max stack count per item. (only applies if clusterItem == true)
         * chance : 0-1. The percent chance of the item drop. If projectile is not 100 and the item does not drop, projectile method returns -1.
         * clusterItem : If true, it will stick the drops into stacks that fit to the item's maxStack value. If false it drops them as individual items.
         */
        public static int DropItem(Entity codable, int type, int amt, int maxStack, float chance, bool clusterItem = false, bool sync = false)
        {
            int itemID = -1;
            if ((sync || Main.netMode != NetmodeID.MultiplayerClient) && (float)Main.rand.NextDouble() <= chance)
            {
                if (clusterItem)
                {
                    int stackCount = 0;
                    int stackCount2 = 0;
                    while (stackCount != amt)
                    {
                        stackCount++; stackCount2++;
                        if (stackCount == amt || stackCount2 == maxStack)
                        {
                            itemID = Item.NewItem(codable.GetSource_Loot(), (int)codable.position.X, (int)codable.position.Y, codable.width, codable.height, type, stackCount2, false, 0);
                            if (sync) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemID, 0f, 0f, 0f, 0, 0, 0);
                            stackCount2 = 0;
                        }
                    }
                }
                else
                {
                    int count = 0;
                    while (count < amt)
                    {
                        count++;
                        itemID = Item.NewItem(codable.GetSource_Loot(), (int)codable.position.X, (int)codable.position.Y, codable.width, codable.height, type, 1, false, 0);
                        if (sync) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemID, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            return itemID;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 1, NPC.frame, drawColor, true);
            return false;
        }
    }
}


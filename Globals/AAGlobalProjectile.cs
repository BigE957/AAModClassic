using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._Content.Mire.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Void.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Desert.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Hell.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Snow.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.RedMushroom.World.Tiles;
using AAModClassic._Content.Void.World.Tiles;

namespace AAModClassic.Globals
{
    public class AAGlobalProjectile : GlobalProjectile
    {

        public override bool InstancePerEntity => true;

        public static int CountProjectiles(int type)
        {
            int num = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                {
                    num++;
                }
            }

            return num;
        }

        public static bool AnyProjectiles(int type)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static float GetSyncedItemAnimation(Projectile projectile, Player player)
        {
            float itemAnimation = player.itemAnimation;

            if (Main.netMode != NetmodeID.SinglePlayer && Main.myPlayer == projectile.owner)
            {
                if (projectile.ai[1] != itemAnimation)
                {
                    projectile.ai[1] = itemAnimation;
                    projectile.netUpdate = true;
                }
            }

            if (Main.netMode == NetmodeID.SinglePlayer || Main.myPlayer == projectile.owner)
                return itemAnimation;

            if (projectile.ai[1] > 0f)
                projectile.localAI[1] = 1f;

            if (projectile.localAI[1] == 1f)
                return projectile.ai[1];

            return Math.Max(1f, player.itemAnimationMax);
        }

        public override void PostAI(Projectile projectile)
        {
            if (isReflecting && projectile.hostile && !projectile.friendly)
            {
                oldvelocity = projectile.velocity;
                projectile.velocity = reflectvelocity;
                projectile.rotation += projectile.velocity.ToRotation() - oldvelocity.ToRotation();
            }
            if (!projectile.minion && projectile.type > ProjectileID.None && !projectile.CountsAsClass(DamageClass.Melee) && !projectile.CountsAsClass(DamageClass.Magic) && !projectile.CountsAsClass(DamageClass.Ranged))
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (Main.projectile[j].active && Main.projectile[j].sentry && Main.projectile[j].type + 1 == projectile.type)
                    {
                        projectile.minion = true;
                        break;
                    }
                }
            }
            if ((projectile.minion || projectile.sentry) && !ProjectileID.Sets.StardustDragon[projectile.type] && !LongMinion)
			{
				if (setDefMinionDamage)
				{
					DefMinionDamageMultiply = Main.player[projectile.owner].GetDamage(DamageClass.Summon).Multiplicative;
					DefMinionDamage = (int)(projectile.damage / DefMinionDamageMultiply);
					setDefMinionDamage = false;
				}
				if (Main.player[projectile.owner].GetDamage(DamageClass.Summon).Flat != DefMinionDamageMultiply)
				{
					int damage = (int)(Main.player[projectile.owner].GetDamage(DamageClass.Summon)).ApplyTo(DefMinionDamage);
                    if(damage <= 0) damage = 1;
					projectile.damage = damage;
				}
			}
            if (projectile.type == ProjectileID.PureSpray)
            {
                Convert((int)(projectile.position.X + (projectile.width / 2)) / 16, (int)(projectile.position.Y + (projectile.height / 2)) / 16);
            }

            if (projectile.bobber)
            {
                if(Main.player[projectile.owner].GetModPlayer<AAPlayer>().StripeManFish)
                {
                    Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    Rectangle value = new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height);
                    if(projectile.ai[0] != 1 && Main.rand.NextBool(2000))
                    {
                        for(int i = 0; i < 200; i++)
                        {
                            Rectangle npcrec = new Rectangle((int)Main.npc[i].position.X, (int)Main.npc[i].position.Y, Main.npc[i].width, Main.npc[i].height);
                            if(Main.npc[i].active && Main.npc[i].life != 0 && !Main.npc[i].boss && rectangle.Intersects(npcrec))
                            {
                                Main.npc[i].NPCLoot();
                                projectile.ai[0] = 1;
                                Main.npc[i].active = true;
                                break;
                            }
                        }
                    }
                    if(projectile.ai[0] == 1 && projectile.ai[1] == 0)
                    {
                        for(int i = 0; i < 400; i++)
                        {
                            Rectangle itemrec = new Rectangle((int)Main.item[i].position.X, (int)Main.item[i].position.Y, Main.item[i].width, Main.item[i].height);
                            if(Main.item[i].active && rectangle.Intersects(itemrec))
                            {
                                projectile.ai[1] = Main.item[i].type;
                                Main.item[i].stack --;
                                if(Main.item[i].stack == 0) Main.item[i].active = false;
                                candoublefish = false;
                                break;
                            }
                        }
                    }
                    if(candoublefish && rectangle.Intersects(value) && projectile.ai[1] > 0f)
                    {
                        Item item = new Item();
                        int itemtype = 0;
                        int projectileX = (int)(projectile.Center.X / 16f);
			            int projectileY = (int)(projectile.Center.Y / 16f);
                        int WorldHeightType;
                        if (projectileY < Main.worldSurface * 0.5)
                        {
                            WorldHeightType = 0;
                        }
                        else if (projectileY < Main.worldSurface)
                        {
                            WorldHeightType = 1;
                        }
                        else if (projectileY < Main.rockLayer)
                        {
                            WorldHeightType = 2;
                        }
                        else if (projectileY < Main.maxTilesY - 300)
                        {
                            WorldHeightType = 3;
                        }
                        else
                        {
                            WorldHeightType = 4;
                        }
                        if (Main.rand.Next(100) < 20f)
                        {
                            if (Main.rand.NextBool(3))
                            {
                                itemtype = 2336;
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].ZoneCorrupt)
                            {
                                itemtype = 3203;
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].ZoneCrimson)
                            {
                                itemtype = 3204;
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].ZoneHallow)
                            {
                                itemtype = 3207;
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].ZoneDungeon)
                            {
                                itemtype = 3205;
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].ZoneJungle)
                            {
                                itemtype = 3208;
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].ZoneSnow)
                            {
                                itemtype = ModContent.ItemType<IceCrate>();
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].ZoneDesert)
                            {
                                itemtype = ModContent.ItemType<DesertCrate>();
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].GetModPlayer<AAPlayer>().ZoneInferno)
                            {
                                itemtype = ModContent.ItemType<InfernoCrate>();
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].GetModPlayer<AAPlayer>().ZoneMire)
                            {
                                itemtype = ModContent.ItemType<MireCrate>();
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].GetModPlayer<AAPlayer>().ZoneVoid)
                            {
                                itemtype = ModContent.ItemType<VoidCrate>();
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].GetModPlayer<AAPlayer>().ZoneHoard)
                            {
                                itemtype = ItemID.GoldenCrate;
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].ZoneUnderworldHeight)
                            {
                                itemtype = ModContent.ItemType<HellCrate>();
                            }
                            else if (Main.rand.NextBool(3) && WorldHeightType == 0)
                            {
                                itemtype = 3206;
                            }
                            else if (Main.rand.NextBool(2))
                            {
                                itemtype = 2335;
                            }
                            else
                            {
                                itemtype = 2334;
                            }
                        }
                        int liquidtype = 0;
                        int tileX = 0;
                        int tileY = 0;
                        while(tileX < 20 && tileY < 20)
                        {
                            if (Main.tile[projectileX - 10 + tileX, projectileY - 20 + tileY].LiquidType == LiquidID.Lava)
                            {
                                liquidtype = 1;
                            }
                            else if (Main.tile[projectileX - 10 + tileX, projectileY - 20 + tileY].LiquidType == LiquidID.Honey)
                            {
                                liquidtype = 2;
                            }
                            tileY ++;
                            if(tileY >= 20)
                            {
                                tileX ++;
                                tileY = 0;
                            }
                        }

                        if (itemtype == 0)
                        {
                            Player player = Main.player[projectile.owner];

                            // Manually create PlayerFishingConditions to match original int.MaxValue bait power
                            PlayerFishingConditions conditions = player.GetFishingConditions();

                            // Create FishingAttempt
                            FishingAttempt attempt = new FishingAttempt
                            {
                                playerFishingConditions = conditions,
                                X = projectileX,
                                Y = projectileY,
                                bobberType = projectile.type,
                                common = true,
                                uncommon = true,
                                rare = true,
                                veryrare = true,
                                legendary = true,
                                crate = true,
                                inLava = (liquidtype == 1),
                                inHoney = (liquidtype == 2),
                                waterTilesCount = 1000,
                                waterNeededToFish = 0,
                                waterQuality = 0,
                                chumsInWater = 0,
                                fishingLevel = 0,
                                CanFishInLava = (liquidtype == 1),
                                atmo = (WorldHeightType == 0) ? 0.25f : 1f,
                                questFish = 0,
                                heightLevel = WorldHeightType
                            };

                            int itemDrop = 0;
                            int enemySpawn = 0;
                            AdvancedPopupRequest sonar = default;
                            Vector2 sonarPosition = default;

                            int maxAttempts = 100;
                            int attempts = 0;
                            while (itemDrop == 0 && attempts < maxAttempts)
                            {
                                PlayerLoader.CatchFish(player, attempt, ref itemDrop, ref enemySpawn,
                                                       ref sonar, ref sonarPosition);
                                enemySpawn = 0;
                                attempts++;
                            }

                            if (itemDrop > 0)
                            {
                                itemtype = itemDrop;
                            }
                            else
                            {
                                itemtype = ItemID.TinCan;
                            }
                        }

                        item.SetDefaults(itemtype, false);
                        ItemLoader.CaughtFishStack(item);
						item.newAndShiny = true;
                        Item CreatItem = Main.player[projectile.owner].GetItem(projectile.owner, item, new());
                        if (CreatItem.stack > 0)
                        {
                            int number = Item.NewItem(projectile.GetSource_FromThis(), (int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, item.type, 1, false, 0, true, false);
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
                            }
                        }
                        else
                        {
                            item.position.X = projectile.Center.X - item.width / 2;
                            item.position.Y = projectile.Center.Y - item.height / 2;
                            item.active = true;
                            PopupText.NewText(PopupTextContext.RegularItemPickup, item, 0, false, false);
                        }
                    }
                }
            }

            base.PostAI(projectile);
        }


        public static void Convert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < 6)
                    {
                        if (Main.tile[k, l].TileType == ModContent.TileType<InfernoGrass_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<MireGrass_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<Mycelium_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<DoomGrass_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.Grass;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].TileType == ModContent.TileType<Torchstone_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<Depthstone_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<DoomstoneB_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.Stone;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].TileType == ModContent.TileType<Torchsand_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<Depthsand_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.Sand;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].TileType == ModContent.TileType<TorchsandHardened_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<DepthsandHardened_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.HardenedSand;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].TileType == ModContent.TileType<Torchsandstone_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<Depthsandstone_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.Sandstone;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (Main.tile[k, l].TileType == ModContent.TileType<Torchice_Tile>() || Main.tile[k, l].TileType == ModContent.TileType<IndigoIce_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.IceBlock;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                    }
                }
            }
        }


        public Vector2 reflectvelocity = Vector2.Zero;

        private Vector2 oldvelocity = Vector2.Zero;

        public bool candoublefish = true;

        public bool isReflecting = false;

        private bool setDefMinionDamage = true;

        public bool LongMinion = false;

        public float DefMinionDamageMultiply = 1f;

		public int DefMinionDamage;
    }
}

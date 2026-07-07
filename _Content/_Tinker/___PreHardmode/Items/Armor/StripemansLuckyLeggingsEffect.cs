using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Desert.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Hell.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Mire.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic._Content.Snow.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Void.___PreHardmode.Items.Consumables;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    public class StripemansLuckyLeggingsEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<StripemansLuckyLeggingsPlayer>().effect = true;
        }
    }

    public class StripemansLuckyLeggingsPlayer : EquipmentEffectPlayer
    {

    }

    public class StripemansLuckyLeggingsProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool candoublefish = true;

        public override void PostAI(Projectile projectile)
        {
            if (projectile.bobber)
            {
                if (Main.player[projectile.owner].GetModPlayer<StripemansLuckyLeggingsPlayer>().effect)
                {
                    Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    Rectangle value = new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height);

                    bool condition = false;
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && (projectile.ai[0] != 1))
                        condition = true;
                    else if (projectile.ai[0] != 1 && Main.rand.NextBool(2000))
                        condition = true;

                    if (condition)
                    {
                        for (int i = 0; i < 200; i++)
                        {
                            Rectangle npcrec = new Rectangle((int)Main.npc[i].position.X, (int)Main.npc[i].position.Y, Main.npc[i].width, Main.npc[i].height);
                            if (Main.npc[i].active && Main.npc[i].life != 0 && !Main.npc[i].boss && rectangle.Intersects(npcrec))
                            {
                                if ((WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && Main.rand.NextBool(25)) || (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial)))
                                    Main.npc[i].NPCLoot(); //TODO: can we just pull from the loot and make this not count towards npc kills i feel like thats gonna cause issues
                                projectile.ai[0] = 1;
                                Main.npc[i].active = true;
                                break;
                            }
                        }
                    }

                    if (projectile.ai[0] == 1 && projectile.ai[1] == 0)
                    {
                        for (int i = 0; i < 400; i++)
                        {
                            Rectangle itemrec = new Rectangle((int)Main.item[i].position.X, (int)Main.item[i].position.Y, Main.item[i].width, Main.item[i].height);
                            if (Main.item[i].active && rectangle.Intersects(itemrec))
                            {
                                projectile.ai[1] = Main.item[i].type;
                                Main.item[i].stack--;
                                if (Main.item[i].stack == 0) Main.item[i].active = false;
                                candoublefish = false;
                                break;
                            }
                        }
                    }
                    if (candoublefish && rectangle.Intersects(value) && projectile.ai[1] > 0f)
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
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].GetModPlayer<ZAAPlayer>().ZoneInferno)
                            {
                                itemtype = ModContent.ItemType<InfernoCrate>();
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].GetModPlayer<ZAAPlayer>().ZoneMire)
                            {
                                itemtype = ModContent.ItemType<MireCrate>();
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].GetModPlayer<ZAAPlayer>().ZoneVoid)
                            {
                                itemtype = ModContent.ItemType<VoidCrate>();
                            }
                            else if (Main.rand.NextBool(3) && Main.player[projectile.owner].GetModPlayer<ZAAPlayer>().ZoneHoard)
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
                        while (tileX < 20 && tileY < 20)
                        {
                            if (Main.tile[projectileX - 10 + tileX, projectileY - 20 + tileY].LiquidType == LiquidID.Lava)
                            {
                                liquidtype = 1;
                            }
                            else if (Main.tile[projectileX - 10 + tileX, projectileY - 20 + tileY].LiquidType == LiquidID.Honey)
                            {
                                liquidtype = 2;
                            }
                            tileY++;
                            if (tileY >= 20)
                            {
                                tileX++;
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
        }
    }
}
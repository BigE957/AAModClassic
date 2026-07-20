using AAModClassic._Content._Misc.___PreHardmode.Items.Consumables.LuckyPotions;
using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content._Tinker.___PreHardmode.Items.Armor
{
    public class StripemansLuckyChestplateEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<StripemansLuckyChestplatePlayer>().effect = true;
        }
    }

    public class StripemansLuckyChestplatePlayer : EquipmentEffectPlayer
    {

    }

    public class StripemansLuckyChestplateNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (!player.GetModPlayer<ZAAPlayer>().luckycalm && player.GetModPlayer<StripemansLuckyChestplatePlayer>().effect && !player.calmed && player.active && !player.dead && player.nearbyActiveNPCs < maxSpawns && Main.rand.NextDouble() * (spawnRate / 1.333f) < 1)
            {
                VanillaNPCSpawn(player);
            }
        }

        public static void VanillaNPCSpawn(Player player)
        {
            int spawnRangeXMin = (int)(player.position.X / 16f) - (int)(NPC.sWidth / 16 * 0.7);
            int spawnRangeXMax = (int)(player.position.X / 16f) + (int)(NPC.sWidth / 16 * 0.7);
            int spawnRangeYMin = (int)(player.position.Y / 16f) - (int)(NPC.sHeight / 16 * 0.7);
            int spawnRangeYMax = (int)(player.position.Y / 16f) + (int)(NPC.sHeight / 16 * 0.7);

            int x = Main.rand.Next(spawnRangeXMin, spawnRangeXMax);
            int y = Main.rand.Next(spawnRangeYMin, spawnRangeYMax);

            int npcid = 0;

            if (!Main.tile[x, y].HasTile)
            {
                if (Sandstorm.Happening && player.ZoneSandstorm && TileID.Sets.Conversion.Sand[Main.tile[x, y].TileType] && NPC.Spawning_SandstoneCheck(x, y))
                {
                    if (Main.hardMode && Main.rand.NextBool(15))
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.SandElemental, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (player.ZoneDungeon && NPC.downedPlantBoss)
                {
                    if (Main.rand.NextBool(15))
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.BoneLee, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.NextBool(10))
                    {
                        int Skeletontype = 0;
                        switch (Main.rand.Next(3))
                        {
                            case 0: Skeletontype = 291; break;
                            case 1: Skeletontype = 292; break;
                            case 3: Skeletontype = 293; break;
                        }
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, Skeletontype, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.NextBool(15))
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Paladin, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (y <= Main.worldSurface && Main.dayTime && Main.eclipse)
                {
                    bool flag = false;
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        flag = true;
                    }
                    if (flag && Main.rand.NextBool(40))
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Mothron, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (y <= Main.worldSurface)
                {
                    if (player.ZoneSnow && Main.hardMode && Main.cloudAlpha > 0f && Main.rand.NextBool(15))
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.IceGolem, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (player.ZoneHallow && Main.hardMode && Main.rand.NextBool(30))
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.RainbowSlime, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (y <= Main.worldSurface / 2 && NPC.AnyDanger() && Main.hardMode && NPC.downedGolemBoss && Main.rand.NextBool(100) && !NPC.AnyNPCs(399))
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.MartianProbe, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.hardMode && Main.rand.NextBool(25) && Main.bloodMoon)
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Clown, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.NextBool(100) && Main.bloodMoon)
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.TheGroom, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.NextBool(100) && Main.bloodMoon)
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.TheBride, 0, 0f, 0f, 0f, 0f, 255);
                    }

                    if (Main.dayTime)
                    {
                        if (Main.rand.NextBool(50))
                        {
                            npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.BlueSlime, 0, 0f, 0f, 0f, 0f, 255);
                            Main.npc[npcid].SetDefaults(-4);
                        }
                    }
                }
                else if (Main.hardMode && y > Main.worldSurface && Main.rand.NextBool(40))
                {
                    if (Main.rand.NextBool(2) && player.ZoneCorrupt)
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.BigMimicCorruption, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    else if (Main.rand.NextBool(2) && player.ZoneCrimson)
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.BigMimicCrimson, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    else if (Main.rand.NextBool(2) && player.ZoneHallow)
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.BigMimicHallow, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    else
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Mimic, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (Main.hardMode && Main.tile[x, y - 1].WallType == WallID.DirtUnsafe && Main.rand.NextBool(10))
                {
                    npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Mimic, 0, 0f, 0f, 0f, 0f, 255);
                }
                else if (Main.tile[x, y].TileType == TileID.JungleGrass && Main.rand.NextBool(100) && !Main.dayTime)
                {
                    npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.DoctorBones, 0, 0f, 0f, 0f, 0f, 255);
                }
                else if (Main.tile[x, y].TileType == TileID.JungleGrass && Main.hardMode && Main.rand.NextBool(45) && !Main.dayTime)
                {
                    npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Moth, 0, 0f, 0f, 0f, 0f, 255);
                }
                else if (y > Main.maxTilesY - 190)
                {
                    if (Main.hardMode && !NPC.savedTaxCollector && Main.rand.NextBool(10) && !NPC.AnyNPCs(534))
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.DemonTaxCollector, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (y <= Main.maxTilesY - 190 && y > Main.rockLayer)
                {
                    if (Main.rand.NextBool(50))
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.LostGirl, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (y > (Main.rockLayer + Main.maxTilesY) / 2.0 && Main.rand.NextBool(50))
                    {
                        npcid = NPC.NewNPC(Entity.GetSource_NaturalSpawn(), x * 16 + 8, y * 16, NPCID.Tim, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
            }

            if (Main.netMode == NetmodeID.Server && npcid < 200)
            {
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcid, 0f, 0f, 0f, 0, 0, 0);
                return;
            }
        }
    }

    public class StripemansLuckyChestplateTile : GlobalTile
    {
        public override void Drop(int i, int j, int type)
        {
            if (type == TileID.Pots)
            {
                if (Main.LocalPlayer.GetModPlayer<StripemansLuckyChestplatePlayer>().effect)
                {
                    PotsDropMethod(i, j);
                }
            }
        }

        public override void RandomUpdate(int i, int j, int type)
        {
            if (Main.LocalPlayer.GetModPlayer<StripemansLuckyChestplatePlayer>().effect)
            {
                if (Main.rand.NextBool(800) && j >= GenVars.worldSurfaceLow)
                {
                    if (Main.tile[i, j + 1].HasTile && Main.tileSolid[Main.tile[i, j].TileType] && Main.tile[i, j - 1].LiquidType != LiquidID.Lava)
                    {
                        int style = WorldGen.genRand.Next(0, 4);
                        int tiletype = 0;
                        if (j < Main.maxTilesY - 5)
                        {
                            tiletype = Main.tile[i, j + 1].TileType;
                        }
                        if (tiletype == 147 || tiletype == 161 || tiletype == 162)
                        {
                            style = WorldGen.genRand.Next(4, 7);
                        }
                        if (tiletype == 60)
                        {
                            style = WorldGen.genRand.Next(7, 10);
                        }
                        if (Main.wallDungeon[Main.tile[i, j].WallType])
                        {
                            style = WorldGen.genRand.Next(10, 13);
                        }
                        if (tiletype == 41 || tiletype == 43 || tiletype == 44)
                        {
                            style = WorldGen.genRand.Next(10, 13);
                        }
                        if (tiletype == 22 || tiletype == 23 || tiletype == 25)
                        {
                            style = WorldGen.genRand.Next(16, 19);
                        }
                        if (tiletype == 199 || tiletype == 203 || tiletype == 204 || tiletype == 200)
                        {
                            style = WorldGen.genRand.Next(22, 25);
                        }
                        if (tiletype == 367)
                        {
                            style = WorldGen.genRand.Next(31, 34);
                        }
                        if (tiletype == 226)
                        {
                            style = WorldGen.genRand.Next(28, 31);
                        }
                        if (j > Main.maxTilesY - 200)
                        {
                            style = WorldGen.genRand.Next(13, 16);
                        }
                        if (WorldGen.PlacePot(i, j, 28, style))
                        {
                            NetMessage.SendObjectPlacement(-1, i, j, 28, 0, 0, -1, -1);
                        }
                    }
                }

            }
        }

        public static void PotsDropMethod(int i, int j)
        {
            int itemcreat = 0;
            if (WorldGen.genRand.NextBool(30) || Main.rand.NextBool(30) && Main.expertMode)
            {
                if (WorldGen.genRand.NextBool(20))
                {
                    int rand = WorldGen.genRand.Next(100);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 678, 1, false, 0, false, false);
                    }
                    else
                    {
                        int rand2 = WorldGen.genRand.Next(3);
                        if (rand2 == 0)
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2352, 1, false, 0, false, false);
                        }
                        if (rand2 == 1)
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2353, 1, false, 0, false, false);
                        }
                        if (rand2 == 2)
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2756, 1, false, 0, false, false);
                        }
                    }
                }
                if (Main.rand.NextBool(200))
                {
                    int k = AALuckyConfig.LuckyPotion.Keys.Count;
                    foreach (int itempotion in AALuckyConfig.LuckyPotion.Keys)
                    {
                        if (Main.rand.Next(k) == 0)
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, itempotion, 1, false, 0, false, false);
                            break;
                        }
                        k -= 1;
                    }
                }
                else if (Main.tile[i, j].LiquidAmount > 0)
                {
                    int rand = WorldGen.genRand.Next(3);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2354, 1, false, 0, false, false);
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2355, 1, false, 0, false, false);
                    }
                    if (rand >= 2)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2356, 1, false, 0, false, false);
                    }
                }
                else if (j < Main.worldSurface)
                {
                    int rand = WorldGen.genRand.Next(12);
                    if (rand == 0)
                    {
                        if (Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<LuckyIronskinPotion>();
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 292, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 298, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 299, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        if (Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<LuckySwiftnessPotion>();
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 290, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2322, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        if (Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<LuckyCalmingPotion>();
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2324, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 6)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2325, 1, false, 0, false, false);
                    }
                    if (rand == 7 || rand == 8)
                    {
                        if (Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<LuckyWrathPotion>();
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2349, 1, false, 0, false, false);
                        }
                    }
                    if (rand >= 9)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2350, 1, false, 0, false, false);
                    }
                }
                else if (j < Main.rockLayer)
                {
                    if (Main.LocalPlayer.ZoneJungle)
                    {
                        int rand2 = WorldGen.genRand.Next(3);
                        if (rand2 == 0)
                        {
                            if (Main.rand.NextBool(100))
                            {
                                int rarepotion = ModContent.ItemType<LuckySummoningPotion>();
                                itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                            }
                            else
                            {
                                itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2328, 1, false, 0, false, false);
                            }
                        }
                        else
                        {
                            if (Main.rand.NextBool(100))
                            {
                                int rarepotion = ModContent.ItemType<LuckyThornsPotion>();
                                itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                            }
                            else
                            {
                                itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 301, 1, false, 0, false, false);
                            }
                        }
                    }
                    else if (Main.LocalPlayer.ZoneSnow)
                    {
                        if (WorldGen.genRand.NextBool(2))
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2359, 1, false, 0, false, false);
                        }
                    }
                    int rand = WorldGen.genRand.Next(12);
                    if (rand == 0)
                    {
                        if (Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<LuckyRegenerationPotion>();
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 289, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 298, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 299, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        if (Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<LuckySwiftnessPotion>();
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 290, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 303, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 291, 1, false, 0, false, false);
                    }
                    if (rand == 6)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 304, 1, false, 0, false, false);
                    }
                    if (rand == 7)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2322, 1, false, 0, false, false);
                    }
                    if (rand == 8)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2329, 1, false, 0, false, false);
                    }
                    if (rand == 9)
                    {
                        if (Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<LuckyEndurancePotion>();
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2346, 1, false, 0, false, false);
                        }
                    }
                    if (rand >= 10)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2350, 1, false, 0, false, false);
                    }
                }
                else if (j < Main.maxTilesY - 200)
                {
                    int rand = WorldGen.genRand.Next(15);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 296, 1, false, 0, false, false);
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 295, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 299, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 302, 1, false, 0, false, false);
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 303, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 305, 1, false, 0, false, false);
                    }
                    if (rand == 6)
                    {
                        if (Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<LuckyThornsPotion>();
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 301, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 7)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 302, 1, false, 0, false, false);
                    }
                    if (rand == 8)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 297, 1, false, 0, false, false);
                    }
                    if (rand == 9)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 304, 1, false, 0, false, false);
                    }
                    if (rand == 10)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2322, 1, false, 0, false, false);
                    }
                    if (rand == 11)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2323, 1, false, 0, false, false);
                    }
                    if (rand == 12)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2327, 1, false, 0, false, false);
                    }
                    if (rand == 13)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2329, 1, false, 0, false, false);
                    }
                    if (rand == 14)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2350, 1, false, 0, false, false);
                    }
                }
                else
                {
                    int rand = WorldGen.genRand.Next(16);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 296, 1, false, 0, false, false);
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 295, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 293, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 288, 1, false, 0, false, false);
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 294, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 297, 1, false, 0, false, false);
                    }
                    if (rand == 6)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 304, 1, false, 0, false, false);
                    }
                    if (rand == 7)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 305, 1, false, 0, false, false);
                    }
                    if (rand == 8)
                    {
                        if (Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<LuckyThornsPotion>();
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 301, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 9)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 302, 1, false, 0, false, false);
                    }
                    if (rand == 10)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 288, 1, false, 0, false, false);
                    }
                    if (rand == 11)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 300, 1, false, 0, false, false);
                    }
                    if (rand == 12)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2323, 1, false, 0, false, false);
                    }
                    if (rand == 13)
                    {
                        itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2326, 1, false, 0, false, false);
                    }
                    if (rand == 14)
                    {
                        if (Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<LuckyRagePotion>();
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2347, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 15)
                    {
                        if (Main.rand.NextBool(5))
                        {
                            if (Main.rand.NextBool(100))
                            {
                                int rarepotion = ModContent.ItemType<LuckyLifeforcePotion>();
                                itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                            }
                            else
                            {
                                itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2345, 1, false, 0, false, false);
                            }
                        }
                        else if (Main.rand.NextBool(2))
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2326, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2323, 1, false, 0, false, false);
                        }
                    }
                }
            }
            if (Main.netMode == NetmodeID.MultiplayerClient && itemcreat > 0)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
            }
        }
    }
}
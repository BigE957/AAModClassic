using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void.World.Tiles;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content._Tinker.___PreHardmode.Items.Armor
{
    public class StripemansLuckyHelmetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<StripemansLuckyHelmetPlayer>().effect = true;
        }
    }

    public class StripemansLuckyHelmetPlayer : EquipmentEffectPlayer
    {

    }

    public class StripemansLuckyHelmetItem : GlobalItem
    {
        public override bool? UseItem(Item item, Player player)
        {
            if (player.GetModPlayer<StripemansLuckyHelmetPlayer>().effect)
            {
                int tileTargetX = (int)((Main.mouseX + Main.screenPosition.X) / 16f);
                int tileTargetY = (int)((Main.mouseY + Main.screenPosition.Y) / 16f);
                if (Main.tile[tileTargetX, tileTargetY].HasTile && Main.tile[tileTargetX, tileTargetY].TileType == TileID.Extractinator && item.createTile > TileID.Dirt && (Main.tileSand[item.createTile] || TileID.Sets.Conversion.Sand[item.createTile]))
                {
                    bool flag = player.position.X / 16f - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost - player.blockRange <= Player.tileTargetX && (player.position.X + player.width) / 16f + Player.tileRangeX + player.inventory[player.selectedItem].tileBoost - 1f + player.blockRange >= Player.tileTargetX && player.position.Y / 16f - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost - player.blockRange <= Player.tileTargetY && (player.position.Y + player.height) / 16f + Player.tileRangeY + player.inventory[player.selectedItem].tileBoost - 2f + player.blockRange >= Player.tileTargetY;
                    if (flag && player.itemTime == 0 && player.itemAnimation > 0 && player.controlUseItem)
                    {
                        player.itemTime = (int)(player.inventory[player.selectedItem].useTime / PlayerLoader.UseAnimationMultiplier(player, player.inventory[player.selectedItem]));
                        SoundEngine.PlaySound(SoundID.Grab);
                        ExtractinatorUse2(item.type);
                        for (int i = 0; i < 58; i++)
                        {
                            if (player.inventory[i].type == item.type && player.inventory[i].stack > 0)
                            {
                                player.inventory[i].stack--;
                                if (player.inventory[i].stack <= 0)
                                {
                                    player.inventory[i].SetDefaults(ItemID.None, false);
                                }
                                break;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public override void ExtractinatorUse(int extractType, int extractinatorBlockType, ref int resultType, ref int resultStack)
        {
            int result = 0;
            int stack = 1;

            if (Main.LocalPlayer.GetModPlayer<StripemansLuckyHelmetPlayer>().effect)
            {
                if (extractType == ItemID.DesertFossil || extractType == ItemID.SlushBlock || extractType == ItemID.SiltBlock)
                {
                    if (Main.rand.NextBool(10))
                    {
                        result = 3380;
                        stack += 6;
                    }
                    else if (Main.rand.NextBool(10))
                    {
                        if (Main.rand.NextBool(500))
                        {
                            result = 74;
                            stack += 3;
                        }
                        else if (Main.rand.NextBool(200))
                        {
                            result = 73;
                            stack += 99;
                        }
                        else
                        {
                            result = 72;
                            stack += 99;
                        }
                    }
                    else if (Main.rand.NextBool(100))
                    {
                        result = 1242;
                    }
                    // removed to disallow tierskipping
                    /*
                    else if (Main.rand.NextBool(30))
                    {
                        if(Main.rand.NextBool(2))
                        {
                            result = ModContent.ItemType<DynaskullFossil>();
                            stack += 1;
                            if (Main.rand.NextBool(5))
                            {
                                stack += Main.rand.Next(2);
                            }
                            if (Main.rand.NextBool(10))
                            {
                                stack += Main.rand.Next(3);
                            }
                            if (Main.rand.NextBool(15))
                            {
                                stack += Main.rand.Next(4);
                            }
                        }
                        else
                        {
                            result = ModContent.ItemType<VikingRelic>();
                            stack += 1;
                            if (Main.rand.NextBool(5))
                            {
                                stack += Main.rand.Next(2);
                            }
                            if (Main.rand.NextBool(10))
                            {
                                stack += Main.rand.Next(3);
                            }
                            if (Main.rand.NextBool(15))
                            {
                                stack += Main.rand.Next(4);
                            }
                        }
                    }
                    */
                    else if (Main.rand.NextBool(300))
                    {
                        switch (Main.rand.Next(8))
                        {
                            case 0: result = 12; return;
                            case 1: result = 11; return;
                            case 2: result = 14; return;
                            case 3: result = 13; return;
                            case 4: result = 699; return;
                            case 5: result = 700; return;
                            case 6: result = 701; return;
                            default: result = 702; return;
                        }
                    }
                    else if (Main.rand.NextBool(20))
                    {
                        result = 999;
                        stack += 5;
                        if (Main.rand.NextBool(10))
                        {
                            stack += 5;
                        }
                        if (Main.rand.NextBool(20))
                        {
                            stack += 5;
                        }
                    }
                    else
                    {
                        switch (Main.rand.Next(6))
                        {
                            case 0: result = 181; return;
                            case 1: result = 180; return;
                            case 2: result = 177; return;
                            case 3: result = 179; return;
                            case 4: result = 178; return;
                            default: result = 182; return;
                        }
                    }
                }
            }

            if (result > 0)
            {
                resultType = result;
                resultStack = stack;
            }
        }

        public void ExtractinatorUse2(int extractType)
        {
            int result = 0;
            int stack = 1;
            if (extractType == ItemID.EbonsandBlock)
            {
                if (Main.rand.NextBool(10))
                {
                    result = 56;
                    if (Main.rand.NextBool(5))
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.NextBool(10))
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.NextBool(15))
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }
            else if (extractType == ItemID.CrimsandBlock)
            {
                if (Main.rand.NextBool(10))
                {
                    result = 880;
                    if (Main.rand.NextBool(5))
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.NextBool(10))
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.NextBool(15))
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }
            else if (extractType == ModContent.ItemType<Depthsand>())
            {
                if (Main.rand.NextBool(10))
                {
                    result = ModContent.ItemType<AbyssiumOre>();
                    if (Main.rand.NextBool(5))
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.NextBool(10))
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.NextBool(15))
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }
            else if (extractType == ModContent.ItemType<Torchsand>())
            {
                if (Main.rand.NextBool(10))
                {
                    result = ModContent.ItemType<IncineriteOre>();
                    if (Main.rand.NextBool(5))
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.NextBool(10))
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.NextBool(15))
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }
            else if (extractType == ItemID.PearlsandBlock)
            {
                if (Main.rand.NextBool(10))
                {
                    result = Main.rand.NextBool(2) ? 1104 : 364;

                    if (Main.rand.NextBool(5))
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.NextBool(10))
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.NextBool(15))
                    {
                        stack += Main.rand.Next(4);
                    }
                }
                else if (Main.rand.NextBool(10))
                {
                    result = Main.rand.NextBool(2) ? 1105 : 365;
                    if (Main.rand.NextBool(5))
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.NextBool(10))
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.NextBool(15))
                    {
                        stack += Main.rand.Next(4);
                    }
                }
                else if (Main.rand.NextBool(10))
                {
                    result = Main.rand.NextBool(2) ? 1106 : 366;
                    if (Main.rand.NextBool(5))
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.NextBool(10))
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.NextBool(15))
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }
            if (result == 0)
            {
                if (Main.rand.NextBool(10))
                {
                    result = 3380;
                    if (Main.rand.NextBool(5))
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.NextBool(10))
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.NextBool(15))
                    {
                        stack += Main.rand.Next(4);
                    }
                }
                else if (Main.rand.NextBool(2))
                {
                    if (Main.rand.NextBool(12000))
                    {
                        result = 74;
                        if (Main.rand.NextBool(14))
                        {
                            stack += Main.rand.Next(0, 2);
                        }
                        if (Main.rand.NextBool(14))
                        {
                            stack += Main.rand.Next(0, 2);
                        }
                        if (Main.rand.NextBool(14))
                        {
                            stack += Main.rand.Next(0, 2);
                        }
                    }
                    else if (Main.rand.NextBool(800))
                    {
                        result = 73;
                        if (Main.rand.NextBool(6))
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.NextBool(6))
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.NextBool(6))
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.NextBool(6))
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.NextBool(6))
                        {
                            stack += Main.rand.Next(1, 20);
                        }
                    }
                    else if (Main.rand.NextBool(60))
                    {
                        result = 72;
                        if (Main.rand.NextBool(4))
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.NextBool(4))
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.NextBool(4))
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.NextBool(4))
                        {
                            stack += Main.rand.Next(5, 25);
                        }
                    }
                    else
                    {
                        result = 71;
                        if (Main.rand.NextBool(3))
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.NextBool(3))
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.NextBool(3))
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.NextBool(3))
                        {
                            stack += Main.rand.Next(10, 25);
                        }
                    }
                }
                else if (Main.rand.NextBool(4000))
                {
                    result = 1242;
                }
                else if (Main.rand.NextBool(25))
                {
                    result = Main.rand.Next(6);
                    if (result == 0)
                    {
                        result = 181;
                    }
                    else if (result == 1)
                    {
                        result = 180;
                    }
                    else if (result == 2)
                    {
                        result = 177;
                    }
                    else if (result == 3)
                    {
                        result = 179;
                    }
                    else if (result == 4)
                    {
                        result = 178;
                    }
                    else
                    {
                        result = 182;
                    }
                    if (Main.rand.NextBool(20))
                    {
                        stack += Main.rand.Next(0, 2);
                    }
                    if (Main.rand.NextBool(30))
                    {
                        stack += Main.rand.Next(0, 3);
                    }
                    if (Main.rand.NextBool(40))
                    {
                        stack += Main.rand.Next(0, 4);
                    }
                    if (Main.rand.NextBool(50))
                    {
                        stack += Main.rand.Next(0, 5);
                    }
                    if (Main.rand.NextBool(60))
                    {
                        stack += Main.rand.Next(0, 6);
                    }
                }
                else if (Main.rand.NextBool(50))
                {
                    result = 999;
                    if (Main.rand.NextBool(20))
                    {
                        stack += Main.rand.Next(0, 2);
                    }
                    if (Main.rand.NextBool(30))
                    {
                        stack += Main.rand.Next(0, 3);
                    }
                    if (Main.rand.NextBool(40))
                    {
                        stack += Main.rand.Next(0, 4);
                    }
                    if (Main.rand.NextBool(50))
                    {
                        stack += Main.rand.Next(0, 5);
                    }
                    if (Main.rand.NextBool(60))
                    {
                        stack += Main.rand.Next(0, 6);
                    }
                }
                else if (Main.rand.NextBool(3))
                {
                    if (Main.rand.NextBool(5000))
                    {
                        result = 74;
                        if (Main.rand.NextBool(10))
                        {
                            stack += Main.rand.Next(0, 3);
                        }
                        if (Main.rand.NextBool(10))
                        {
                            stack += Main.rand.Next(0, 3);
                        }
                        if (Main.rand.NextBool(10))
                        {
                            stack += Main.rand.Next(0, 3);
                        }
                        if (Main.rand.NextBool(10))
                        {
                            stack += Main.rand.Next(0, 3);
                        }
                        if (Main.rand.NextBool(10))
                        {
                            stack += Main.rand.Next(0, 3);
                        }
                    }
                    else if (Main.rand.NextBool(400))
                    {
                        result = 73;
                        if (Main.rand.NextBool(5))
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.NextBool(5))
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.NextBool(5))
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.NextBool(5))
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.NextBool(5))
                        {
                            stack += Main.rand.Next(1, 20);
                        }
                    }
                    else if (Main.rand.NextBool(30))
                    {
                        result = 72;
                        if (Main.rand.NextBool(3))
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.NextBool(3))
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.NextBool(3))
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.NextBool(3))
                        {
                            stack += Main.rand.Next(5, 25);
                        }
                    }
                    else
                    {
                        result = 71;
                        if (Main.rand.NextBool(2))
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.NextBool(2))
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.NextBool(2))
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.NextBool(2))
                        {
                            stack += Main.rand.Next(10, 25);
                        }
                    }
                }
                else
                {
                    result = Main.rand.Next(8);
                    if (result == 0)
                    {
                        result = 12;
                    }
                    else if (result == 1)
                    {
                        result = 11;
                    }
                    else if (result == 2)
                    {
                        result = 14;
                    }
                    else if (result == 3)
                    {
                        result = 13;
                    }
                    else if (result == 4)
                    {
                        result = 699;
                    }
                    else if (result == 5)
                    {
                        result = 700;
                    }
                    else if (result == 6)
                    {
                        result = 701;
                    }
                    else
                    {
                        result = 702;
                    }
                    if (Main.rand.NextBool(20))
                    {
                        stack += Main.rand.Next(0, 2);
                    }
                    if (Main.rand.NextBool(30))
                    {
                        stack += Main.rand.Next(0, 3);
                    }
                    if (Main.rand.NextBool(40))
                    {
                        stack += Main.rand.Next(0, 4);
                    }
                    if (Main.rand.NextBool(50))
                    {
                        stack += Main.rand.Next(0, 5);
                    }
                    if (Main.rand.NextBool(60))
                    {
                        stack += Main.rand.Next(0, 6);
                    }
                }
            }
            if (result > 0)
            {
                Vector2 vector = Main.ReverseGravitySupport(Main.MouseScreen, 0f) + Main.screenPosition;
                int number = Item.NewItem(Entity.GetSource_NaturalSpawn(), (int)vector.X, (int)vector.Y, 1, 1, result, stack, false, -1, false, false);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
                }
            }
        }
    }

    public class StripemansLuckyHelmetTile : GlobalTile
    {
        public override void Drop(int i, int j, int type)
        {
            if (Main.LocalPlayer.GetModPlayer<StripemansLuckyHelmetPlayer>().effect)
            {
                if (TileID.Sets.Conversion.Stone[type])
                {
                    int k = DropOreMethod(i, j, type);
                    if (k != 0) Item.NewItem(Entity.GetSource_NaturalSpawn(), i * 16, j * 16, 32, 32, k, 1, false, 0, false, false);
                }
            }
        }

        public static int DropOreMethod(int x, int y, int type)
        {
            float ChanceBalance = 1;
            int SecondDrop = AALuckyConfig.LuckyOre.Keys.Count;
            foreach (int itemtype in AALuckyConfig.LuckyOre.Keys)
            {
                float chance = AALuckyConfig.LuckyOre[itemtype];
                chance -= Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].pick;
                chance = chance / ChanceBalance * 100f;
                if (chance < 100 && !(itemtype == ItemID.DemoniteOre || itemtype == ItemID.CrimtaneOre || itemtype == ModContent.ItemType<AbyssiumOre>() || itemtype == ModContent.ItemType<IncineriteOre>() || itemtype == ModContent.ItemType<ApocalyptiteOre>()))
                {
                    if (Main.rand.NextFloat(SecondDrop) < 1)
                    {
                        int itemcreat = Item.NewItem(Entity.GetSource_NaturalSpawn(), x * 16, y * 16, 32, 32, itemtype, 1, false, 0, false, false);
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
                        }
                    }
                    SecondDrop -= 1;
                    continue;
                }
                else if (itemtype == ItemID.DemoniteOre || itemtype == ItemID.CrimtaneOre)
                {
                    if (Main.rand.NextFloat(chance) < 1 && (type == TileID.Crimstone || type == TileID.Ebonstone))
                    {
                        return itemtype;
                    }
                }
                else if (itemtype == ModContent.ItemType<AbyssiumOre>())
                {
                    if (Main.rand.NextFloat(chance) < 1 && type == ModContent.TileType<Depthstone_Tile>())
                    {
                        return itemtype;
                    }
                }
                else if (itemtype == ModContent.ItemType<IncineriteOre>())
                {
                    if (Main.rand.NextFloat(chance) < 1 && type == ModContent.TileType<Torchstone_Tile>())
                    {
                        return itemtype;
                    }
                }
                else if (itemtype == ModContent.ItemType<ApocalyptiteOre>())
                {
                    if (Main.rand.NextFloat(chance) < 1 && type == ModContent.TileType<Doomstone_Tile>() && AADowned.DownedZero)
                    {
                        return itemtype;
                    }
                }
                else if (!Main.hardMode)
                {
                    if (AALuckyConfig.LuckyOre[itemtype] <= 500)
                    {
                        if (Main.rand.NextFloat(chance) < 1)
                        {
                            return itemtype;
                        }
                    }
                }
                else
                {
                    chance /= 2 * (1 + (NPC.downedPlantBoss ? 1 : 0) + (NPC.downedMoonlord ? 1 : 0) + (AADowned.downedEquinoxWorms ? 1 : 0) + (AADowned.DownedShen ? 1 : 0));
                    int digcheck = 500 + (NPC.downedPlantBoss ? 200 : 0) + (NPC.downedMoonlord ? 110 : 0) + (AADowned.downedEquinoxWorms ? 20 : 0);
                    bool flag = AALuckyConfig.LuckyOre[itemtype] <= digcheck;
                    if (flag || AADowned.DownedShen)
                    {
                        if (Main.rand.NextFloat(chance) < 1)
                        {
                            return itemtype;
                        }
                    }
                }
                ChanceBalance = 1 - 1 / chance;
            }
            return 0;
        }
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAMod.Items.Boss.Akuma;
using AAMod.Items.Boss.Grips;
using AAMod.Items;
using AAMod.Items.Dev.Invoker;
using AAMod.Items.Usable;
using Terraria.GameContent.ItemDropRules;

namespace AAMod
{
    public class AAModGlobalItem : GlobalItem
	{
        public override bool InstancePerEntity => true;
		protected override bool CloneNewInstances => true;
		public bool AAOnly = false;
        public bool NOHitPlayer = false;
        public bool HardCoreMode = false;
        public bool spellbookmagic = false;
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.SoulofNight)
            {
                item.color = WorldGen.crimson ? Color.Firebrick : Color.Violet;
            }

            if (item.type == ItemID.LunarOre)
            {
                item.createTile = Mod.Find<ModTile>("LuminiteOre").Type;
            }

            if (item.ModItem != null && item.ModItem.Mod.Name == Mod.Name && (item.damage > 0 || item.accessory || item.defense > 0) && item.maxStack < 2)
            {
                BaseAAItem AAitem = (BaseAAItem)item.ModItem;

                if (AAitem.AARarity != 0)
                {
                    switch (AAitem.AARarity)
                    {
                        case 12:
                            item.value = Item.sellPrice(0, 30, 0, 0);
                            break;

                        case 13:
                            item.value = Item.sellPrice(0, 35, 0, 0);
                            break;

                        case 14:
                            item.value = Item.sellPrice(0, 40, 0, 0);
                            break;

                        case 15:
                            item.value = Item.sellPrice(0, 45, 0, 0);
                            break;
                    }
                }
                else
                {
                    switch (item.rare)
                    {
                        case 0:
                            item.value = Item.sellPrice(0, 0, 25, 0);
                            break;

                        case 1:
                            item.value = Item.sellPrice(0, 0, 50, 0);
                            break;

                        case 2:
                            item.value = Item.sellPrice(0, 0, 75, 0);
                            break;

                        case 3:
                            item.value = Item.sellPrice(0, 1, 0, 0);
                            break;

                        case 4:
                            item.value = Item.sellPrice(0, 2, 0, 0);
                            break;

                        case 5:
                            item.value = Item.sellPrice(0, 4, 0, 0);
                            break;

                        case 6:
                            item.value = Item.sellPrice(0, 6, 0, 0);
                            break;

                        case 7:
                            item.value = Item.sellPrice(0, 8, 0, 0);
                            break;

                        case 8:
                            item.value = Item.sellPrice(0, 10, 0, 0);
                            break;

                        case 9:
                            item.value = Item.sellPrice(0, 15, 0, 0);
                            break;

                        case 10:
                            item.value = Item.sellPrice(0, 20, 0, 0);
                            break;

                        case 11:
                            item.value = Item.sellPrice(0, 25, 0, 0);
                            break;
                    }
                }
            }
            if(item.CountsAsClass(DamageClass.Magic) && item.useStyle == 5 && !Item.staff[item.type] && item.width < item.height * 1.25 && !item.channel)
            {
                spellbookmagic = true;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
            if(AAOnly)
            {
                TooltipLine line = new TooltipLine(Mod, "AAOnly", "AAMod Loaded Only Item");
			    tooltips.Insert(tooltips.Count,line);
            }
            if(NOHitPlayer)
            {
                TooltipLine line = new TooltipLine(Mod, "NOHitPlayer", "NohitPlayer bonus item");
			    tooltips.Insert(tooltips.Count,line);
            }
            if(HardCoreMode)
            {
                TooltipLine line = new TooltipLine(Mod, "HardCoreMode", "HardCoreMode Item");
			    tooltips.Insert(tooltips.Count,line);
            }
		}

        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            Item HeldItem = player.HeldItem;
            if (HeldItem.type == ModContent.ItemType<CodeMagnet>())
            {
                grabRange += 810;
            }
        }

        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (item.type == ItemID.AnkhShield || item.type == ItemID.ObsidianShield || item.type == ModContent.ItemType<TaiyangBaolei>() || item.type == Mod.Find<ModItem>("Duality").Type)
            {
                if (slot < 10)
                {
                    int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                        if (slot != i && player.armor[i].type == ItemID.AnkhShield)
                        {
                            return false;
                        }

                        if (slot != i && player.armor[i].type == ItemID.ObsidianShield)
                        {
                            return false;
                        }

                        if (slot != i && player.armor[i].type == ModContent.ItemType<TaiyangBaolei>())
                        {
                            return false;
                        }

                        if (slot != i && player.armor[i].type == Mod.Find<ModItem>("Duality").Type)
                        {
                            return false;
                        }
                    }
                }
            }

            if (item.type == ItemID.EoCShield ||  item.type == ModContent.ItemType<BulwarkOfChaos>())
            {
                if (slot < 10)
                {
                    int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                        if (slot != i && player.armor[i].type == ItemID.EoCShield)
                        {
                            return false;
                        }

                        if (slot != i && player.armor[i].type == ModContent.ItemType<BulwarkOfChaos>())
                        {
                            return false;
                        }
                    }
                }
            }

            if (item.type == ItemID.WormScarf)
            {
                if (slot < 10)
                {
                    int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        if (slot != i && player.armor[i].type == ItemID.WormScarf)
                        {
                            return false;
                        }
                        if (slot != i && player.armor[i].type == ModContent.ItemType<Items.Boss.DragonSerpentNecklace>())
                        {
                            return false;
                        }
                        if (slot != i && player.armor[i].type == ModContent.ItemType<Items.Boss.Broodmother.DragonCape>())
                        {
                            return false;
                        }
                    }

                }
            }

            return true;
        }

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            switch(item.type)
            {
                case ItemID.GoldenCrate:
                    itemLoot.Add(ItemDropRule.FewFromOptions(1, 5, 
                        ItemID.AnglerEarring, 
                        ItemID.HighTestFishingLine, 
                        ItemID.TackleBox, 
                        ItemID.AnglerHat, 
                        ItemID.AnglerVest, 
                        ItemID.AnglerPants,
                        ItemID.FishermansGuide,
                        ItemID.WeatherRadio,
                        ItemID.Sextant,
                        ItemID.GoldenFishingRod,
                        ItemID.GoldenBugNet,
                        ItemID.FishHook,
                        ItemID.BottomlessBucket,
                        ItemID.SuperAbsorbantSponge,
                        ItemID.HotlineFishingHook
                    ));
                    break;
                case ItemID.GoldenCrateHard:
                    itemLoot.Add(ItemDropRule.FewFromOptions(1, 5,
                        ItemID.AnglerEarring,
                        ItemID.HighTestFishingLine,
                        ItemID.TackleBox,
                        ItemID.AnglerHat,
                        ItemID.AnglerVest,
                        ItemID.AnglerPants,
                        ItemID.FishermansGuide,
                        ItemID.WeatherRadio,
                        ItemID.Sextant,
                        ItemID.GoldenFishingRod,
                        ItemID.GoldenBugNet,
                        ItemID.FishHook,
                        ItemID.BottomlessBucket,
                        ItemID.SuperAbsorbantSponge,
                        ItemID.HotlineFishingHook,
                        ItemID.FinWings
                    ));
                    break;
            }
        }

        /*
        public override void OpenVanillaBag(string context, Player player, int arg)
		{
            if(context == "crate" && arg == ItemID.GoldenCrate)
            {
                if(Main.rand.Next(5) == 0)
                {
                    switch(WorldGen.genRand.Next(20))
                    {
                        case 0:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.AnglerEarring, 1);
                            break;
                        case 1:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.HighTestFishingLine, 1);
                            break;
                        case 2:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.TackleBox, 1);
                            break;
                        case 3:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.AnglerHat, 1);
                            break;
                        case 4:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.AnglerVest, 1);
                            break;
                        case 5:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.AnglerPants, 1);
                            break;
                        case 6:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.FishermansGuide, 1);
                            break;
                        case 7:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.WeatherRadio, 1);
                            break;
                        case 8:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.Sextant, 1);
                            break;
                        case 9:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.GoldenFishingRod, 1);
                            break;
                        case 10:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.GoldenBugNet, 1);
                            break;
                        case 11:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.FishHook, 1);
                            break;
                        case 12:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.FuzzyCarrot, 1);
                            break;
                        case 13:
                            if (Main.hardMode)
                            {
                                player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.FinWings, 1);
                            }
                            else
                            {
                                goto default;
                            }
                            break;
                        case 14:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.BottomlessBucket, 1);
                            break;
                        case 15:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.SuperAbsorbantSponge, 1);
                            break;
                        default:
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.HotlineFishingHook, 1);
                            break;

                    }
                }
            }
		}
        */

        public override bool OnPickup(Item item, Player player)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (item.ammo == AmmoID.Coin)
            {
                if (modPlayer.GreedCharm)
                {
                    player.AddBuff(ModContent.BuffType<Items.Boss.Greed.CharmBuff>(), 240);
                    if (modPlayer.GreedyDamage < 20)
                    {
                        modPlayer.GreedyDamage += 1;
                    }
                }
                else if (modPlayer.GreedTalisman)
                {
                    player.AddBuff(ModContent.BuffType<Items.Boss.Greed.WKG.TalismanBuff>(), 240);
                    if (modPlayer.GreedyDamage < 40)
                    {
                        modPlayer.GreedyDamage += 1;
                    }
                }
            }
            return base.OnPickup(item, player);
        }

        public static void OpenAACrate(Player player, int CrateType)
        {
            bool noRareItem = true;
            while (noRareItem)
            {
                if (Main.rand.Next(4) == 0)
                {
                    player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.GoldCoin, Main.rand.Next(5, 13));
                    noRareItem = false;
                }

                if (Main.rand.Next(4) == 0)
                {
                    int item = 0;
                    int amount = 0;

                    if (!Main.hardMode || Main.rand.Next(3) == 0)
                    {
                        int[] items = new int[]
                        {
                            ItemID.IronBar, ItemID.SilverBar, ItemID.GoldBar,
                            ItemID.LeadBar, ItemID.TungstenBar, ItemID.PlatinumBar
                        };
                        item = Main.rand.Next(items);
                        amount = Main.rand.Next(10, 21);
                    }
                    else if (Main.hardMode)
                    {
                        int[] items = new int[]
                        {
                            ItemID.CobaltBar,
                            ItemID.PalladiumBar,
                            ItemID.MythrilBar,
                            ItemID.OrichalcumBar,
                            ItemID.AdamantiteBar,
                            ItemID.TitaniumBar,
                        };
                        item = Main.rand.Next(items);
                        amount = Main.rand.Next(7, 18);
                    }

                    player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), item, amount);
                    noRareItem = false;
                }
            }

            if (Main.rand.Next(4) == 0)
            {
                int[] items = new int[]
                {
                    ItemID.ObsidianSkinPotion, ItemID.SpelunkerPotion,
                    ItemID.HunterPotion, ItemID.GravitationPotion,
                    ItemID.MiningPotion, ItemID.HeartreachPotion
                };
                player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), Main.rand.Next(items), Main.rand.Next(2, 5));
            }

            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), Main.rand.Next(188, 190), Main.rand.Next(5, 18));
            }

            if (Main.rand.Next(2) == 0)
            {
                int item = Main.rand.Next(2) == 0 ? ItemID.MasterBait : ItemID.JourneymanBait;
                player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), item, Main.rand.Next(2, 7));
            }

            Mod mod = AAMod.instance;

            if (CrateType < 2)
            {
                if (Main.rand.Next(6) == 0)
                {
                    if (CrateType == 0)
                    {
                        int item = Main.rand.Next(5);

                        switch (item)
                        {
                            case 0:
                                item = mod.Find<ModItem>("Pyrosphere").Type;
                                break;
                            case 1:
                                item = mod.Find<ModItem>("Firebuster").Type;
                                break;
                            case 2:
                                item = mod.Find<ModItem>("Volley").Type;
                                break;
                            case 3:
                                item = mod.Find<ModItem>("DragonsSoul").Type;
                                break;
                            default:
                                item = mod.Find<ModItem>("DragonsGuard").Type;
                                break;
                        }
                        player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), item);
                        player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), mod.Find<ModItem>("IncineriteBar").Type, Main.rand.Next(1, 12));

                    }
                    if (CrateType == 1)
                    {
                        int item = Main.rand.Next(5);

                        switch (item)
                        {
                            case 0:
                                item = mod.Find<ModItem>("HydrasSpear").Type;
                                break;
                            case 1:
                                item = mod.Find<ModItem>("Mossket").Type;
                                break;
                            case 2:
                                item = mod.Find<ModItem>("GlowmossBall").Type;
                                break;
                            case 3:
                                item = mod.Find<ModItem>("ShadowBand").Type;
                                break;
                            default:
                                item = mod.Find<ModItem>("GunkWand").Type;
                                break;
                        }

                        player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), item);
                        player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), mod.Find<ModItem>("AbyssiumBar").Type, Main.rand.Next(1, 12));

                    }
                }
                if (Main.hardMode && Main.rand.Next(2) == 0)
                {
                    int item = CrateType == 1 ? ModContent.ItemType<Items.Materials.SoulOfSpite>() : ModContent.ItemType<Items.Materials.SoulOfSmite>();
                    player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), item, Main.rand.Next(2, 6));
                }

                if (Main.hardMode && Main.rand.Next(2) == 0)
                {
                    int item = CrateType == 1 ? ModContent.ItemType<Items.Materials.HydraToxin>() : ModContent.ItemType<Items.Materials.DragonFire>();
                    player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), item, Main.rand.Next(2, 6));
                }
            }
        }
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ShieldUp && item.damage > 0)
            {
                return false;
            }
            return true;
        }
    }

    public class ExtractinatorItem : GlobalItem
    {
        public override bool? UseItem(Item item, Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            if(player.GetModPlayer<AAPlayer>().StripeManOre)
            {
                int tileTargetX = (int)((Main.mouseX + Main.screenPosition.X) / 16f);
				int tileTargetY = (int)((Main.mouseY + Main.screenPosition.Y) / 16f);
                if(Main.tile[tileTargetX, tileTargetY].HasTile && Main.tile[tileTargetX, tileTargetY].TileType == 219 && item.createTile > 0 && (Main.tileSand[item.createTile] || TileID.Sets.Conversion.Sand[item.createTile]))
                {
                    bool flag = player.position.X / 16f - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost - player.blockRange <= Player.tileTargetX && (player.position.X + player.width) / 16f + Player.tileRangeX + player.inventory[player.selectedItem].tileBoost - 1f + player.blockRange >= Player.tileTargetX && player.position.Y / 16f - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost - player.blockRange <= Player.tileTargetY && (player.position.Y + player.height) / 16f + Player.tileRangeY + player.inventory[player.selectedItem].tileBoost - 2f + player.blockRange >= Player.tileTargetY;
                    if(flag && player.itemTime == 0 && player.itemAnimation > 0 && player.controlUseItem)
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
                                    player.inventory[i].SetDefaults(0, false);
                                }
                                break;
                            }
                        }
                    }
                }
            }
            
			return false;
		}
        public void ExtractinatorUse2(int extractType)
		{
            int result = 0;
            int stack = 1;
            if(extractType == ItemID.EbonsandBlock)
            {
                if(Main.rand.Next(10) == 0)
                {
                    result = 56;
                    if (Main.rand.Next(5) == 0)
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }
            else if(extractType == ItemID.CrimsandBlock)
            {
                if(Main.rand.Next(10) == 0)
                {
                    result = 880;
                    if (Main.rand.Next(5) == 0)
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }
            else if(extractType == Mod.Find<ModItem>("Depthsand").Type)
            {
                if(Main.rand.Next(10) == 0)
                {
                    result = Mod.Find<ModItem>("Abyssium").Type;
                    if (Main.rand.Next(5) == 0)
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }
            else if(extractType == Mod.Find<ModItem>("Torchsand").Type)
            {
                if(Main.rand.Next(10) == 0)
                {
                    result = Mod.Find<ModItem>("Incinerite").Type;
                    if (Main.rand.Next(5) == 0)
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }
            else if(extractType == ItemID.PearlsandBlock)
            {
                if(Main.rand.Next(10) == 0)
                {
                    result = Main.rand.Next(2) == 0? 1104 : 364;

                    if (Main.rand.Next(5) == 0)
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        stack += Main.rand.Next(4);
                    }
                }
                else if(Main.rand.Next(10) == 0)
                {
                    result = Main.rand.Next(2) == 0? 1105 : 365;
                    if (Main.rand.Next(5) == 0)
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        stack += Main.rand.Next(4);
                    }
                }
                else if(Main.rand.Next(10) == 0)
                {
                    result = Main.rand.Next(2) == 0? 1106 : 366;
                    if (Main.rand.Next(5) == 0)
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }
            if(result == 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    result = 3380;
                    if (Main.rand.Next(5) == 0)
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        stack += Main.rand.Next(4);
                    }
                }
                else if (Main.rand.Next(2) == 0)
                {
                    if (Main.rand.Next(12000) == 0)
                    {
                        result = 74;
                        if (Main.rand.Next(14) == 0)
                        {
                            stack += Main.rand.Next(0, 2);
                        }
                        if (Main.rand.Next(14) == 0)
                        {
                            stack += Main.rand.Next(0, 2);
                        }
                        if (Main.rand.Next(14) == 0)
                        {
                            stack += Main.rand.Next(0, 2);
                        }
                    }
                    else if (Main.rand.Next(800) == 0)
                    {
                        result = 73;
                        if (Main.rand.Next(6) == 0)
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.Next(6) == 0)
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.Next(6) == 0)
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.Next(6) == 0)
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.Next(6) == 0)
                        {
                            stack += Main.rand.Next(1, 20);
                        }
                    }
                    else if (Main.rand.Next(60) == 0)
                    {
                        result = 72;
                        if (Main.rand.Next(4) == 0)
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.Next(4) == 0)
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.Next(4) == 0)
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.Next(4) == 0)
                        {
                            stack += Main.rand.Next(5, 25);
                        }
                    }
                    else
                    {
                        result = 71;
                        if (Main.rand.Next(3) == 0)
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.Next(3) == 0)
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.Next(3) == 0)
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.Next(3) == 0)
                        {
                            stack += Main.rand.Next(10, 25);
                        }
                    }
                }
                else if (Main.rand.Next(4000) == 0)
                {
                    result = 1242;
                }
                else if (Main.rand.Next(25) == 0)
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
                    if (Main.rand.Next(20) == 0)
                    {
                        stack += Main.rand.Next(0, 2);
                    }
                    if (Main.rand.Next(30) == 0)
                    {
                        stack += Main.rand.Next(0, 3);
                    }
                    if (Main.rand.Next(40) == 0)
                    {
                        stack += Main.rand.Next(0, 4);
                    }
                    if (Main.rand.Next(50) == 0)
                    {
                        stack += Main.rand.Next(0, 5);
                    }
                    if (Main.rand.Next(60) == 0)
                    {
                        stack += Main.rand.Next(0, 6);
                    }
                }
                else if (Main.rand.Next(50) == 0)
                {
                    result = 999;
                    if (Main.rand.Next(20) == 0)
                    {
                        stack += Main.rand.Next(0, 2);
                    }
                    if (Main.rand.Next(30) == 0)
                    {
                        stack += Main.rand.Next(0, 3);
                    }
                    if (Main.rand.Next(40) == 0)
                    {
                        stack += Main.rand.Next(0, 4);
                    }
                    if (Main.rand.Next(50) == 0)
                    {
                        stack += Main.rand.Next(0, 5);
                    }
                    if (Main.rand.Next(60) == 0)
                    {
                        stack += Main.rand.Next(0, 6);
                    }
                }
                else if (Main.rand.Next(3) == 0)
                {
                    if (Main.rand.Next(5000) == 0)
                    {
                        result = 74;
                        if (Main.rand.Next(10) == 0)
                        {
                            stack += Main.rand.Next(0, 3);
                        }
                        if (Main.rand.Next(10) == 0)
                        {
                            stack += Main.rand.Next(0, 3);
                        }
                        if (Main.rand.Next(10) == 0)
                        {
                            stack += Main.rand.Next(0, 3);
                        }
                        if (Main.rand.Next(10) == 0)
                        {
                            stack += Main.rand.Next(0, 3);
                        }
                        if (Main.rand.Next(10) == 0)
                        {
                            stack += Main.rand.Next(0, 3);
                        }
                    }
                    else if (Main.rand.Next(400) == 0)
                    {
                        result = 73;
                        if (Main.rand.Next(5) == 0)
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.Next(5) == 0)
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.Next(5) == 0)
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.Next(5) == 0)
                        {
                            stack += Main.rand.Next(1, 21);
                        }
                        if (Main.rand.Next(5) == 0)
                        {
                            stack += Main.rand.Next(1, 20);
                        }
                    }
                    else if (Main.rand.Next(30) == 0)
                    {
                        result = 72;
                        if (Main.rand.Next(3) == 0)
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.Next(3) == 0)
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.Next(3) == 0)
                        {
                            stack += Main.rand.Next(5, 26);
                        }
                        if (Main.rand.Next(3) == 0)
                        {
                            stack += Main.rand.Next(5, 25);
                        }
                    }
                    else
                    {
                        result = 71;
                        if (Main.rand.Next(2) == 0)
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            stack += Main.rand.Next(10, 26);
                        }
                        if (Main.rand.Next(2) == 0)
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
                    if (Main.rand.Next(20) == 0)
                    {
                        stack += Main.rand.Next(0, 2);
                    }
                    if (Main.rand.Next(30) == 0)
                    {
                        stack += Main.rand.Next(0, 3);
                    }
                    if (Main.rand.Next(40) == 0)
                    {
                        stack += Main.rand.Next(0, 4);
                    }
                    if (Main.rand.Next(50) == 0)
                    {
                        stack += Main.rand.Next(0, 5);
                    }
                    if (Main.rand.Next(60) == 0)
                    {
                        stack += Main.rand.Next(0, 6);
                    }
                }
            }
            if (result > 0)
            {
                Vector2 vector = Main.ReverseGravitySupport(Main.MouseScreen, 0f) + Main.screenPosition;
                int number = Item.NewItem(Item.GetSource_NaturalSpawn(), (int)vector.X, (int)vector.Y, 1, 1, result, stack, false, -1, false, false);
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
                }
            }
        }
        public override void ExtractinatorUse(int extractType, int extractinatorBlockType, ref int resultType, ref int resultStack)
		{
            int result = 0;
            int stack = 1;
            if(extractType == ItemID.SlushBlock)
            {
                if(Main.rand.Next(50) == 0)
                {
                    result = Mod.Find<ModItem>("VikingRelic").Type;
                    if (Main.rand.Next(5) == 0)
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }
            else if(extractType == ItemID.DesertFossil)
            {
                if(Main.rand.Next(50) == 0)
                {
                    result = Mod.Find<ModItem>("DynaskullOre").Type;
                    if (Main.rand.Next(5) == 0)
                    {
                        stack += Main.rand.Next(2);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        stack += Main.rand.Next(3);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        stack += Main.rand.Next(4);
                    }
                }
            }

            if(Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().StripeManOre)
            {
                if(extractType == ItemID.DesertFossil || extractType == ItemID.SlushBlock || extractType == ItemID.SiltBlock)
                {
                    if (Main.rand.Next(10) == 0)
                    {
                        result = 3380;
                        stack += 6;
                    }
                    else if (Main.rand.Next(10) == 0)
                    {
                        if (Main.rand.Next(500) == 0)
                        {
                            result = 74;
                            stack += 3;
                        }
                        else if (Main.rand.Next(200) == 0)
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
                    else if (Main.rand.Next(100) == 0)
                    {
                        result = 1242;
                    }
                    else if (Main.rand.Next(30) == 0)
                    {
                        if(Main.rand.Next(2) == 0)
                        {
                            result = Mod.Find<ModItem>("DynaskullOre").Type;
                            stack += 1;
                            if (Main.rand.Next(5) == 0)
                            {
                                stack += Main.rand.Next(2);
                            }
                            if (Main.rand.Next(10) == 0)
                            {
                                stack += Main.rand.Next(3);
                            }
                            if (Main.rand.Next(15) == 0)
                            {
                                stack += Main.rand.Next(4);
                            }
                        }
                        else
                        {
                            result = Mod.Find<ModItem>("VikingRelic").Type;
                            stack += 1;
                            if (Main.rand.Next(5) == 0)
                            {
                                stack += Main.rand.Next(2);
                            }
                            if (Main.rand.Next(10) == 0)
                            {
                                stack += Main.rand.Next(3);
                            }
                            if (Main.rand.Next(15) == 0)
                            {
                                stack += Main.rand.Next(4);
                            }
                        }
                    }
                    else if (Main.rand.Next(300) == 0)
                    {
                        switch(Main.rand.Next(8))
                        {
                            case 0: result = 12; return;
                            case 1: result=11; return;
                            case 2: result=14; return;
                            case 3: result=13; return;
                            case 4: result=699; return;
                            case 5: result=700; return;
                            case 6: result=701; return;
                            default: result=702; return;
                        }
                    }
                    else if (Main.rand.Next(20) == 0)
                    {
                        result = 999;
                        stack += 5;
                        if (Main.rand.Next(10) == 0)
                        {
                            stack += 5;
                        }
                        if (Main.rand.Next(20) == 0)
                        {
                            stack += 5;
                        }
                    }
                    else
                    {
                        switch(Main.rand.Next(6))
                        {
                            case 0: result=181; return;
                            case 1: result=180; return;
                            case 2: result=177; return;
                            case 3: result=179; return;
                            case 4: result=178; return;
                            default: result=182; return;
                        }
                    }
                }
            }
                
            if (stack > 99)
            {
                stack = 99;
            }
            if (result == 1242)
            {
                stack = 1;
            }

            if (result > 0)
			{
                resultType = result;
                resultStack = stack;
            }
            /*
            if (result > 0)
			{
				Vector2 vector = Main.ReverseGravitySupport(Main.MouseScreen, 0f) + Main.screenPosition;
				int number = Item.NewItem((int)vector.X, (int)vector.Y, 1, 1, resultType, resultStack, false, -1, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
				}
			}
            */
		}
    }

    public class InvokerCaligulaItem : GlobalItem
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.GetModPlayer<InvokerPlayer>().InvokedCaligula && item.damage > 0 && !(player.GetModPlayer<InvokerPlayer>().DarkCaligula && player.inventory[player.selectedItem].type == Mod.Find<ModItem>("InvokerStaff").Type && player.altFunctionUse == 2))
            {
                return false;
            }
            return true;
        }
    }
}

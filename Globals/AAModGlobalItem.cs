using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;
using AAModClassic.Items.Usable;
using AAModClassic.Items.Boss.Akuma;
using AAModClassic.Items.Dev.Invoker;
using AAModClassic.Tiles.Ore;
using AAModClassic.Items.Boss.Shen;
using AAModClassic.Items.Melee;
using AAModClassic.Items.Ranged;
using AAModClassic.Items.Magic;
using AAModClassic.Items.Blocks;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Pets;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.Accessories;
using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Accessories;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Pets;
using AAModClassic._Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic._Removed.Content._Tinker.___PreHardmode.Items.Accessories;

namespace AAModClassic.Globals
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
                item.createTile = ModContent.TileType<LuminiteOre_Tile>();
            }

            if (item.ModItem != null && item.ModItem.Mod.Name == Mod.Name && (item.damage > 0 || item.accessory || item.defense > 0) && item.maxStack < 2)
            {
                bool hasDoneShit = false;
                if (item.ModItem is BaseAAItem AAitem)
                {
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
                    hasDoneShit = true;
                }
                
                if (hasDoneShit == false)
                {
                    switch (item.rare)
                    {
                        case ItemRarityID.White:
                            item.value = Item.sellPrice(0, 0, 25, 0);
                            break;

                        case ItemRarityID.Blue:
                            item.value = Item.sellPrice(0, 0, 50, 0);
                            break;

                        case ItemRarityID.Green:
                            item.value = Item.sellPrice(0, 0, 75, 0);
                            break;

                        case ItemRarityID.Orange:
                            item.value = Item.sellPrice(0, 1, 0, 0);
                            break;

                        case ItemRarityID.LightRed:
                            item.value = Item.sellPrice(0, 2, 0, 0);
                            break;

                        case ItemRarityID.Pink:
                            item.value = Item.sellPrice(0, 4, 0, 0);
                            break;

                        case ItemRarityID.LightPurple:
                            item.value = Item.sellPrice(0, 6, 0, 0);
                            break;

                        case ItemRarityID.Lime:
                            item.value = Item.sellPrice(0, 8, 0, 0);
                            break;

                        case ItemRarityID.Yellow:
                            item.value = Item.sellPrice(0, 10, 0, 0);
                            break;

                        case ItemRarityID.Cyan:
                            item.value = Item.sellPrice(0, 15, 0, 0);
                            break;

                        case ItemRarityID.Red:
                            item.value = Item.sellPrice(0, 20, 0, 0);
                            break;

                        case ItemRarityID.Purple:
                            item.value = Item.sellPrice(0, 25, 0, 0);
                            break;
                    }
                }
            }
            if(item.CountsAsClass(DamageClass.Magic) && item.useStyle == ItemUseStyleID.Shoot && !Item.staff[item.type] && item.width < item.height * 1.25 && !item.channel)
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
            if (item.type == ItemID.AnkhShield || item.type == ItemID.ObsidianShield || item.type == ModContent.ItemType<TaiyangBaolei>() || item.type == ModContent.ItemType<Duality>())
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

                        if (slot != i && player.armor[i].type == ModContent.ItemType<Duality>())
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
                        if (slot != i && player.armor[i].type == ModContent.ItemType<DragonSerpentNecklace>())
                        {
                            return false;
                        }
                        if (slot != i && player.armor[i].type == ModContent.ItemType<DragontamersCloak>())
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

        public override bool OnPickup(Item item, Player player)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (item.ammo == AmmoID.Coin)
            {
                if (modPlayer.GreedCharm)
                {
                    player.AddBuff(ModContent.BuffType<CharmOfDesire_Desire>(), 240);
                    if (modPlayer.GreedyDamage < 20)
                    {
                        modPlayer.GreedyDamage += 1;
                    }
                }
                else if (modPlayer.GreedTalisman)
                {
                    player.AddBuff(ModContent.BuffType<TalismanOfDesire_RuthlessDesire>(), 240);
                    if (modPlayer.GreedyDamage < 40)
                    {
                        modPlayer.GreedyDamage += 1;
                    }
                }
            }
            return base.OnPickup(item, player);
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
                if(Main.tile[tileTargetX, tileTargetY].HasTile && Main.tile[tileTargetX, tileTargetY].TileType == TileID.Extractinator && item.createTile > TileID.Dirt && (Main.tileSand[item.createTile] || TileID.Sets.Conversion.Sand[item.createTile]))
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
        public void ExtractinatorUse2(int extractType)
		{
            int result = 0;
            int stack = 1;
            if(extractType == ItemID.EbonsandBlock)
            {
                if(Main.rand.NextBool(10))
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
            else if(extractType == ItemID.CrimsandBlock)
            {
                if(Main.rand.NextBool(10))
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
            else if(extractType == ModContent.ItemType<Depthsand>())
            {
                if(Main.rand.NextBool(10))
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
            else if(extractType == ModContent.ItemType<Torchsand>())
            {
                if(Main.rand.NextBool(10))
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
            else if(extractType == ItemID.PearlsandBlock)
            {
                if(Main.rand.NextBool(10))
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
                else if(Main.rand.NextBool(10))
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
                else if(Main.rand.NextBool(10))
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
            if(result == 0)
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
                int number = Item.NewItem(Item.GetSource_NaturalSpawn(), (int)vector.X, (int)vector.Y, 1, 1, result, stack, false, -1, false, false);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
                }
            }
        }
        public override void ExtractinatorUse(int extractType, int extractinatorBlockType, ref int resultType, ref int resultStack)
		{
            int result = 0;
            int stack = 1;
            if(extractType == ItemID.SlushBlock)
            {
                if(Main.rand.NextBool(50))
                {
                    result = ModContent.ItemType<VikingRelic>();
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
            else if(extractType == ItemID.DesertFossil)
            {
                if(Main.rand.NextBool(50))
                {
                    result = ModContent.ItemType<DynaskullFossil>();
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

            if(Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().StripeManOre)
            {
                if(extractType == ItemID.DesertFossil || extractType == ItemID.SlushBlock || extractType == ItemID.SiltBlock)
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
                    else if (Main.rand.NextBool(300))
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
            if (player.GetModPlayer<InvokerPlayer>().InvokedCaligula && item.damage > 0 && !(player.GetModPlayer<InvokerPlayer>().DarkCaligula && player.inventory[player.selectedItem].type == ModContent.ItemType<InvokerStaff>() && player.altFunctionUse == 2))
            {
                return false;
            }
            return true;
        }
    }
}

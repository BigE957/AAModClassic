using AAModClassic._Content._Dev.__Hardmode.Items.Weapons;
using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;
using AAModClassic._Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Accessories;
using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Accessories;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._Content.Void.___PreHardmode.Items.Tools;
using AAModClassic._Removed.Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic.Rarities;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Globals
{
    public class AAModGlobalItem : GlobalItem
	{
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.Cannonball)
                item.ammo = ItemID.Cannonball;

            if (item.type == ItemID.SoulofNight)
            {
                item.color = WorldGen.crimson ? Color.Firebrick : Color.Violet;
            }

            if (item.ModItem != null && item.ModItem.Mod.Name == Mod.Name && (item.damage > 0 || item.accessory || item.defense > 0) && item.maxStack < 2)
            {
                bool hasDoneShit = false;


                if(item.rare == ModContent.RarityType<PostEquinoxRarity>())
                { 
                    item.value = Item.sellPrice(0, 30, 0, 0);
                    hasDoneShit = true;
                }
                else if (item.rare == ModContent.RarityType<AncientsRarity>())
                {
                    item.value = Item.sellPrice(0, 35, 0, 0);
                    hasDoneShit = true;
                }
                else if (item.rare == ModContent.RarityType<SuperancientsRarity>())
                {
                    item.value = Item.sellPrice(0, 40, 0, 0);
                    hasDoneShit = true;
                }
                else if (item.rare == ModContent.RarityType<HyperancientsRarity>())
                {
                    item.value = Item.sellPrice(0, 45, 0, 0);
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
        }

        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                foreach (Item invItem in player.inventory)
                {
                    if (invItem.type == ModContent.ItemType<CodeMagnet>())
                        grabRange += 810;
                } 
            }
            else
            {
                if (player.HeldItem.type == ModContent.ItemType<CodeMagnet>())
                    grabRange += 810;
            }
        }

        //TODO: move this stuff into its own file... obv...
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed) && item.type == ItemID.AnkhShield)
            {
                player.GetAttackSpeed(DamageClass.Melee) += 0.07f;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed) && item.type == ItemID.AnkhShield)
            {
                int index = 1;
                for (int m = 0; m < tooltips.Count; m++)
                {
                    TooltipLine line = tooltips[m];
                    if (line.Mod == "Terraria" && line.Text == "Grants immunity to most debuffs")
                    {
                        index = m;
                        break;
                    }
                }
                tooltips.Insert(index + 1, new TooltipLine(Mod, "AnkhShieldMeleeSpeed", "7% melee speed"));
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
    }

    public class ExtractinatorItem : GlobalItem
    {
        public override void ExtractinatorUse(int extractType, int extractinatorBlockType, ref int resultType, ref int resultStack)
		{
            int result = 0;
            int stack = 1;
            // these both allow weird tierskipping im killing them
            /*
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
            */
                
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
            if (player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().InvokedCaligula && item.damage > 0 && !(player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().DarkCaligula && player.inventory[player.selectedItem].type == ModContent.ItemType<AleisterStaff>() && player.altFunctionUse == 2))
            {
                return false;
            }
            return true;
        }
    }
}

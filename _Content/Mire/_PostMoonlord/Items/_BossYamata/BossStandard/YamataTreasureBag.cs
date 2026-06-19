using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Accessories;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Ammo;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Tools;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard
{
    public class YamataTreasureBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Red;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
        //public override int BossBagNPC => ModContent.NPCType<YamataA>();

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.OneFromOptions(7, ModContent.ItemType<YamataMask>(), ModContent.ItemType<YamataAMask>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DreadScale>(), 1, 30, 40));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Naitokurosu>()));

            int[] lootTable = { ModContent.ItemType<Flairdra>(), ModContent.ItemType<Crescent>(), ModContent.ItemType<Amenomuraku>(), ModContent.ItemType<EventideArrow>(), ModContent.ItemType<HydraStabber>(), ModContent.ItemType<MidnightWrath>(), ModContent.ItemType<AbyssalYari>(), ModContent.ItemType<AbyssalBomb>(), ModContent.ItemType<AbyssalEruption>(), ModContent.ItemType<Darksprayer>(), ModContent.ItemType<FallingTwilight>(), ModContent.ItemType<Sevenshot>(), ModContent.ItemType<ThrowingCrescent>(), ModContent.ItemType<DreadTerratool>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}
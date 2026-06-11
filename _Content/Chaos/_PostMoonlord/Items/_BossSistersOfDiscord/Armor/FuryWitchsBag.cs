using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    public class FuryWitchsBag : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ashe's Satchel");
            /* Tooltip.SetDefault(@"Right click to open
Contains a set of Fury Witch's robes"); */
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FuryWitchsHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FuryWitchsChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FuryWitchsLeggings>());
        }
    }
}

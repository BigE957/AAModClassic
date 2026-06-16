using AAModClassic._Content._Dev.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Cerberus
{
    public class CerberusBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pup Cerberus' Kennel");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Invoker of Pups!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
            Item.createTile = ModContent.TileType<CerberusKennel_Tile>(); 
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CerberusHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CerberusChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CerberusLeggings>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CerberusWhistle>());
        }
    }
}
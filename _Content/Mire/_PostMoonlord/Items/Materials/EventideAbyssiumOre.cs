using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items.Materials
{
    public class EventideAbyssiumOre : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eventide Abyssium Ore");
            // Tooltip.SetDefault("It's cold and wet, like an evening in a swampy marsh");

            ItemTrader.ChlorophyteExtractinator.AddOption_Interchangable(ModContent.ItemType<EventideAbyssiumOre>(), ModContent.ItemType<DaybreakIncineriteOre>());

            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<EventideAbyssiumOre_Tile>(); //put your CustomBlock Tile name
        }

        
    }
}

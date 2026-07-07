using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.BossStandard
{
    public class SoulOfCthulhuTrophy : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Boss.Trophy";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul of Cthulhu Trophy");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 2000;
            Item.createTile = ModContent.TileType<SoulOfCthulhuTrophy_Tile>();
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        
    }
}

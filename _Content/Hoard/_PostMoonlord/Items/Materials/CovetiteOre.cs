using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.Items.Materials
{
    public class CovetiteOre : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
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
            Item.value = Item.sellPrice(0, 1, 8, 0);
            Item.consumable = true;
            Item.createTile = ModContent.TileType<CovetiteOre_Tile>();
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Covetite Ore");
            // Tooltip.SetDefault("Only a fool would want this. Makes sense why greed has it.");
            Item.ResearchUnlockCount = 100;
        }

    }
}

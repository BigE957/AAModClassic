using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic._Content.Hallow.__Hardmode.Items.Materials
{
    public class HallowedOre : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<HallowedOre_Tile>();
            Item.value = 10000;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hallowed Ore");
            // Tooltip.SetDefault("It's super bright");
        }

    }
}

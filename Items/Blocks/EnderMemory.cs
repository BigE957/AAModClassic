using AAModClassic.Tiles.Decoration;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic.Items.Blocks
{
    public class EnderMemory : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eternal Memory");
            /* Tooltip.SetDefault(@"An immense statue made to commemorate somebody
A somber engraving is etched into the base."); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 38;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = ModContent.TileType<EnderMemory_Tile>();
        }
    }
}

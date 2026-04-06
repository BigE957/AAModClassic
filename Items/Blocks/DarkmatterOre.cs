using AAModClassic;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic.Items.Blocks
{
    public class DarkmatterOre : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.Red;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<RadiumOre_Tile>(); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Darkmatter Ore");
            // Tooltip.SetDefault("It feels weightless, yet it still has some kind of mass to it");
        }

    }
}

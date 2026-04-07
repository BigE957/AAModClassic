using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic.___Content.Mire._PreHardmode.Items.Materials
{
    public class AbyssiumOre : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.value = Terraria.Item.sellPrice(0, 0, 8, 0);
            Item.consumable = true;
            Item.createTile = ModContent.TileType<AbyssiumOre_Tile>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssium");
            // Tooltip.SetDefault("It's all mushy. Nasty.");
        }
    }
}

using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic.___Content.Void.___PreHardmode.Items.Materials
{
    public class DoomiteBar : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomite Bar");
            // Tooltip.SetDefault("Unsettling energy radiates from this bar");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Orange;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<DoomiteBar_Tile>();
            Item.value = Terraria.Item.sellPrice(0, 0, 32, 0);
        }
    }
}
using AAModClassic;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic.Items.Materials
{
    public class OrderBar : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Order Bar");
            // Tooltip.SetDefault("Glows with the power of unity");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
			Item.maxStack = 99;
            Item.rare = ItemRarityID.Green;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("OrderBar").Type;
            Item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
        }
    }
}

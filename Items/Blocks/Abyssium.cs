using Terraria.ModLoader;
namespace AAMod.Items.Blocks
{
    public class Abyssium : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.rare = 1;
            Item.value = Terraria.Item.sellPrice(0, 0, 8, 0);
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("AbyssiumOre").Type;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssium");
            // Tooltip.SetDefault("It's all mushy. Nasty.");
        }
    }
}

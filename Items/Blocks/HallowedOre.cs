using Terraria.ModLoader;
namespace AAMod.Items.Blocks
{
    public class HallowedOre : BaseAAItem
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
            Item.rare = 4;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("HallowedOre").Type;
            Item.value = 10000;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hallowed Ore");
            // Tooltip.SetDefault("It's super bright");
        }

    }
}

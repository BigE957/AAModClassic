using Terraria.ModLoader;
namespace AAMod.Items.Blocks
{
    public class RadiumOre : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.rare = 10;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("RadiumOre").Type; //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radium Ore");
            // Tooltip.SetDefault("Twinkles like the stars in the midnight skies");
        }

    }
}

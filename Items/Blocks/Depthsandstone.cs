using Terraria.ModLoader;
namespace AAMod.Items.Blocks
{
    public class Depthsandstone : BaseAAItem
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
            Item.consumable = true;
            Item.rare = 2;
            Item.createTile = Mod.Find<ModTile>("Depthsandstone").Type; //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Depthsandstone");
        }

    }
}

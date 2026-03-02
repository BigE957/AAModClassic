using Terraria.ModLoader;
namespace AAMod.Items.Blocks
{
    public class Torchice : BaseAAItem
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
            Item.rare = 2;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("Torchice").Type; //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Orange Ice");
          // Tooltip.SetDefault(@"");
        }

    }
}

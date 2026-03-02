using Terraria.ModLoader;
namespace AAMod.Items.Materials
{
    public class DoomiteScrap : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomite Scrap");
            /* Tooltip.SetDefault(@"It's worthless
...or is it?"); */
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.rare = -1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("DoomitePlate").Type;
        }
    }
}
using Terraria.ID;
using Terraria.ModLoader;
namespace AAMod.Items.Blocks
{
    public class Darkmud : BaseAAItem
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
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("Darkmud").Type; //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmud");
        }
    }
}

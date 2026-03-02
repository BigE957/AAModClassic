using Terraria.ID;
using Terraria.ModLoader;
namespace AAMod.Items.Blocks
{
    class Razewood : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 24;
            Item.height = 22;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("Razewood").Type; //put your CustomBlock Tile name
            Item.ammo = Item.type;
            Item.notAmmo = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood");
            // Tooltip.SetDefault("");
        }
    }
}

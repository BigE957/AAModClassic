using AAModClassic;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic.Items.Blocks
{
    public class DoomstoneB : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("DoomstoneB").Type; //put your CustomBlock Tile name
        }
       
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Doomstone");
            // Tooltip.SetDefault("");

        }
    }
}

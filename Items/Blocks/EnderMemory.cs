using Terraria.ModLoader;
namespace AAMod.Items.Blocks
{
    public class EnderMemory : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eternal Memory");
            /* Tooltip.SetDefault(@"An immense statue made to commemorate somebody
A somber engraving is etched into the base."); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 38;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = 9;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = Mod.Find<ModTile>("EnderMemory").Type;
        }
    }
}

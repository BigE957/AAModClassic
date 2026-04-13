using Terraria.ID;

namespace AAModClassic.___Content.Acropolis.__Hardmode.Items.Materials
{
    public class SeraphFeather : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Seraph Feather");
            // Tooltip.SetDefault("A silvery feather from a harpy seraph");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 36;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Lime;
        }
    }
}
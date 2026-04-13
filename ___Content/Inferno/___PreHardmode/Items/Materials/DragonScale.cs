using Terraria.ID;

namespace AAModClassic.___Content.Inferno.___PreHardmode.Items.Materials
{
    public class DragonScale : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Scale");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Blue;
        }
    }
}
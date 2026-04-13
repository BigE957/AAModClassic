using Terraria.ID;

namespace AAModClassic.___Content.Mire.___PreHardmode.Items.Materials
{
    public class MirePod : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mire Pod");
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
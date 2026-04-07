using Terraria.ID;

namespace AAModClassic.___Content.Mire.Quest
{
    public class SwampKoi : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Swamp Koi");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Orange;
        }
    }
}
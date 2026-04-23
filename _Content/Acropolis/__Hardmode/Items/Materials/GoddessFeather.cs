using Terraria.ID;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items.Materials
{
    public class GoddessFeather : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Goddess Feather");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.maxStack = 99;
            Item.value = 50000;
            Item.rare = ItemRarityID.Lime;
        }
    }
}

using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Materials
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
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
        }
    }
}
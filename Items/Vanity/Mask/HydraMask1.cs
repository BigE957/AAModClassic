using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class HydraMask1 : BaseAAItem
    {
        public static int type;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hydra Mask");
		}

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.rare = ItemRarityID.Green;
            Item.vanity = true;
        }
    }
}
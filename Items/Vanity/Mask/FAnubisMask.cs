using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class FAnubisMask : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Forsaken Anubis Mask");
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
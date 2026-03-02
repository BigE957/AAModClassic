using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class YamataMask : BaseAAItem
	{
        public static int type;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Yamata Mask");
		}

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 26;
            Item.rare = ItemRarityID.Green;
            Item.vanity = true;
        }
    }
}
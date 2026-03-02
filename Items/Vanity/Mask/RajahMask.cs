using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class RajahMask : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Rajah Rabbit Mask");
		}

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;
            Item.rare = ItemRarityID.Green;
            Item.vanity = true;
        }
    }
}
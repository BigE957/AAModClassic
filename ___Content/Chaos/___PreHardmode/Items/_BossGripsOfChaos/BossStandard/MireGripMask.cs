using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class MireGripMask : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Mire Grip of Chaos Mask");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
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
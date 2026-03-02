using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class GripMaskBlue : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Mire Grip of Chaos Mask");
		}

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;
            Item.rare = ItemRarityID.Green;
            Item.vanity = true;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true */
        {
            drawHair = true;
        }
    }
}
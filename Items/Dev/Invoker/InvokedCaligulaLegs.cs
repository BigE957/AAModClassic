using Terraria.ModLoader;

namespace AAMod.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.Legs)]
	public class InvokedCaligulaLegs : EquipTexture
	{
		public override bool DrawLegs()/* tModPorter Note: Removed. After registering this as EquipType.Legs or Shoes, use ArmorIDs.Legs.Sets.HidesBottomSkin[slot] = true if you returned false for EquipType.Legs, and ArmorIDs.Shoe.Sets.OverridesLegs[slot] = true if you returned false for EquipType.Shoes */
		{
			return false;
		}
	}
}
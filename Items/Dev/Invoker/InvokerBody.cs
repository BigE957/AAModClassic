using Terraria.ModLoader;

namespace AAMod.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.Body)]
	public class InvokerBody : EquipTexture
	{
		public override bool DrawBody()/* tModPorter Note: Removed. After registering this as EquipType.Body, use ArmorIDs.Body.Sets.HidesTopSkin[slot] = true if you returned false */
		{
			return false;
		}
	}
}
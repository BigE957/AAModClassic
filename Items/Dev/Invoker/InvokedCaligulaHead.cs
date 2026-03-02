using Terraria.ModLoader;

namespace AAMod.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.Head)]
	public class InvokedCaligulaHead : EquipTexture
	{
		public override bool DrawHead()/* tModPorter Note: Removed. After registering this as EquipType.Head, use ArmorIDs.Head.Sets.DrawHead[slot] = false if you returned false */
		{
			return false;
		}
	}
}
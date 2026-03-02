using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.Head)]
	public class InvokedCaligulaHead : EquipTexture
	{
        public override void PreUpdateVanitySet(Player player)
        {
            ArmorIDs.Head.Sets.DrawHead[Slot] = false;
        }
	}
}
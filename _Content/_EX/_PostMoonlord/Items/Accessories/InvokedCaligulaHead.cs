using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Accessories
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
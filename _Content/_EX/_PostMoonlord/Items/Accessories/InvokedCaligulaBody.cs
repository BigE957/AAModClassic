using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.Body)]
	public class InvokedCaligulaBody : EquipTexture
	{
        public override void PreUpdateVanitySet(Player player)
        {
			ArmorIDs.Body.Sets.HidesTopSkin[Slot] = true;
        }
	}
}
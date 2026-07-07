using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.Legs)]
	public class InvokedCaligulaLegs : EquipTexture
	{
        public override void PreUpdateVanitySet(Player player)
        {
			ArmorIDs.Legs.Sets.HidesBottomSkin[Slot] = true;
        }
	}
}
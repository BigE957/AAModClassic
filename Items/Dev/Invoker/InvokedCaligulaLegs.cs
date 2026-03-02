using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev.Invoker
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
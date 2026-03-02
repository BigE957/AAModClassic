using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev.Invoker
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
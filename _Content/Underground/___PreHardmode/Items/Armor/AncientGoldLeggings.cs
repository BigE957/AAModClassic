using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;


namespace AAModClassic._Content.Underground.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class AncientGoldLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ancient Gold Greaves");
			// Tooltip.SetDefault(@"You have more chance to meet with vanilla gold critters.");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
            Item.height = 18;
            Item.defense = 4;
            Item.value = 15000;
			Item.expert = true;
			Item.expertOnly = true;
        }

        public override void UpdateEquip(Player player)
		{
            player.GetModPlayer<AAPlayer>().AncientGoldLeg = true;
		}
    }
}
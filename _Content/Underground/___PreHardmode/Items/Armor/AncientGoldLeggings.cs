using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ModLoader;


namespace AAModClassic._Content.Underground.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class AncientGoldLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.AncientGold";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ancient Gold Greaves");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
            Item.height = 18;
            Item.defense = 4;
            Item.value = 15000;
			Item.expert = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new AncientGoldLeggingsEffect(false));
        }
    }
}
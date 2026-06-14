using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.GoblinArmy.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class GoblinSlayersChestplate : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.GoblinSlayers";
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Goblin Slayer's Chestplate");
            // Tooltip.SetDefault(@"An immense hatred of Goblinkind haunts this chestplate");
        }

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 20;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 7;
        }
	}
}
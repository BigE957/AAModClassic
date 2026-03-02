using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.GoblinSlayer
{
    [AutoloadEquip(EquipType.Body)]
	public class GoblinSlayerChest : BaseAAItem
	{
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
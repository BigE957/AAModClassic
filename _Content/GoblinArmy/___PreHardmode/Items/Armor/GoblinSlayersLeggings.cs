using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.GoblinArmy.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class GoblinSlayersLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Goblin Slayer's Greaves");
            // Tooltip.SetDefault(@"An immense hatred of Goblinkind haunts these greaves");

        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.value = Item.sellPrice(0, 0, 5, 0);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 7;
		}
        
	}
}
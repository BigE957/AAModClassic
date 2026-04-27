using Terraria;
using Terraria.ID;

namespace AAModClassic.Items.Usable;

public class TerraKey : BaseAAItem
{
	public override void SetStaticDefaults()
	{
		//((ModItem)this).DisplayName.SetDefault("Keep Key");
		//((ModItem)this).Tooltip.SetDefault("A very ornate key");
	}

	public override void SetDefaults()
	{
		Item.width = Item.height = 16;
		Item.rare = ItemRarityID.Lime;
		Item.maxStack = Item.CommonMaxStack;
		Item.value = 0;
		Item.noMelee = true;
	}
}

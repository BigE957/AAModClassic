using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World;

public class TerraKey : BaseAAItem, ILocalizedModType
{
    public new string LocalizationCategory => "Items.Consumables";

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

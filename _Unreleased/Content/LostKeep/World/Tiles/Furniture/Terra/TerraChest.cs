using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraChest : BaseAAItem, ILocalizedModType
{
        public new string LocalizationCategory => "Items.Placeables.Furniture.Terra";
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Terra Chest");
	}

	public override void SetDefaults()
	{
		Item.width = 32;
		Item.height = 32;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.rare = ItemRarityID.White;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 500;
        Item.createTile = ModContent.TileType<TerraChest_Tile>();
	}

	public override void AddRecipes()
	{

		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.IronBar, 2);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 12);
		val.AddIngredient(ItemID.HallowedBar, 4);
		val.AddTile(TileID.WorkBenches);
		val.Register();
		Recipe val2 = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val2.AddIngredient(ItemID.LeadBar, 2);
		val2.AddIngredient(ModContent.ItemType<TerraShard>(), 12);
		val2.AddIngredient(ItemID.HallowedBar, 4);
		val2.AddTile(TileID.WorkBenches);
		val2.Register();
	}
}

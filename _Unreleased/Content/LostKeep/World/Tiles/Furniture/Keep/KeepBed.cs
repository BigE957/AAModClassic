using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepBed : ModItem, ILocalizedModType
{
        public new string LocalizationCategory => "Items.Placeables.Furniture.Keep";
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Keep Bed");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 250;
        Item.createTile = ModContent.TileType<KeepBed_Tile>();
	}

	public override void AddRecipes()
	{

		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 15);
		val.AddIngredient(ItemID.Silk, 5);
		val.AddTile(TileID.Sawmill);
		val.Register();
	}
}

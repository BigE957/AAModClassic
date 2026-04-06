using AAModClassic.Items.Materials;
using AAModClassic.Tiles.Furniture.Terra;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Terra;

public class TerraDresser : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Terra Dresser");
	}

	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 24;
		Item.maxStack = 99;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 250;
        Item.createTile = ModContent.TileType<AAModClassic.Tiles.Furniture.Terra.TerraDresser_Tile>();
	}

	public override void AddRecipes()
	{

		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 16);
		val.AddIngredient(ItemID.HallowedBar, 4);
		val.AddTile(TileID.Sawmill);
		val.Register();
	}
}

using AAModClassic.Items.Materials;
using AAModClassic.Tiles.Furniture.Terra;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Terra;

public class TerraBed : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Terra Bed");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.maxStack = 99;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 250;
        Item.createTile = ModContent.TileType<AAModClassic.Tiles.Furniture.Terra.TerraBed>();
	}

	public override void AddRecipes()
	{
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 15);
		val.AddIngredient(ItemID.HallowedBar, 4);
		val.AddIngredient(ItemID.Silk, 5);
		val.AddTile(TileID.Sawmill);
		val.Register();
	}
}

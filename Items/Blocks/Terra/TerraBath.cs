using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Terra;

public class TerraBath : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Terra Bathtub");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 26;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 250;
        Item.createTile = ModContent.TileType<TerraBath_Tile>();
	}

	public override void AddRecipes()
	{
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 14);
		val.AddIngredient(ItemID.HallowedBar, 4);
		val.AddTile(TileID.Sawmill);
		val.Register();
	}
}

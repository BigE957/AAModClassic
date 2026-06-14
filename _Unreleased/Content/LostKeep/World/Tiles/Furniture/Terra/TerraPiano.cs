using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraPiano : ModItem, ILocalizedModType
{
        public new string LocalizationCategory => "Items.Placeables";
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Terra Piano");
	}

	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 24;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 250;
		Item.createTile = ModContent.TileType<TerraPiano_Tile>();
	}

	public override void AddRecipes()
	{
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 15);
		val.AddIngredient(ItemID.HallowedBar, 4);
		val.AddIngredient(ItemID.Bone, 4);
		val.AddTile(TileID.WorkBenches);
		val.Register();
	}
}

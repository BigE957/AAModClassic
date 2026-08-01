using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepTable : ModItem, ILocalizedModType
{
    public new string LocalizationCategory => "Items.Placeables.Furniture.Keep";
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Keep Table");
	}

	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 26;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 250;
        Item.createTile = ModContent.TileType<KeepTable_Tile>();
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ModContent.ItemType<TerraShard>(), 8);
		recipe.AddTile(TileID.WorkBenches);
		recipe.Register();
	}
}

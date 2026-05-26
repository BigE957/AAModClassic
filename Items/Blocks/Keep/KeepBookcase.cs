using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Keep;

public class KeepBookcase : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Keep Bookcase");
	}

	public override void SetDefaults()
	{
		Item.width = 32;
		Item.height = 34;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 250;
        Item.createTile = ModContent.TileType<KeepBookcase_Tile>();
	}

	public override void AddRecipes()
	{

		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 20);
		val.AddIngredient(ItemID.Book, 10);
		val.AddTile(TileID.Sawmill);
		val.Register();
	}
}

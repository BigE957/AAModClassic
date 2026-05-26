using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Terra;

public class TerraTable : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Terra Table");
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
        Item.createTile = ModContent.TileType<TerraTable_Tile>();
	}

	public override void AddRecipes()
	{

		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 8);
		val.AddIngredient(ItemID.HallowedBar, 4);
		val.AddTile(TileID.WorkBenches);
		val.Register();
	}
}

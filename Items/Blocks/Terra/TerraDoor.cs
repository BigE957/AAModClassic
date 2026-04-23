using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic.Tiles.Furniture.Terra;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Terra;

public class TerraDoor : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Terra Door");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 34;
		Item.maxStack = 99;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 250;
		Item.createTile = ModContent.TileType<TerraDoorClosed_Tile>();
	}

	public override void AddRecipes()
	{

		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 6);
		val.AddIngredient(ItemID.HallowedBar, 4);
		val.AddTile(TileID.WorkBenches);
		val.Register();
	}
}

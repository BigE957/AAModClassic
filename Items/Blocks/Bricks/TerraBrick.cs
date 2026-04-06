using AAModClassic.Items.Materials;
using AAModClassic.Tiles.Bricks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Bricks;

public class TerraBrick : BaseAAItem
{
	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 16;
		Item.maxStack = 9999;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.createTile = ModContent.TileType<TerraBrickS_Tile>();
	}

	public override void SetStaticDefaults()
	{
		//((ModItem)this).DisplayName.SetDefault("Keep Brick");
	}

	public override void AddRecipes()
	{
		Recipe val = CreateRecipe(300);
		val.AddIngredient(ModContent.ItemType<KeepBrick>(), 300);
		val.AddIngredient(ModContent.ItemType<HeroShards>(), 1);
		val.AddTile(TileID.Furnaces);
		val.Register();
	}
}

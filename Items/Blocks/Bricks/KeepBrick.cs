using AAModClassic.Items.Materials;
using AAModClassic.Tiles.Bricks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Bricks;

public class KeepBrick : BaseAAItem
{
	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 16;
		Item.maxStack = 999;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.createTile = ModContent.TileType<KeepBrickS>();
	}

	public override void SetStaticDefaults()
	{
		//((ModItem)this).DisplayName.SetDefault("Keep Brick");
	}

	public override void AddRecipes()
	{
		Recipe val = CreateRecipe(1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 1);
		val.AddIngredient(ItemID.StoneBlock, 1);
		val.AddTile(TileID.Furnaces);
		val.Register();
	}
}

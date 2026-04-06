using AAModClassic.Items.Materials;
using AAModClassic.Tiles.Furniture.Keep;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Keep;

public class KeepLamp : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Keep Lamp");
	}

	public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 34;
		Item.maxStack = 99;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 250;
        Item.createTile = ModContent.TileType<AAModClassic.Tiles.Furniture.Keep.KeepLamp_Tile>();
	}

	public override void AddRecipes()
	{

		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 3);
		val.AddIngredient(ItemID.Torch, 1);
		val.AddTile(TileID.WorkBenches);
		val.Register();
	}
}

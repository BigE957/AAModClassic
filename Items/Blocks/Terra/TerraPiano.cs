using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Terra;

public class TerraPiano : ModItem
{
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
		Item.createTile = ModContent.TileType<KeepPiano_Tile>();
	}

	public override void AddRecipes()
	{

		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 15);
		val.AddIngredient(ItemID.HallowedBar, 4);
		val.AddIngredient(ItemID.Bone, 4);
		val.AddTile(TileID.WorkBenches);
		val.Register();
	}
}

using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepChandelier : ModItem, ILocalizedModType
{
        public new string LocalizationCategory => "Items.Placeables.Furniture.Keep";
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Keep Chandelier");
	}

	public override void SetDefaults()
	{
		Item.width = 40;
		Item.height = 38;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 250;
        Item.createTile = ModContent.TileType<KeepChandelier_Tile>();
	}

	public override void AddRecipes()
	{

		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ModContent.ItemType<TerraShard>(), 4);
		val.AddIngredient(ItemID.Torch, 4);
		val.AddIngredient(ItemID.Chain, 1);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}

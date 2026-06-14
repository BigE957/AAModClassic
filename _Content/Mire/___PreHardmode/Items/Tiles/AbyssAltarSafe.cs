using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Chaos.World.Tiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles
{
    public class AbyssAltarSafe : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyss Altar");
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
            Item.createTile = ModContent.TileType<ChaosAltarSafe_Tile>();
            Item.placeStyle = 0;
            Item.width = 28;
			Item.height = 24;
			Item.rare = ItemRarityID.Orange;
			Item.value = 1000;
			Item.accessory = false;
			Item.maxStack = Item.CommonMaxStack;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch.BossStandard;
using AAModClassic._Content.Inferno.__Hardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Void.__Hardmode.Items.Tiles;
using AAModClassic._Content.Mire.__Hardmode.Items.Tiles.Decoration;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Terrarium.__Hardmode.Items.Tiles
{
    public class TerrariumBox : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
            
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Terrarium)");
            // Tooltip.SetDefault("Plays ‘Resting Place’ by ProduceVGM");

        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<TerrariumBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
            
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBoxTitle);
            recipe.AddIngredient(ModContent.ItemType<MonarchBox>(), 1);
            recipe.AddIngredient(ModContent.ItemType<InfernoSurfaceBox>(), 1);
            recipe.AddIngredient(ModContent.ItemType<InfernoUndergroundBox>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MireSurfaceBox>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MireUndergroundBox>(), 1);
            recipe.AddIngredient(ModContent.ItemType<VoidBox>(), 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}

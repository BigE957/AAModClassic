using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.BossStandard
{
    public class AthenaBox : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables.MusicBoxes";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Athena)");
            // Tooltip.SetDefault(@"Plays 'Magisaint' by ENNWAY");
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<AthenaBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Yellow;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<GoddessFeather>(), 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}

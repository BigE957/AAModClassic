using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class AthenaBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Athena Music Box");
            // Tooltip.SetDefault(@"Plays 'Magisaint' by Turquoise");
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("AthenaBox").Type;
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
            recipe.AddIngredient(null, "GoddessFeather", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}

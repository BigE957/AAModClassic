using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Items.Blocks.Statues
{
	public class LizStatue : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Kitty Statue");
        }

        public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 50000;
			Item.rare = ItemRarityID.Blue;
			Item.createTile = Mod.Find<ModTile>("DevStatue").Type;
			Item.placeStyle = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();
        }
    }
}
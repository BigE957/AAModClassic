using Terraria;
using Terraria.ModLoader; using Terraria.ID;

namespace AAMod.Items.Blocks.Statues
{
	public class EnderStatue : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hero of Legend Statue");
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
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 50000;
			Item.rare = 1;
			Item.createTile = Mod.Find<ModTile>("DevStatue").Type;
			Item.placeStyle = 16;
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
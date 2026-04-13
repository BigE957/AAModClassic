using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.___Content.Mire.__Hardmode.Items.Materials;

namespace AAModClassic.Items.Usable
{
    public class KeyOfSpite : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Key of Spite");
			// Tooltip.SetDefault("'Charged with abyssal energy'");
		}

        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.White;
            Item.maxStack = 99;
            Item.value = 100;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = Item.useAnimation = 19;
            Item.noMelee = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }


    }
}

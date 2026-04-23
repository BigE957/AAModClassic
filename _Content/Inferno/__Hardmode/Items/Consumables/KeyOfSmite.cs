using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Consumables
{
    public class KeyOfSmite : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Key of Smite");
			// Tooltip.SetDefault("'Charged with flaming energy'");
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
            recipe.AddIngredient(ModContent.ItemType<SoulOfSmite>(), 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }


    }
}

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Consumables
{
    public class KeyOfSmite : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Consumables";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Key of Smite");
			// Tooltip.SetDefault("'Charged with flaming energy'");
		}


        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.White;
            Item.maxStack = Item.CommonMaxStack;
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

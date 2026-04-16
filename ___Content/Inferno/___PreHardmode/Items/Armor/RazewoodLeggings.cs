using AAModClassic.Items.Blocks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.___Content.Inferno.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class RazewoodLeggings : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood Boots");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 18;
            Item.value = 100;
            Item.rare = ItemRarityID.White;
            Item.defense = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Razewood>(), 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
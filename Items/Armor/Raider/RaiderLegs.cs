using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Raider
{
    [AutoloadEquip(EquipType.Legs)]
	public class RaiderLegs : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Raider Greaves");
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 0, 5, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 12;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("VikingBoots").Type);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(Mod.Find<ModItem>("HydraHide").Type, 6);
            recipe.AddIngredient(Mod.Find<ModItem>("Doomite").Type, 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
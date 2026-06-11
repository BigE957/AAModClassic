using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class RadiumLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Radium Cuisses");
			/* Tooltip.SetDefault(@"30% increased movement speed
Shines with the light of a starry night sky"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = 300000;
			Item.defense = 20;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.3f;
			player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .3f;
            Lighting.AddLight(player.Center, 1.0f, 1.0f, 1.0f);
        }

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 27);
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 15);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
}
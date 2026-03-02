using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Doomite
{
    [AutoloadEquip(EquipType.Body)]
	public class DoomiteBreastplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomite Plate");
            // Tooltip.SetDefault(@"+1 Minion slot");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.rare = ItemRarityID.LightRed;
            Item.defense = 7;
            Item.value = 9000;
		}

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DoomiteUPlate");
            recipe.AddIngredient(null, "Doomite", 10);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ItemID.FossilOre, 8);
            recipe.AddIngredient(null, "BroodScale", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
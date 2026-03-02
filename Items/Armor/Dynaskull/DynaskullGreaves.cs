using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Dynaskull
{
    [AutoloadEquip(EquipType.Legs)]
	public class DynaskullGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dynaskull Greaves");
            // Tooltip.SetDefault("12% Increased ranged critical chance");

        }

		public override void SetDefaults()
		{
            Item.width = 30;
			Item.height = 28;
			Item.value = 90000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Ranged) += 12;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FossilPants, 1);
            recipe.AddIngredient(null, "DynaskullOre", 15);
            recipe.AddIngredient(null, "Doomite", 6);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(null, "BroodScale", 6);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
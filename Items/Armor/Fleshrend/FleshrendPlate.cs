using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Fleshrend
{
    [AutoloadEquip(EquipType.Body)]
	public class FleshrendPlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Fleshrend Plate");
			// Tooltip.SetDefault("7% Increased melee damage");
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 34;
			Item.value = 90000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 9;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += .07f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimsonScalemail, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddIngredient(ItemID.Bone, 8);
            recipe.AddIngredient(null, "DevilSilk", 8);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}
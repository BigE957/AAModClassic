using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Terra
{
	[AutoloadEquip(EquipType.Head)]
	public class TerraHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Helm");
			/* Tooltip.SetDefault(@"22% increased melee damage
9% increased melee speed"); */
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 26;
			Item.value = 90000;
			Item.rare = ItemRarityID.Lime;
			Item.defense = 30;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Melee) += .22f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.09f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("TerraPlate").Type && legs.type == Mod.Find<ModItem>("TerraGreaves").Type;
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.TerraHelmBonus");

			player.moveSpeed += 0.28f;
			player.GetModPlayer<AAPlayer>().TerraMe = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe;
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "NightsHelm", 1);
			recipe.AddIngredient(null, "TerraCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "FleshrendHelm", 1);
			recipe.AddIngredient(null, "TerraCrystal", 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
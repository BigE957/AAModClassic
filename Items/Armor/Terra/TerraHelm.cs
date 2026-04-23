using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Items.Materials;
using AAModClassic._Content.Crimson.___PreHardmode.Items.Armor;
using AAModClassic._Content.Corruption.___PreHardmode.Items.Armor;


namespace AAModClassic.Items.Armor.Terra
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
			return body.type == ModContent.ItemType<TerraPlate>() && legs.type == ModContent.ItemType<TerraGreaves>();
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
			recipe.AddIngredient(ModContent.ItemType<NightsHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerraPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<FleshrendHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerraPrism>(), 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
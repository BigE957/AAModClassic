using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic._Content.Crimson.___PreHardmode.Items.Armor;
using AAModClassic._Content.Corruption.___PreHardmode.Items.Armor;
using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;


namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class TerraHelmetMelee : BaseAAItem
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
			return body.type == ModContent.ItemType<TerraChestplate>() && legs.type == ModContent.ItemType<TerraLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = FilePathUtils.SetBonusPath<TerraHelmetMelee>();

			player.GetAttackSpeed(DamageClass.Melee) += 0.28f;
			player.GetModPlayer<TerraHelmetMeleePlayer>().effect = true;
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
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAModClassic.Items.Armor.Nights
{
    [AutoloadEquip(EquipType.Head)]
	public class NightsHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Night's Helm");
			// Tooltip.SetDefault("9% increased melee speed");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 28;
			Item.value = 90000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 6;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.GetAttackSpeed(DamageClass.Melee) += 0.09f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<NightsPlate>() && legs.type == ModContent.ItemType<NightsGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.NightsHelmBonus");
            player.moveSpeed += 0.22f;
            player.panic = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ShadowHelmet, 1);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.AddIngredient(ItemID.Bone, 5);
			recipe.AddIngredient(null, "DevilSilk", 5);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}
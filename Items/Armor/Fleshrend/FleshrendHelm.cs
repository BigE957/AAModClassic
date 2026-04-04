using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic;


namespace AAModClassic.Items.Armor.Fleshrend
{
    [AutoloadEquip(EquipType.Head)]
	public class FleshrendHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fleshrend Helm");
			// Tooltip.SetDefault("7% increased melee damage");

		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 26;
			Item.value = 90000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 7;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Melee) += .07f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<FleshrendPlate>() && legs.type == ModContent.ItemType<FleshrendGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.FleshrendHelmBonus");

            player.crimsonRegen = true;
			player.GetModPlayer<AAPlayer>().fleshrendSet = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CrimsonHelmet, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddIngredient(null, "DevilSilk", 5);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}
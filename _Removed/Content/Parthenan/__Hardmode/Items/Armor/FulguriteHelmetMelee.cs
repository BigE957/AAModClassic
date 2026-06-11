using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class FulguriteHelmetMelee : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fulgurite Helmet");
			// Tooltip.SetDefault(@"10% increased melee damage, critical strike chance, and melee speed");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 50000;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 22;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Melee) *= 1.12f;
            player.GetCritChance(DamageClass.Melee) += 12;
            player.GetAttackSpeed(DamageClass.Melee) *= 1.10f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<FulguriteChestplate>() && legs.type == ModContent.ItemType<FulguriteLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = @"Being struck causes a burst of lightning to erupt from your body, knocking back enemies
+20% increased melee and movement speed";

            player.GetModPlayer<FulguriteArmorPlayer>().FulguriteArmorSetBonus = true;
            player.GetAttackSpeed(DamageClass.Melee) *= 1.20f;
            player.moveSpeed *= 1.20f;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
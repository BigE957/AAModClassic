using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class FulguriteHelmetRanged : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fulgurite Visor");
			/* Tooltip.SetDefault(@"17% increased ranged damage
5% increased ranged critical strike chance"); */

		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 50000;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 8;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Ranged) *= 1.17f;
            player.GetCritChance(DamageClass.Ranged) += 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == ModContent.ItemType<FulguriteChestplate>() && legs.type == ModContent.ItemType<FulguriteLeggings>();
        }

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = @"Being struck causes a burst of lightning to erupt from your body, knocking back enemies
25% chance to not consume ammo weapons";

            player.GetModPlayer<FulguriteArmorPlayer>().FulguriteArmorSetBonus = true;
            player.ammoCost75 = true;
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
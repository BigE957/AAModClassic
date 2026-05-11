using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class FulguriteLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fulgurite Pants");
            /* Tooltip.SetDefault(@"5% increased critical chance
16% increased movement speed"); */

        }

		public override void SetDefaults()
		{
            Item.width = 22;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 11;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Throwing) += 5;
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetCritChance(DamageClass.Magic) += 5;
            player.GetCritChance(DamageClass.Ranged) += 5;
            player.moveSpeed *= 1.16f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 18);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
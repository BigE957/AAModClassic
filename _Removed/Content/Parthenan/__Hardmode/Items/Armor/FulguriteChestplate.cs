using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class FulguriteChestplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fulgurite Breastplate");
            // Tooltip.SetDefault("7% Increased Damage");
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 40000;
			Item.rare = 5;
			Item.defense = 17;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Throwing) *= 1.07f;
            player.GetDamage(DamageClass.Melee) *= 1.07f;
            player.GetDamage(DamageClass.Ranged) *= 1.07f;
            player.GetDamage(DamageClass.Magic) *= 1.07f;
            player.GetDamage(DamageClass.Summon) *= 1.07f;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 24);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
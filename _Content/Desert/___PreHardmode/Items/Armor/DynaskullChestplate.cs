using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Desert.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class DynaskullChestplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Dynaskull Ribguard");
            // Tooltip.SetDefault("13% increased ranged damage");
        }

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.value = 90000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Ranged) += 0.13f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FossilShirt, 1);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 8);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 8);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
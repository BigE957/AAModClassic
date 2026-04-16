using AAModClassic.___Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.Items.Boss.Broodmother;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.___Content.Inferno.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class KindledChestplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Kindled Dao");
			// Tooltip.SetDefault("Forged in the flames of the blazing sun");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.value = 6000;
			Item.rare = ItemRarityID.Green;
			Item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.buffImmune[BuffID.OnFire] = true;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 25);
            recipe.AddIngredient(ModContent.ItemType<BroodScale>(), 20);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
using AAModClassic.Items.Boss.MushroomMonarch;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.___Content.GlowingMushroom.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class GlowingMushiumChestplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Glowing Mushium Shirt");
            // Tooltip.SetDefault("2% increased mana regeneration");
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 50;
			Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
            Item.value = Item.sellPrice(0, 0, 25, 0);
		}

		public override void UpdateEquip(Player player)
        {
            player.manaRegenBonus += 2;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GlowingMushiumBar>(), 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
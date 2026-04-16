using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Items.Boss.Broodmother;
using AAModClassic.___Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.___Content.Snow.___PreHardmode.Items.Materials;

namespace AAModClassic.___Content.Ocean.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class AtlanteanChestplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Atlantean Chestplate");
			/* Tooltip.SetDefault(@"Increases magic damage by 15%
It vibrates with the powers of Atlantis"); */

		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice (0, 0, 5, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 8;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Magic) += 0.15f;
		}
		
		public override void AddRecipes()
		{
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OceanChestplate>());
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 8);
            recipe.AddIngredient(ModContent.ItemType<RelicBar>(), 8);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OceanChestplate>());
            recipe.AddIngredient(ModContent.ItemType<BroodScale>(), 8);
            recipe.AddIngredient(ItemID.FossilOre, 8);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
		}
	}
}
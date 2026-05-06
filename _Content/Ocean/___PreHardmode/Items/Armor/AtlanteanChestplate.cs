using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Armor
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
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 8);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 16);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
		}
	}
}
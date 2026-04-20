using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.___Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.___Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic.___Content.Inferno.___PreHardmode.Items.Materials;

namespace AAModClassic.___Content.Ocean.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class AtlanteanLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Atlantean Greaves");
            /* Tooltip.SetDefault(@"Increases magic critical strike chance by 10%
Allows to freely move in liquids"); */

        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 0, 5, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 6;
		}
        
		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Magic) += 10;
            player.accFlipper = true;
			player.ignoreWater = true;
		}
		
		public override void AddRecipes()
		{
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OceanLeggings>());
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 6);
            recipe.AddIngredient(ModContent.ItemType<RelicBar>(), 6);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 6);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OceanLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 6);
            recipe.AddIngredient(ItemID.FossilOre, 6);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();

        }
	}
}
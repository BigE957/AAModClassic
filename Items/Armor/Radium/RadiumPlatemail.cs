using AAModClassic;
using AAModClassic.Globals;
using AAModClassic.Items.Materials;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Body)]
	public class RadiumPlatemail : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Radium Platemail");
			// Tooltip.SetDefault("25% increased damage \n" + "Shines with the light of a starry night sky");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 300000;
			Item.defense = 28;
			Item.rare = ItemRarityID.Cyan;
			AARarity = 12;
		}

		public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.Mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.OverrideColor = AAColor.Rarity12;
				}
			}
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += .25f;
            Lighting.AddLight(player.Center, 1.0f, 1.0f, 1.0f);
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 30);
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 20);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
			recipe.Register();
		}
	}
}
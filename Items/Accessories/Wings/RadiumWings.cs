using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
	public class RadiumWings : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radium Wings");
            // Tooltip.SetDefault("Allows flight and slow fall");

            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(200, 10, 3f);
        }

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 40;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Purple;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 200;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.95f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 4f;
			constantAscend = 0.17f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "RadiumBar", 10);
            recipe.AddIngredient(null, "Stardust", 15);
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.Register();
		}
	}
}
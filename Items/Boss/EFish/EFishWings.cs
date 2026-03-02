using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.EFish
{

    [AutoloadEquip(EquipType.Wings)]
    public class EFishWings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Emperor Fishron Wings");
            // Tooltip.SetDefault("Allows flight and slow fall");
		}

		public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 42;
			Item.value = 500000;
			Item.rare = 6;
			Item.accessory = true;
		}
        
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 270;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 9f;
			acceleration *= 2.5f;
		}



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FishronWings);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

    }
}

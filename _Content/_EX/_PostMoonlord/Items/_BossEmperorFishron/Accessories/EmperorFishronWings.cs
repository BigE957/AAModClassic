using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items._BossEmperorFishron.Accessories
{

    [AutoloadEquip(EquipType.Wings)]
    public class EmperorFishronWings : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
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
			Item.rare = ItemRarityID.LightPurple;
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
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }

    }
}

using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
	public class ZeroWings : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zero Jet");
            // Tooltip.SetDefault("Allows flight and slow fall");

            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(220, 14, 3.5f);
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Green;
			Item.accessory = true;
            
        }
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 220;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.95f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 4f;
			constantAscend = 0.135f;
		}

        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                player.wingFrameCounter++;
                if (player.wingFrameCounter >= 6)
                {
                    player.wingFrameCounter = 0;
                }
                player.wingFrame = 1 + player.wingFrameCounter / 2;
            }
            else
            {
                player.wingFrame = 0;
            }
            return true;
        }

        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 15);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration.OuroborosWoodFurniture
{
    public class OuroborosWoodChest : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables.Furniture.OuroborosWood";
        
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Ouroboros Wood Chest");
        }

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
            Item.rare = ItemRarityID.Pink;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 500;
			Item.createTile = ModContent.TileType<OuroborosWoodChest_Tile>();
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(100, 0, 10);
                }
            }
        }

        public override void AddRecipes()
		{
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<DoomiteScrap>(), 2);
                recipe.AddIngredient(ModContent.ItemType<OuroborosWood>(), 12);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}
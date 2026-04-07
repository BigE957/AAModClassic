using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Tiles.Bricks;
using AAModClassic.Tiles.Crafters;

namespace AAModClassic.Items.Blocks.Bricks
{
    public class DoomstoneBrick : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<DoomstoneBrick_Tile>(); //put your CustomBlock Tile name
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomstone Brick");
            // Tooltip.SetDefault("");
           
        }
        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<Doomstone>(), 3);
                recipe.AddTile(ModContent.TileType<ACS_Tile>());
                recipe.Register();
            }
        }
    }
}

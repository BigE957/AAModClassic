using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using AAModClassic.Globals;
using AAModClassic.Tiles.Boxes;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class Zero2Box : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zero Awakened Music Box");
            // Tooltip.SetDefault("Plays 'Doomsday Arrives' by Saucecoie");
        }
		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Zero2Box_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
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

        public override void AddRecipes()
        {
            if (Main.expertMode == true)
            { 
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.MusicBox);
                recipe.AddIngredient(null, "ZeroBox");
                recipe.AddIngredient(null, "BrokenCode");
                recipe.AddTile(TileID.Sawmill);
                recipe.Register();
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using AAModClassic.Globals;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class VoidBox : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Music Box");
            // Tooltip.SetDefault(@"Plays 'Gaze into Darkness' by Charlie Debnam");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<VoidBox_Tile>();
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

                    line2.OverrideColor = new Color(100, 0, 10);

                    line2.OverrideColor = AAColor.Rarity13;
//
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "OroborosWood", 20);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}

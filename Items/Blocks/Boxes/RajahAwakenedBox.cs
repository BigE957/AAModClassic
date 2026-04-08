
using AAModClassic.Globals;
using AAModClassic.Items.Boss;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class RajahAwakenedBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Champion of the Innocent Music Box");
            //Tooltip.SetDefault(@"Plays '' by ");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void SetDefaults()
		{
            Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<AAModClassic.Tiles.Boxes.RajahAwakenedBox_Tile>();
			Item.width = 24;
			Item.height = 24;
            Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<RajahBox>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}

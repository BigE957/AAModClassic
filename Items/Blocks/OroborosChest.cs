using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class OroborosChest : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Oroboros Chest");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
            Item.rare = ItemRarityID.Pink;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 500;
			Item.createTile = Mod.Find<ModTile>("OroborosChest").Type;
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
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "DoomiteScrap", 2);
                recipe.AddIngredient(null, "OroborosWood", 12);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}
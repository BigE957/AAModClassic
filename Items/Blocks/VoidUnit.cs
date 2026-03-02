using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class VoidUnit : BaseAAItem
	{

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Recreation Unit");
        }

        public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.rare = 10;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.createTile = Mod.Find<ModTile>("VoidUnit").Type;
            Item.rare = 10;
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
			recipe.AddIngredient(null, "ApocalyptitePlate", 15);
			recipe.AddTile(null, "ACS");
			recipe.Register();
		}
	}
}
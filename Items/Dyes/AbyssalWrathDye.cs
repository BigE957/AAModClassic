using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Dyes
{
	public class AbyssalWrathDye : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Abyssal Wrath Dye");
            BaseUtility.AddTooltips(Item, new string[] { "Gives an abyssal touch to whatever this dye is applied to" });		
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.YamataA;
                }
            }
        }
        
        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 15;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Yellow;
			Item.dye = (byte)GameShaders.Armor.GetShaderIdFromItemId(Item.type); 
            Item.value = BaseUtility.CalcValue(0, 10, 0, 0);			
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AbyssalDye", 2);
            recipe.AddIngredient(Terraria.ID.ItemID.BottledWater);
            recipe.AddTile(Terraria.ID.TileID.DyeVat);
            recipe.Register();
        }
    }
}
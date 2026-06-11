using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Mire._PostMoonlord.Items.Dyes
{
	public class AbyssalDye : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Abyssal Dye");
            // Tooltip.SetDefault("Gives an abyssal touch to whatever this dye is applied to");		
        }

        
        
        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 15;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Yellow;
			Item.dye = (byte)GameShaders.Armor.GetShaderIdFromItemId(Item.type); 
            Item.value = Item.sellPrice(0, 10, 0, 0);			
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 3);
            recipe.AddIngredient(Terraria.ID.ItemID.BottledWater);
            recipe.AddTile(Terraria.ID.TileID.DyeVat);
            recipe.Register();
        }
    }
}
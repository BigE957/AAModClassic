using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno._PostMoonlord.Items.Dyes
{
    public class BlazingFuryDye : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blazing Fury Dye");
            BaseUtility.AddTooltips(Item, new string[] { "Gives a blazing touch to whatever this dye is applied to" });
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.AkumaA;
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
            Item.value = Item.sellPrice(0, 10, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BlazingDye>(), 2);
            recipe.AddTile(TileID.DyeVat);
            recipe.Register();
        }
    }
}
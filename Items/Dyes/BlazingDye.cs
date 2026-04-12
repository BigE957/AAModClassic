using AAModClassic.___Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Dyes
{
    public class BlazingDye : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blazing Dye");
            BaseUtility.AddTooltips(Item, new string[] { "Gives a blazing touch to whatever this dye is applied to" });
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
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 3);
            recipe.AddIngredient(Terraria.ID.ItemID.BottledWater);
            recipe.AddTile(Terraria.ID.TileID.DyeVat);
            recipe.Register();
        }
    }
}
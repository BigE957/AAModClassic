using AAModClassic._Content.Inferno._PostMoonlord.Items.Dyes;
using AAModClassic._Content.Mire._PostMoonlord.Items.Dyes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Dyes
{
    public class DiscordianDye : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discordian Dye");
            // Tooltip.SetDefault("Gives a discordian touch to whatever this dye is applied to");
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
Recipe recipe = CreateRecipe(2);
recipe.AddIngredient(ModContent.ItemType<BlazingDye>(), 1);
recipe.AddIngredient(ModContent.ItemType<AbyssalDye>(), 1);
recipe.AddTile(TileID.DyeVat);
recipe.Register();
}
}
}
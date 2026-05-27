using AAModClassic._Content.Inferno._PostMoonlord.Items.Dyes;
using AAModClassic._Content.Mire._PostMoonlord.Items.Dyes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Dyes
{
    public class DiscordianInfernoDye : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discordian Inferno Dye");
            // Tooltip.SetDefault("Gives a discordian touch to whatever this dye is applied to");
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
            recipe.AddIngredient(ModContent.ItemType<BlazingFuryDye>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AbyssalWrathDye>(), 1);
            recipe.AddTile(TileID.DyeVat);
            recipe.Register();
        }
    }
}
using System.Collections.Generic;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Boss.Zero;

namespace AAModClassic.Items.Dyes
{
    public class DoomsdayDye : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomsday Dye");
            BaseUtility.AddTooltips(Item, new string[] { "Adds a glitchy-look to whatever this dye is applied to" });
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 7));
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
            Item.value = BaseUtility.CalcValue(0, 10, 0, 0);
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 3);
            recipe.AddIngredient(Terraria.ID.ItemID.BottledWater);
            recipe.AddTile(Terraria.ID.TileID.DyeVat);
            recipe.Register();
        }
    }
}
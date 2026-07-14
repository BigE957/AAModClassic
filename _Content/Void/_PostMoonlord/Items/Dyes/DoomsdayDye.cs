using System.Collections.Generic;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Dyes
{
    public class DoomsdayDye : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Dyes";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomsday Dye");
            // Tooltip.SetDefault("Adds a glitchy-look to whatever this dye is applied to");
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(4, 7));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
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
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 3);
            recipe.AddTile(TileID.DyeVat);
            recipe.Register();
        }
    }
}
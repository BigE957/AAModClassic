using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;

namespace AAModClassic._Content.Terrarium.World.Tiles
{
    public class TerraLeafWand : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Leaf Wand");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LivingWoodWand);
            Item.createTile = ModContent.TileType<TerraLeaves_Tile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TerraCrystal>(), 20);
            recipe.AddIngredient(ItemID.LeafWand);
            recipe.AddTile(ModContent.TileType<TruePaladinsSmeltery_Tile>());
            recipe.Register();
        }
    }
}

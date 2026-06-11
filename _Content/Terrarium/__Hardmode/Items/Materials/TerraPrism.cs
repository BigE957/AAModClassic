using AAModClassic._Content._Misc.__Hardmode.Items.Materials;
using AAModClassic._Content.Sky.__Hardmode.Items.Materials;
using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic._Content.Underground.___PreHardmode.Items.Materials;
using AAModClassic.Assets;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.__Hardmode.Items.Materials
{
    public class TerraPrism : BaseAAItem
    {
        public override string Texture => AssetDirectory.Items.BiomePrism;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Prism");
            // Tooltip.SetDefault("Imbued with the unified harmony of the land of Terraria");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 10000;
            Item.rare = ItemRarityID.Yellow;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.TerraGlow;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, AAColor.TerraGlow.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            //Dropped by Biomite Core in Mixed and Beta
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TerraShard>(), 5);
            recipe.AddIngredient(ModContent.ItemType<BiomePrism>());
            recipe.AddTile(ModContent.TileType<TerraPrism_Tile>());
            recipe.AddCondition(Language.GetText("Mods.AAModClassic.Commoon.Conditions.ReleaseExclusive"), () => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased));
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DragonSpirit>(), 5);
            recipe.AddIngredient(ModContent.ItemType<BiomePrism>());
            recipe.AddTile(ModContent.TileType<TerraPrism_Tile>());
            recipe.AddCondition(Language.GetText("Mods.AAModClassic.Commoon.Conditions.ReleaseExclusive"), () => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased));
            recipe.Register();
        }
    }
}
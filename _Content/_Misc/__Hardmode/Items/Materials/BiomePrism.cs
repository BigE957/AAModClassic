using AAModClassic._Content.Hallow.__Hardmode.Items.Tiles.Functional;
using AAModClassic._Content.Terra._PostMoonLord.Items.Tiles.Functional;
using AAModClassic._Content.Underground.___PreHardmode.Items.Materials;
using AAModClassic.Assets;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Materials
{
    public class BiomePrism : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override string Texture => AssetDirectory.Items.BiomePrism;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Biome Prism");
            // Tooltip.SetDefault("A magical prism that can be enhanced with the power of a biome.");
            Item.ResearchUnlockCount = 5;
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
            return AAColor.COLOR_WHITEFADE1;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, AAColor.COLOR_WHITEFADE1.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Prism>(), 5);
            recipe.AddTile(ModContent.TileType<HallowedForge_Tile>());
            recipe.Register();
        }
    }
}
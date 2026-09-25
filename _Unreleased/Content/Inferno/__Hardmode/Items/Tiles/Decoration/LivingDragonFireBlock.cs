using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Inferno.__Hardmode.Items.Tiles.Decoration
{
    public class LivingDragonFireBlock : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public static Vector3 LightColor = new Vector3(0.9f, 0.3f, 0.2f);

        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<LivingDragonFireBlock_Tile>());
            Item.width = 12;
            Item.height = 12;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, LightColor);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(20);
            recipe.AddIngredient(ItemID.LivingFireBlock, 20);
            recipe.AddIngredient(ModContent.ItemType<DragonFire>());
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddCondition(Language.GetOrRegister("Mods.AAModClassic.Common.Conditions.Unofficial"), () => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial));
            recipe.SortAfterFirstRecipesOf(ItemID.LivingUltrabrightFireBlock);
            recipe.Register();
        }
    }
}

using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus.Accessories;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch.Accessories;
using AAModClassic._Unreleased.Content.Parthenan.__Hardmode.Items._BossTechnoTruffle.Accessories;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Mire.__Hardmode.Items.Tiles.Decoration
{
    public class LivingBogtoxinBlock : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public static Vector3 LightColor = new Vector3(0.8f, 0.2f, 0.9f);

        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<LivingBogtoxinBlock_Tile>());
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
            recipe.AddIngredient(ModContent.ItemType<Bogtoxin>());
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddCondition(ConditionUtils.Unofficial);
            recipe.SortAfterFirstRecipesOf(ItemID.LivingUltrabrightFireBlock);
            recipe.Register();
        }
    }
}

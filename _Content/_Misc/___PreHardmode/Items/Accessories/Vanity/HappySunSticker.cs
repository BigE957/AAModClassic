using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.___PreHardmode.Items.Accessories.Vanity
{
    public class HappySunSticker : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity";
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Happy Sun Sticker");
            // Tooltip.SetDefault(@":D");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sunglasses);
            recipe.AddIngredient(ItemID.SunplateBlock, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
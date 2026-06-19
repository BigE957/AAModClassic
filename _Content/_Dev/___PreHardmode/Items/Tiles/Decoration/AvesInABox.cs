using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.___PreHardmode.Items.Tiles.Decoration
{
    public class AvesInABox : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Aves In A Box");
            //Tooltip.SetDefault("'MAKE MORE MUSIC BOXES YOU DAMN DUCK'");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<AvesInABox_Tile>();
            Item.placeStyle = 1;
            Item.width = 28;
            Item.height = 24;
            Item.rare = ItemRarityID.Orange;
            Item.value = 1000;
            Item.accessory = false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Duck, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.Register();
        }
    }
}


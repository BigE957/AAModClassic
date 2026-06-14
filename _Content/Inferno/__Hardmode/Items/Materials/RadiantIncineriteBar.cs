using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Materials
{
    public class RadiantIncineriteBar : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetDefaults()
        {

            Item.width = 30;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<RadiantIncineriteBar_Tile>();
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radiant Incinerite Bar");
            // Tooltip.SetDefault("You can barely look at it, it's so bright");
        }

		public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 1);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Autohammer);
            recipe.Register();
        }
    }
}

using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration
{
    public class DoomsdayCircuitPlating : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomsday Circuit Plating");
        }

        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<DoomsdayCircuitPlating_Tile>(); //put your CustomBlock Tile name
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 1);
            recipe.AddIngredient(ItemID.StoneBlock, 5);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomsdayPlatingWall>(), 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}

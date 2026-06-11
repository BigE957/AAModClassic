using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Void.World.Tiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration
{
    public class DoomstoneBrick : BaseAAItem
    {
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
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.consumable = true;
            Item.createTile = ModContent.TileType<DoomstoneBrick_Tile>(); //put your CustomBlock Tile name
        }

        

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomstone Brick");
            // Tooltip.SetDefault("");
           
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Doomstone>(), 2);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomstoneBrickWall>(), 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}

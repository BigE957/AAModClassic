using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Chaos.World.Tiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles
{
    public class DragonAltarSafe : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Dragon Altar");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ChaosAltarSafe_Tile>();
            Item.placeStyle = 1;
            Item.width = 28;
            Item.height = 22;
            Item.rare = ItemRarityID.Orange;
            Item.value = 1000;
            Item.accessory = false;
            Item.maxStack = Item.CommonMaxStack;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}


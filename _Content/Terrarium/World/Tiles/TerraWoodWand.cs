using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Globals;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic._Content.Hallow.__Hardmode.Items.Tiles.Functional;

namespace AAModClassic._Content.Terrarium.World.Tiles
{
    public class TerraWoodWand : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Wood Wand");
            /* Tooltip.SetDefault(@"Right click to swap modes"); */
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LivingWoodWand);
            Item.createTile = ModContent.TileType<TerraWood_Tile>();
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Research);
            bool favorited = Item.favorited;
            Item.SetDefaults(ModContent.ItemType<PermeableTerraWoodWand>());
            Item.stack++;
            Item.favorited = favorited;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TerraCrystal>(), 20);
            recipe.AddIngredient(ItemID.LivingWoodWand);
            recipe.AddTile(ModContent.TileType<TruePaladinsSmeltery_Tile>());
            recipe.Register();
        }
    }
}

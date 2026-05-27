using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Tiles.Boxes;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Materials;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class FungusBox : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Feudal Fungus)");
            // Tooltip.SetDefault("Plays 'Bioluminescent Beatdown' by Spectral Aves");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<FungusBox_Tile>();
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 10000;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<GlowingMushium>(), 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}


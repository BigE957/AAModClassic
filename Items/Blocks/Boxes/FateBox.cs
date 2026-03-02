using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class FateBox : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Void Music Box");
            // Tooltip.SetDefault("Plays 'Nihil' by Charlie Debnam");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("FateBox").Type;
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
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}

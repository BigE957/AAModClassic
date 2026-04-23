using AAModClassic.___Content.Snow.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Snow.___PreHardmode.Items.Tiles.Decoration
{
    public class IceCrate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Crate");
            // Tooltip.SetDefault("'It's too frigid to open'");

            Item.ResearchUnlockCount = 5;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<IceCrate_Tile>());
            Item.width = Item.height = 32;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WoodenCrate, 1);
            recipe.AddIngredient(ModContent.ItemType<SnowMana>(), 5);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();
        }
    }
}
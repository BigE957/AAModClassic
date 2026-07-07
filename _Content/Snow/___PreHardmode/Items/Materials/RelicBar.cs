using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Snow.___PreHardmode.Items.Materials
{
    public class RelicBar : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Relic Bar");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<RelicBar_Tile>();
            Item.value = Item.sellPrice(0, 0, 32, 0);
        }

        public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VikingRelic>(), 2);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}

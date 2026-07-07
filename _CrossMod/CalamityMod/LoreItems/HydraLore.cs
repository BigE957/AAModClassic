using AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.BossStandard;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod.LoreItems
{
    public class HydraLore : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HydraTrophy>());
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}

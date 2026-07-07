using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.BossStandard;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod.LoreItems
{
    public class AthenaLore : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Lime;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AthenaTrophy>());
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}

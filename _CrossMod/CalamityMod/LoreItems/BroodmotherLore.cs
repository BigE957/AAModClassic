using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.BossStandard;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod.LoreItems
{
    public class BroodmotherLore : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BroodmotherTrophy>());
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}

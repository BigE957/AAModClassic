using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.BossStandard;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod.LoreItems
{
    public class GripsOfChaosLore : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.consumable = false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<InfernoGripTrophy>());
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<MireGripTrophy>());
            recipe2.AddTile(TileID.Bookcases);
            recipe2.Register();
        }
    }
}

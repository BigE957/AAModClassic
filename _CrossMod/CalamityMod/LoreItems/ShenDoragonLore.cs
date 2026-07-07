using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.BossStandard;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod.LoreItems
{
    public class ShenDoragonLore : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShenDoragonTrophy>());
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<ShenDoragonATrophy>());
            recipe2.AddTile(TileID.Bookcases);
            recipe2.Register();
        }
    }
}

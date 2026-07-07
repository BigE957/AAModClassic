using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.BossStandard;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod.LoreItems
{
    internal class ZeroLore : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.consumable = false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ZeroTrophy>());
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<ZeroATrophy>());
            recipe2.AddTile(TileID.Bookcases);
            recipe2.Register();
        }
    }
}

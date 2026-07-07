using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod.LoreItems
{
    public class YamataLore : LoreItem
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
            recipe.AddIngredient(ModContent.ItemType<YamataTrophy>());
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<YamataATrophy>());
            recipe2.AddTile(TileID.Bookcases);
            recipe2.Register();
        }
    }
}

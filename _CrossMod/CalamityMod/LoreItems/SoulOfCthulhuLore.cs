using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.BossStandard;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod.LoreItems
{
    public class SoulOfCthulhuLore : LoreItem
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
            recipe.AddIngredient(ModContent.ItemType<SoulOfCthulhuTrophy>());
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}

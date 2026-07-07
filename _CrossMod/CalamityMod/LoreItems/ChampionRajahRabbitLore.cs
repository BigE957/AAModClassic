using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.BossStandard;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod.LoreItems
{
    public class ChampionRajahRabbitLore : LoreItem
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
            recipe.AddIngredient(ModContent.ItemType<RajahRabbitTrophy>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}

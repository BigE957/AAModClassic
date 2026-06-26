using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.BossStandard;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod.LoreItems
{
    public class GreedLore : LoreItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Greed");
        }

        /*
        public override void UpdateInventory(Player player)
        {
            if (!CalamityMod.IsEnabled)
                Item.TurnToAir();
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (!CalamityMod.IsEnabled)
                Item.active = false;
        }
        */

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Lime;
            Item.consumable = false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GreedTrophy>());
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
}
using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Depth
{
    [AutoloadEquip(EquipType.Legs)]
    public class DepthHakama : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Depth Hakama");
            /* Tooltip.SetDefault(@"15% increased movement speed
Weightless as shadow itself"); */
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 5000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.15f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AbyssiumBar", 20);
            recipe.AddIngredient(null, "HydraHide", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
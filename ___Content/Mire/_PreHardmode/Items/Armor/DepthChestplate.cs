using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Depth
{
    [AutoloadEquip(EquipType.Body)]
    public class DepthGi : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Depth Gi");
            /* Tooltip.SetDefault(@"40% increased movement speed
Weightless as shadow itself"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += .40f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AbyssiumBar", 25);
            recipe.AddIngredient(null, "HydraHide", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
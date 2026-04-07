using AAModClassic;
using AAModClassic.Items.Blocks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Oroboros
{
    [AutoloadEquip(EquipType.Body)]
    public class OroborosChestplate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oroboros Wood Chestplate");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = 2000;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 4;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<OroborosWood>(), 30);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}
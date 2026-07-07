using AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class OuroborosChestplate : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Ouroboros";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ouroboros Wood Chestplate");
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
                recipe.AddIngredient(ModContent.ItemType<OuroborosWood>(), 30);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}
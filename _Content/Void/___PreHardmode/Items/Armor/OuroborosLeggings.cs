using AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class OuroborosLeggings : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Ouroboros";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ouroboros Wood Boots");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 18;
            Item.value = 100;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 4;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<OuroborosWood>(), 25);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}
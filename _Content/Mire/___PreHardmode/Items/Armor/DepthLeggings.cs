using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class DepthLeggings : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Depth";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Depth Hakama");
            /* Tooltip.SetDefault(@"'Weightless as shadow itself'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 5000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 5;
        }

        public override void RegisterEquipStats()
        {
            AddEffect(new MovementSpeedEffect(0.15f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 20);
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
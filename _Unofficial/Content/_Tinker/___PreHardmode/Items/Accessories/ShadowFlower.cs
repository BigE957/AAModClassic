using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content._Tinker.___PreHardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Waist)]
    [AutoloadEquipGlow(EquipType.Waist)]
    public class ShadowFlower : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shadow Band");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 44;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new ManaCostEffect(-0.08f));
            AddEffect(new ShadowFlowerEffect(10));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShadowBand>());
            recipe.AddIngredient(ItemID.ManaFlower);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddCondition(ConditionUtils.Unofficial);
            recipe.Register();
        }
    }
}
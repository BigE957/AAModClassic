using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class ShadowBand : EquipAbstract, ILocalizedModType
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
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddEffect(new MovementSpeedEffect(0.15f));
            else
                AddEffect<ShadowBandUnofficialEffect>();
        }
    }
}
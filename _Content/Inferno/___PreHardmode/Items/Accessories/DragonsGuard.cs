using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class DragonsGuard : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon's Guard");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.defense = 3;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<DragonsGuardEffect>();
        }
    }
}

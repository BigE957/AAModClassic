using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class OrnateBand : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new MaxLifeEffect(50));
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ornate Band");
        }

    }
}
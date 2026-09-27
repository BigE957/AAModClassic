using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Accessories
{
    public class CharmOfDesire : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Charm of Desire");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 6, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new CharmOfDesireEffect(20));
        }
    }
}
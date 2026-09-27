using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class DarkVoid : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Void");
            /* Tooltip.SetDefault(@"'Dark and spooky'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new EquinoxDayNightStatBoostsEffect(true));
            AddEffect<NightOwlEffect>();
        }
    }
}
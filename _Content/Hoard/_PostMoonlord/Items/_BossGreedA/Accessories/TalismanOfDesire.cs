using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories;
using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Accessories
{
    public class TalismanOfDesire : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Talisman of Desire");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 12, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new CharmOfDesireEffect(40));
            AddEffect<GoldRingEffect>();
            AddEffect<LuckyCoinEffect>();
            AddEffect<DiscountCardEffect>();
        }
    }
}
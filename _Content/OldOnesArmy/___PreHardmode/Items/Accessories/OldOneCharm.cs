using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.OldOnesArmy.___PreHardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class OldOneCharm : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Old One Charm");
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(1, 0, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
        }

        public override void RegisterEquipStats()
        {
			damageMap.GetDamage(DamageClass.Summon) += 0.12f;
			AddEffect(new MaxSentrySlotEffect(1));
            AddEffect(new OldOneCharmEffect(0.22f));
        }
	}
}

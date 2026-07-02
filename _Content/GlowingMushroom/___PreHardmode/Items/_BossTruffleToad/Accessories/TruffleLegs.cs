using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.Accessories
{
    [AutoloadEquip(EquipType.Shoes)]
    public class TruffleLegs : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Truffle Legs");
            /* Tooltip.SetDefault(@"Increased jump speed and allows auto-jump
You are immune to fall damage
Increased jump height"); */
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void RegisterEquipStats()
        {
            AddEffect(new JumpStatsEffect(1.5f, 20, true));
            AddEffect<FallDamageImmunityEffect>();
        }
    }
}
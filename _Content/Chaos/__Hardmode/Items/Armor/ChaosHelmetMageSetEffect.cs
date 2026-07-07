using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Chaos.__Hardmode.Items.Materials;
using AAModClassic._Content.Ocean.___PreHardmode.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Armor
{
    public class ChaosHelmetMageSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.wet)
                player.AddBuff(ModContent.BuffType<ChaosHelmetMageSetEffect_ChaoticFury>(), 2);
        }
    }
}
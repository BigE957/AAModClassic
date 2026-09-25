using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
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
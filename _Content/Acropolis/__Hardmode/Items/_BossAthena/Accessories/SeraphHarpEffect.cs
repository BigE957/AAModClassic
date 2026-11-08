using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories
{
    public class SeraphHarpEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            SummonEquipUtils.HandleSummonerEquip<SeraphHarp_Buff>(player);
        }
    }
}

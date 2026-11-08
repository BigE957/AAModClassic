using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;

namespace AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Accessories
{
    public class GoddessHarpEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            SummonEquipUtils.HandleSummonerEquip<GoddessHarp_Buff>(player);
        }
    }
}

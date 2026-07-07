using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Terrarium.Buffs;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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

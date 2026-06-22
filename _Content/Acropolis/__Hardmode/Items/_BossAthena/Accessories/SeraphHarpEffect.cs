using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Accessories;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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

using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Accessories
{
    public class ArcticMedallionEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.ZoneRain && player.ZoneSnow)
            {
                player.GetDamage(DamageClass.Generic) += 1f;
                player.GetCritChance(DamageClass.Generic) += 100;
            }
        }
    }
}
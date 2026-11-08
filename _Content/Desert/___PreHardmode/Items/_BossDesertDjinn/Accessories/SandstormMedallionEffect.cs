using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Accessories
{
    public class SandstormMedallionEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.ZoneSandstorm)
            {
                player.GetDamage(DamageClass.Generic) += 1f;
                player.GetCritChance(DamageClass.Generic) += 100;
            }
        }
    }
}
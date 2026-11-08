using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;

namespace AAModClassic._Content.Snow.___PreHardmode.Items.Armor
{
    public class RaiderChestplateSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.endurance += (1 - player.statLife / player.statLifeMax) * .1f;
        }
    }
}
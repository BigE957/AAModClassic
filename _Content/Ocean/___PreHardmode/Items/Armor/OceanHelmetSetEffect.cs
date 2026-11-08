using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Armor
{
    public class OceanHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.wet && !player.lavaWet && !player.honeyWet)
            {
                player.AddBuff(ModContent.BuffType<AquaintedWithWater_Buff>(), 2);
            }
        }
    }
}
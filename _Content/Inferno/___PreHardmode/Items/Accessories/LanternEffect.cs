using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories
{
    public class LanternEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.buffImmune[ModContent.BuffType<Clueless_Buff>()] = true;
        }
    }
}
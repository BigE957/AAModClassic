using AAModClassic._Content._Misc._PostMoonlord.Items.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    public class DwarvenGauntletEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.buffImmune[ModContent.BuffType<InfinityOverload_Buff>()] = true;
        }
    }
}
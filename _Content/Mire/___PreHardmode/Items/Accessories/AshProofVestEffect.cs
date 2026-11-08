using AAModClassic._Content.Inferno.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Accessories
{
    public class AshProofVestEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.buffImmune[ModContent.BuffType<BurningAsh_Buff>()] = true;
        }
    }
}
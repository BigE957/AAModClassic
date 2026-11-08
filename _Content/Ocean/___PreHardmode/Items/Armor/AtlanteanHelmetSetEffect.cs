using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Armor
{
    public class AtlanteanHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.wet)
            {
                player.AddBuff(ModContent.BuffType<Atlantean_Buff>(), 2);
            }
        }
    }
}
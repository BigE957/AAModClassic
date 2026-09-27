using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.Accessories
{
    public class StormClawEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (!player.HeldItem.autoReuse && player.HeldItem.damage > -1 && !Main.SettingsEnabled_AutoReuseAllItems)
            {
                player.GetAttackSpeed(DamageClass.Generic) *= 2;
            }
        }
    }
}
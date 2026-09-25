using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.DataStructures;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    public class TimeStoneRespawnEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<TimeStoneRespawnPlayer>().effect = true;
        }
    }

    public class TimeStoneRespawnPlayer : EquipmentEffectPlayer
    {
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (effect)
            {
                Player.respawnTimer = (int)(Player.respawnTimer * .2);
            }
        }
    }
}
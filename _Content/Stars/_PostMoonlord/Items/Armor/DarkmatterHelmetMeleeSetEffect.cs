using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class DarkmatterHelmetMeleeSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            const float effectRange = 500;

            if (!Main.dayTime && player.GetModPlayer<StarHelmetMeleePlayer>().ShieldCoolDown > 0)
                player.lifeRegen += 2;

            for (int p = 0; p < Main.player.Length; p++)
            {
                if (Main.player[p].active && (Main.player[p].Center - player.Center).Length() < effectRange && player.team == Main.player[p].team && Main.player[p].GetModPlayer<StarHelmetMeleePlayer>().ShieldCoolDown <= 0)
                {
                    Main.player[p].GetModPlayer<StarHelmetMeleePlayer>().ShieldTime = 2;
                    Main.player[p].GetModPlayer<StarHelmetMeleePlayer>().badShield = false;
                }
            }
        }
    }
}
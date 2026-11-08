using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class RadiumHelmetMeleeSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            const float effectRange = 500;

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                for (int p = 0; p < Main.player.Length; p++)
                {
                    if (Main.player[p].active && (Main.player[p].Center - player.Center).Length() < effectRange && player.team != Main.player[p].team)
                    {
                        Main.player[p].GetModPlayer<StarHelmetMeleePlayer>().ShieldTime = 2;
                        Main.player[p].GetModPlayer<StarHelmetMeleePlayer>().badShield = true;
                    }
                }
            }

            for (int n = 0; n < Main.npc.Length; n++)
            {
                if ((Main.npc[n].Center - player.Center).Length() < effectRange && Main.npc[n].CanBeChasedBy(ignoreDontTakeDamage: false))
                {
                    Main.npc[n].GetGlobalNPC<StarHelmetMeleePlayer_RadiumWeaken>().BrokenShield = 2;
                }
            }
        }
    }
}
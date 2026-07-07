using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

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
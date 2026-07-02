using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Accessories;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Accessories
{
    public class ArtifactOfGuiltEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<ArtifactOfGuiltPlayer>().effect = true;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Main.LocalPlayer.GetModPlayer<ArtifactOfGuiltPlayer>().charge);
    }

    public class ArtifactOfGuiltPlayer : EquipmentEffectPlayer
    {
        public int charge = 0;

        public override void OnHitByAnything(Player.HurtInfo hurtInfo, NPC npc = null, Projectile proj = null)
        {
            base.OnHitByAnything(hurtInfo, npc, proj);

            if (effect)
                charge += hurtInfo.Damage;
        }

        public override void PostUpdate()
        {
            if (charge >= 250)
            {
                Player.AddBuff(ModContent.BuffType<ArtifactOfGuilt_Buff>(), 900);
                charge = 0;
            }
            else if (!effect)
                charge = 0;
        }
    }
}
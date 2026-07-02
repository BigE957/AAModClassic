using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories;
using AAModClassic.Rarities;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Accessories
{
    public class NaitokurosuDebuffEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<NaitokurosuDebuffPlayer>().effect = true;
        }
    }

    public class NaitokurosuDebuffPlayer : EquipmentEffectPlayer
    {
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (effect && (proj.CountsAsClass(DamageClass.Ranged) || proj.minion))
            {
                int buff = Main.dayTime ? BuffID.Venom : ModContent.BuffType<Moonraze_Buff>();
                target.AddBuff(buff, 1000);
            }
        }
    }
}
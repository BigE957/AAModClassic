using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;
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
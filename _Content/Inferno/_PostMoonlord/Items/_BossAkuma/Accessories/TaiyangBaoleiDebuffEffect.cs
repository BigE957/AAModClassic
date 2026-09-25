using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories
{
    public class TaiyangBaoleiDebuffEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<TaiyangBaoleiDebuffPlayer>().effect = true;
        }
    }

    public class TaiyangBaoleiDebuffPlayer : EquipmentEffectPlayer
    {
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (effect && (proj.CountsAsClass(DamageClass.Melee) || proj.CountsAsClass(DamageClass.Magic)))
            {
                int buff = Main.dayTime ? BuffID.Daybreak : BuffID.OnFire;
                target.AddBuff(buff, 1000);
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (effect)
            {
                int buff = Main.dayTime ? BuffID.Daybreak : BuffID.OnFire;
                target.AddBuff(buff, 1000);
            }
        }
    }
}
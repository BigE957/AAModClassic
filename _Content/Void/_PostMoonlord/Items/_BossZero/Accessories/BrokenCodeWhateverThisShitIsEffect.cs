using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories
{
    public class BrokenCodeWhateverThisShitIsEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<BrokenCodeWhateverThisShitIsPlayer>().effect = true;
        }
    }

    public class BrokenCodeWhateverThisShitIsPlayer : EquipmentEffectPlayer
    {
        public override void OnHitByAnything(Player.HurtInfo hurtInfo, NPC npc = null, Projectile proj = null)
        {
            if (effect)
            {
                Player.AddBuff(BuffID.Panic, 180);
                Player.immuneTime = Player.longInvince ? 180 : 120;
            }
            ;
        }
    }
}
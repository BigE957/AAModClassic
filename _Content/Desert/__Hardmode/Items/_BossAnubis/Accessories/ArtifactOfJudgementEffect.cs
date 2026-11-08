using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Accessories
{
    public class ArtifactOfJudgementEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<ArtifactOfJudgementPlayer>().effect = true;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Main.LocalPlayer.GetModPlayer<ArtifactOfJudgementPlayer>().charge);
    }

    public class ArtifactOfJudgementPlayer : EquipmentEffectPlayer
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
                Player.AddBuff(ModContent.BuffType<ArtifactOfJudgement_Buff>(), 900);
                charge = 0;
            }
            else if (!effect)
                charge = 0;
        }
    }
}
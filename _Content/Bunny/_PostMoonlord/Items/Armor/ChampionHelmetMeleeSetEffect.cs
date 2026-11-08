using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    public class ChampionHelmetMeleeSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<ChampionHelmetMeleeSetPlayer>().effect = true;
        }
    }

    public class ChampionHelmetMeleeSetPlayer : EquipmentEffectPlayer
    {
        public override void PostUpdate()
        {
            if (effect && AAMod.ArmorAbilityKey.JustPressed && !Player.HasBuff(ModContent.BuffType<ChampionHelmetMeleeSetEffect_RajahsRage>()))
            {
                int BuffLength = 240;
                if (Player.statLife < (int)(Player.statLifeMax2 * .75f))
                {
                    BuffLength = 360;
                }
                if (Player.statLife < (int)(Player.statLifeMax2 * .5f))
                {
                    BuffLength = 480;
                }
                if (Player.statLife < (int)(Player.statLifeMax2 * .25f))
                {
                    BuffLength = 600;
                }
                Player.AddBuff(ModContent.BuffType<ChampionHelmetMeleeSetEffect_DefendersRage>(), BuffLength);
                int RageCooldown = BuffLength * 4;
                Player.AddBuff(ModContent.BuffType<ChampionHelmetMeleeSetEffect_RajahsRage>(), RageCooldown);
            }

            if (Player.HasBuff(ModContent.BuffType<ChampionHelmetMeleeSetEffect_DefendersRage>()))
            {
                Player.armorEffectDrawShadowLokis = true;
            }
        }
    }
}
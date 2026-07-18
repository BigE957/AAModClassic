using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Unofficial.Content._Tinker.EquipmentEffects;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Accessories
{
    public class ShadowBandUnofficialEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<OutOfCombatPlayer>().OutOfCombatEffectsToPerform.Add(() => ApplyMyEpicBuff(player));
            player.GetModPlayer<ShadowBandUnofficialPlayer>().effect = true;
        }

        public void ApplyMyEpicBuff(Player player)
        {
            player.AddBuff(ModContent.BuffType<ShadowBandUnofficialEffect_ShadowStealth>(), 2);
        }
    }

    public class ShadowBandUnofficialPlayer : EquipmentEffectPlayer
    {
        private bool _isOutOfCombat => Player.GetModPlayer<OutOfCombatPlayer>().IsOutOfCombat;
        private bool _isOutOfCombatPrevFrame;

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (_isOutOfCombatPrevFrame != _isOutOfCombat)
            {
                if (_isOutOfCombat)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Dust.NewDust(drawInfo.Position, 20, 20, DustID.CrimsonSpray, Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-20, 20), 0, default, 2);
                    }
                }
                else
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Dust.NewDust(drawInfo.Position, 20, 20, DustID.BlueFairy, Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-20, 20), 0, default, 2);
                    }
                }
            }

            _isOutOfCombatPrevFrame = _isOutOfCombat;
        }
    }
}
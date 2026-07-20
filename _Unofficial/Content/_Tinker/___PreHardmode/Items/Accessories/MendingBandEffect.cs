using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
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

namespace AAModClassic._Unofficial.Content._Tinker.___PreHardmode.Items.Accessories
{
    public class MendingBandEffect : EquipmentEffectData
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
}
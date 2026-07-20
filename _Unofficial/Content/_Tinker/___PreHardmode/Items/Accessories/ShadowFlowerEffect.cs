using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Unofficial.Content._Tinker.EquipmentEffects;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content._Tinker.___PreHardmode.Items.Accessories
{
    public class ShadowFlowerEffect(int amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<OutOfCombatPlayer>().OutOfCombatEffectsToPerform.Add(() => ApplyMyEpicBuff(player));
            player.GetModPlayer<ShadowBandUnofficialPlayer>().effect = true;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(amount);

        public void ApplyMyEpicBuff(Player player)
        {
            int outOfCombatTimer = player.GetModPlayer<OutOfCombatPlayer>().OutOfCombatTimer;
            int outOfCombatThreshold = player.GetModPlayer<OutOfCombatPlayer>().OutOfCombatThreshold;
            bool isOutOfCombat = player.GetModPlayer<OutOfCombatPlayer>().IsOutOfCombat;

            if (isOutOfCombat && outOfCombatTimer % 60 == 0 && player.statMana < player.statManaMax2)
            {
                player.statMana += amount;
                if (player.statMana > player.statManaMax2)
                    player.statMana = player.statManaMax2;

                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(player.Center, 20, 20, DustID.BlueFairy, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 0, default, 2);
                }
            }
        }
    }
}
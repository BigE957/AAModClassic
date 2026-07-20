using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Unofficial.Content._Tinker.EquipmentEffects;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content._Tinker.__Hardmode.Items.Accessories
{
    public class CuringBandEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<OutOfCombatPlayer>().OutOfCombatEffectsToPerform.Add(() => ApplyMyEpicBuff(player));
            player.GetModPlayer<ShadowBandUnofficialPlayer>().effect = true;
        }

        public void ApplyMyEpicBuff(Player player)
        {
            player.AddBuff(ModContent.BuffType<CuringBandEffect_CuringStealth>(), 2);
        }
    }

    public class CuringBandEffectEdits
    {
        public static void ApplyEdits()
        {
            On_Player.UpdateBuffs += On_Player_UpdateBuffs;
            On_Main.DrawBuffIcon += On_Main_DrawBuffIcon;
        }

        private static int On_Main_DrawBuffIcon(On_Main.orig_DrawBuffIcon orig, int drawBuffText, int buffSlotOnPlayer, int x, int y)
        {
            int returnStuff = returnStuff = orig(drawBuffText, buffSlotOnPlayer, x, y);
            int buffType = Main.LocalPlayer.buffType[buffSlotOnPlayer];

            if (Main.LocalPlayer.HasBuff<CuringBandEffect_CuringStealth>() && !BuffID.Sets.TimeLeftDoesNotDecrease[buffType] && Main.debuff[buffType])
            {
                Color color = new Color(Main.buffAlpha[buffSlotOnPlayer], Main.buffAlpha[buffSlotOnPlayer], Main.buffAlpha[buffSlotOnPlayer], Main.buffAlpha[buffSlotOnPlayer]);
                Texture2D tex = CuringBandEffect_CuringStealth.BuffOverlay.Value;
                Main.spriteBatch.Draw(tex, new Vector2(x, y), new Rectangle(0, 0, tex.Width, tex.Height), color, 0f, default, 1f, SpriteEffects.None, 0f);
            }

            return returnStuff;
        }

        private static void On_Player_UpdateBuffs(On_Player.orig_UpdateBuffs orig, Player self, int i)
        {
            if (self.HasBuff<CuringBandEffect_CuringStealth>())
            {
                for (int j = 0; j < Player.MaxBuffs; j++)
                {
                    int buffType = self.buffType[j];
                    if (self.whoAmI == Main.myPlayer && !BuffID.Sets.TimeLeftDoesNotDecrease[buffType] && Main.debuff[buffType])
                        self.buffTime[j]--;
                }
            }

            orig(self, i);
        }
    }
}
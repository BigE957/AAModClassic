using AAModClassic._Content.Terra.__Hardmode.Items.Armor;
using AAModClassic._Unreleased.Content.Void.Dusts;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories
{
    public class BrokenCodeTeleportUnofficialEffect : EquipmentEffectData
    {
        public const int FREEZEDURATION = 150;
        public const int SECRETINVULDURATION = 30;
        public const int FREEZECOOLDOWNDURATION = 480;

        public override void DoEffect(Player player)
        {
            player.GetModPlayer<BrokenCodeTeleportUnofficialPlayer>().effect = true;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(ChatUtils.GetVanillaKeybindGlyph("Grapple"), (float)FREEZECOOLDOWNDURATION / 60, (float)FREEZEDURATION / 60);
    }

    public class BrokenCodeTeleportUnofficialPlayer : EquipmentEffectPlayer
    {
        public override void UpdateEquips()
        {
            if (effect)
            {
                if (PlayerInput.Triggers.JustPressed.Grapple && !Player.HasBuff<BrokenCodeTeleportUnofficialEffect_FreezeCooldown>() && !Player.HasBuff<BrokenCodeTeleportUnofficialEffect_Freeze>() && Main.myPlayer == Player.whoAmI)
                {
                    Player.immune = true;
                    Player.immuneNoBlink = true;
                    Player.immuneTime = BrokenCodeTeleportUnofficialEffect.SECRETINVULDURATION;

                    Teleport();

                    Player.AddBuff(ModContent.BuffType<BrokenCodeTeleportUnofficialEffect_FreezeCooldown>(), BrokenCodeTeleportUnofficialEffect.FREEZECOOLDOWNDURATION);
                    //Player.AddBuff(ModContent.BuffType<BrokenCode_FreezeCooldown>(), 60);
                    Player.AddBuff(ModContent.BuffType<BrokenCodeTeleportUnofficialEffect_Freeze>(), BrokenCodeTeleportUnofficialEffect.FREEZEDURATION);
                    //Player.AddBuff(ModContent.BuffType<BrokenCode_Freeze>(), 20);
                }
            }
        }

        public void Teleport()
        {
            Vector2 teleportPos = Main.MouseWorld;
            TeleportEffect(Player.getRect(), 1, teleportPos);
            Player.Teleport(teleportPos, TeleportationStyleID.DebugTeleport, 0);
            NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, teleportPos.X, teleportPos.Y, 1, 0, 0);
            TeleportEffect(Player.getRect(), 1, Player.position);
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Glitch") { PitchVariance = 0.8f });
        }

        // shamelessly stolen from the queen slime hook teleport effect
        public static void TeleportEffect(Rectangle effectRect, float dustCountMult = 1f, Vector2 otherPosition = default)
        {
            effectRect.Inflate(15, 15);
            int num7 = (int)(60f * dustCountMult);
            Vector2 vector = otherPosition - effectRect.TopLeft();
            for (int n = 0; n < num7; n++)
            {
                float fadeIn = 0.4f + Main.rand.NextFloat();
                float scale = 0.4f + Main.rand.NextFloat();
                Color newColor = Main.hslToRgb(0.66f + Main.rand.NextFloat() * 0.24f, 1f, 0.5f);
                Dust dust = Dust.NewDustDirect(effectRect.TopLeft(), effectRect.Width, effectRect.Height, ModContent.DustType<VoidDust_Unreleased>(), 0f, 0f, 127, newColor);
                dust.scale = (float)Main.rand.Next(20, 70) * 0.01f;
                if (n < 10)
                    dust.scale += 0.25f;

                if (n < 5)
                    dust.scale += 0.25f;

                if ((float)n < (float)num7 * 0.8f)
                    dust.velocity += vector * 0.1f * Main.rand.NextFloat();

                dust.noGravity = true;
                dust.noLight = true;
                dust.scale = scale;
                dust.fadeIn = fadeIn;
                if (dust.dustIndex != 6000)
                {
                    Dust obj2 = Dust.CloneDust(dust);
                    obj2.scale *= 0.65f;
                    obj2.fadeIn *= 0.65f;
                    obj2.color = new Color(255, 255, 255, 255);
                }
            }
        }
    }

    // oughh thanks diamondwalker this thing is awesome
    public class BrokenCodeTeleportUnofficialEdits
    {
        public static void ApplyEdits()
        {
            On_Player.Update += UpdatePlayer;
            On_NPC.UpdateNPC += UpdateNPC;
            On_Projectile.Update += UpdateProj;
            On_Dust.UpdateDust += UpdateDust;

            On_Player.QuickGrapple_GetItemToUse += QuickGrapple_GetItemToUse;
        }

        private static void UpdatePlayer(On_Player.orig_Update orig, Player p, int i)
        {
            int buff = ModContent.BuffType<BrokenCodeTeleportUnofficialEffect_Freeze>();

            if (p.active && p.HasBuff(buff))
            {
                // make sure the glitched buff is still counting down even though the others aren't
                int index = p.FindBuffIndex(buff);
                p.buffTime[index]--;

                // the teleport
                // we check if player is pressing grapple a diff way bcuz since player logic is paused we cant know if theyre still pressing
                // it or not the safe normal human way
                if (PlayerInput.Triggers.JustPressed.Grapple && Main.myPlayer == p.whoAmI)
                {
                    p.GetModPlayer<BrokenCodeTeleportUnofficialPlayer>().Teleport();
                    p.ClearBuff(ModContent.BuffType<BrokenCodeTeleportUnofficialEffect_Freeze>());
                }

                return;
            }

            // if any other players have the buff, we still freeze
            foreach (Player player in Main.ActivePlayers)
            {
                if (player != null && player != p && player.active && player.HasBuff(buff))
                {
                    return;
                }
            }

            orig(p, i);
        }

        private static void UpdateNPC(On_NPC.orig_UpdateNPC orig, NPC npc, int i)
        {
            int buff = ModContent.BuffType<BrokenCodeTeleportUnofficialEffect_Freeze>();
            foreach (Player player in Main.ActivePlayers)
            {
                if (player != null && player.active && player.HasBuff(buff))
                {
                    return;
                }
            }

            orig(npc, i);
        }

        private static void UpdateProj(On_Projectile.orig_Update orig, Projectile proj, int i)
        {
            int buff = ModContent.BuffType<BrokenCodeTeleportUnofficialEffect_Freeze>();
            foreach (Player player in Main.ActivePlayers)
            {
                if (player != null && player.active && player.HasBuff(buff))
                {
                    return;
                }
            }

            orig(proj, i);
        }

        private static void UpdateDust(On_Dust.orig_UpdateDust orig)
        {
            int buff = ModContent.BuffType<BrokenCodeTeleportUnofficialEffect_Freeze>();
            foreach (Player player in Main.ActivePlayers)
            {
                if (player != null && player.active && player.HasBuff(buff))
                {
                    return;
                }
            }

            orig();
        }

        private static Item QuickGrapple_GetItemToUse(On_Player.orig_QuickGrapple_GetItemToUse orig, Player self)
        {
            if (self.GetModPlayer<BrokenCodeTeleportUnofficialPlayer>().effect && Main.myPlayer == self.whoAmI)
                return null;

            return orig(self);
        }
    }

}
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Effects;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon
{
    public class ShenDoragonTransition : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discordian Awakening");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.alpha = 255;
            NPC.lifeMax = 10000000;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            Music = MusicManagementSystem.MusicSlots["Silence"];
            NPC.boss = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            NPC.Center = player.Center - new Vector2(0, 300f);
            NPC.netOffset = Vector2.Zero;
            NPC.ai[0]++;
            if (NPC.timeLeft <= 10)
                NPC.timeLeft = 10;
            if (NPC.ai[0] > 350)
                Music = MusicManagementSystem.MusicSlots["Shen_Transition"];
            if (NPC.ai[0] >= 600)
            {
                for (int LOOP = 0; LOOP < 8; LOOP++)
                {
                    Dust dust1;
                    Vector2 position1 = NPC.Center;
                    dust1 = Main.dust[Dust.NewDust(position1 - new Vector2(10, 10), 20, 20, ModContent.DustType<Dusts.Discord_Dust>(), 0, 0, 0, default, 1f)];
                    dust1.noGravity = false;
                    dust1.scale *= 1.3f;
                    dust1.velocity.Y -= 6;
                }
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 690)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Transition.1"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] == 790)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Transition.2"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] == 900)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Transition.3"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] == 1080)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Transition.4"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] == 1300)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Transition.5"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] >= 1540)
                {
                    NPC.alpha -= 5;
                }
                if (NPC.ai[0] == 1600)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Transition.6"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] >= 1870)
                {
                    MusicUtils.InstantSwitchMusic(MusicManagementSystem.MusicSlots["Shen_Awakened"]);
                    SummonShen();
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
        }

        public override bool PreAI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                NPC.TargetClosest();
                Player player = Main.player[NPC.target];
                NPC.Center = player.Center - new Vector2(0, 300f);
                NPC.netOffset = Vector2.Zero;
                NPC.ai[0]++;
                if (NPC.alpha < 255 && NPC.ai[0] > 200)
                {
                    Music = MusicManagementSystem.MusicSlots["Shen_Awakened"];
                    for (int LOOP = 0; LOOP < 8; LOOP++)
                    {
                        Dust dust1;
                        Vector2 position1 = NPC.Center;
                        dust1 = Main.dust[Dust.NewDust(position1, 20, 20, ModContent.DustType<Dusts.Discord_Dust>(), 0, 0, 0, default, 1f)];
                        dust1.noGravity = false;
                        dust1.scale *= 1.3f;
                        dust1.velocity.Y -= 6;
                    }
                }

                if (NPC.ai[0] >= 400)
                {
                    NPC.alpha -= 5;
                }

                if (NPC.ai[0] >= 600)
                {
                    SummonShen();
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
                return false;
            }
            return true;
        }

        public void SummonShen()
        {
            Player player = Main.player[NPC.target];
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Transition.7"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Transition.8"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

                int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 1, Main.myPlayer, 0, 0);
                Main.projectile[b].Center = NPC.Center;

                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<ShenDoragonA>(), false, NPC.Center, "Shen Awakened", false);
            }
        }

        public static Color GetColorAlpha()
        {
            return new Color(233, 0, 233) * (Main.mouseTextColor / 255f);
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;
        public bool saythelinezero = false;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            if (NPC.alpha <= 0)
            {
                //spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                BaseDrawing.DrawAura(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, auraPercent, 1f, 0f, 0f, GetColorAlpha());
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, GetColorAlpha());
                return false;
            }

            return true;
        }
    }
}
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
    public class ShenDoragonSpawn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discord");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.alpha = 255;
            Music = MusicManagementSystem.MusicSlots["Shen_Intro"];
            SceneEffectPriority = SceneEffectPriority.BossMedium;
            NPC.lifeMax = 1000000000;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10000000;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.boss = true;
        }

        int dustx = 50;

        public override void AI()
        {
            if (!NPC.HasNPCTarget)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];
            NPC.Center = player.Center - new Vector2(0, 300f);
            NPC.ai[0]++;
            if (NPC.ai[0] <= 960)
            {
                for (int LOOP = 0; LOOP < 4; LOOP++)
                {
                    Dust dust1;
                    Dust dust2;
                    Vector2 position1 = new Vector2(NPC.Center.X + dustx, NPC.Center.Y);
                    Vector2 position2 = new Vector2(NPC.Center.X - dustx, NPC.Center.Y);
                    dust1 = Main.dust[Dust.NewDust(position1, 1, 1, ModContent.DustType<Dusts.AkumaDust>(), 0, 0, 0, default, 1f)];
                    dust1.noGravity = false;
                    dust2 = Main.dust[Dust.NewDust(position2, 1, 1, ModContent.DustType<Dusts.YamataDust>(), 0, 0, 0, default, 1f)];
                    dust2.noGravity = true;
                    dust2.scale *= 1.3f;
                    dust2.velocity.Y -= 6;
                }
            }
            else if (NPC.ai[0] > 960 && NPC.ai[0] < 1640)
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

            if (NPC.ai[0] == 150)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.1"), new Color(180, 41, 32));
            }

            if (NPC.ai[0] == 330)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.2"), AAColor.YamataDialogue);
            }

            if (NPC.ai[0] == 510)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.3"), new Color(180, 41, 32));
            }

            if (NPC.ai[0] == 700)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.4"), AAColor.YamataDialogue);
            }
            if (NPC.ai[0] == 880)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.5"), new Color(180, 41, 32));
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.5"), AAColor.YamataDialogue);
            }

            if (dustx > 0 && NPC.ai[0] >= 900)
            {
                dustx -= 1;
                if (dustx < 0)
                {
                    dustx = 0;
                }
            }

            if (NPC.ai[0] == 960)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.6"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (NPC.ai[0] == 1040)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.7"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (NPC.ai[0] == 1320)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.8"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (NPC.ai[0] >= 1500)
            {
                NPC.alpha -= 5;
            }

            if (NPC.ai[0] == 1520)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.9"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

            }

            if (NPC.ai[0] == 1780)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.10"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

            }

            if (NPC.ai[0] >= 1945)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Spawn.First.11"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                SummonShen();
                NPC.active = false;
            }
        }

        public override bool PreAI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                NPC.TargetClosest();
                Player player = Main.player[NPC.target];
                NPC.Center = player.Center - new Vector2(0, 300f); ;
                NPC.ai[0]++;

                if (NPC.ai[0] <= 960)
                {
                    for (int LOOP = 0; LOOP < 4; LOOP++)
                    {
                        Dust dust1;
                        Dust dust2;
                        Vector2 position1 = new Vector2(NPC.Center.X + dustx, NPC.Center.Y);
                        Vector2 position2 = new Vector2(NPC.Center.X - dustx, NPC.Center.Y);
                        dust1 = Main.dust[Dust.NewDust(position1, 1, 1, ModContent.DustType<Dusts.AkumaDust>(), 0, 0, 0, default, 1f)];
                        dust1.noGravity = false;
                        dust2 = Main.dust[Dust.NewDust(position2, 1, 1, ModContent.DustType<Dusts.YamataDust>(), 0, 0, 0, default, 1f)];
                        dust2.noGravity = true;
                        dust2.scale *= 1.3f;
                        dust2.velocity.Y -= 6;
                    }
                }
                else if (NPC.ai[0] > 960 && NPC.ai[0] < 1640)
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

                if (NPC.ai[0] >= 400)
                {
                    NPC.alpha -= 5;
                }


                if (dustx > 0 && NPC.ai[0] >= 900)
                {
                    dustx -= 1;
                    if (dustx < 0)
                    {
                        dustx = 0;
                    }
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
            MusicUtils.InstantSwitchMusic(MusicManagementSystem.MusicSlots["Shen"]);
            if (Main.netMode != NetmodeID.MultiplayerClient)
                AAModGlobalNPC.SpawnBoss(Main.player[NPC.target], ModContent.NPCType<ShenDoragon>(), false, NPC.Center, "");
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
                //spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                BaseDrawing.DrawAura(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, auraPercent, 1f, 0f, 0f, GetColorAlpha());
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, GetColorAlpha());
                return false;
            }
            return true;
        }
    }
}
using AAModClassic.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Dialogue;
using AAModClassic.UI.Dialogue.DisplayEffects;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.NPCs.__BossGreed
{
    public class SparkOfDesire : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spark of Desire");
            Main.npcFrameCount[NPC.type] = 4;
            this.HideFromBestiary();
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            NPC.alpha = 255;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            Music = MusicManagementSystem.MusicSlots["Silence"];
            NPC.boss = true;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            if (Main.netMode != NetmodeID.Server)
            {
                if (NPC.ai[0] > 175)
                {
                    NPC.alpha -= 3;
                    if (NPC.alpha < 0)
                    {
                        NPC.alpha = 0;
                    }
                }

            }

            NPC.ai[0]++;

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                switch (NPC.ai[0])
                {
                    case 175:
                        WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.Greed.Spawn", NPC.Center, 0, 60, effects: new NearbyBossText());
                        break;
                    case 760:
                        if (Main.netMode != NetmodeID.Server)
                            Music = MusicManagementSystem.MusicSlots["Greed"];
                        break;
                    case 1000:
                        AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<GreedHead>(), true, NPC.Center, ModContent.GetInstance<GreedHead>().DisplayName.Value, false);
                        NPC.active = false;
                        NPC.netUpdate = true;
                        break;
                }
            }
            else
            {
                switch (NPC.ai[0])
                {
                    case 175:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Greed.Spawn.1"), Color.Goldenrod);
                        break;
                    case 350:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Greed.Spawn.2"), Color.Goldenrod);
                        break;
                    case 500:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Greed.Spawn.3"), Color.Goldenrod);
                        break;
                    case 570:
                        if (Main.netMode != NetmodeID.Server)
                            Music = MusicManagementSystem.MusicSlots["Greed"];
                        break;
                    case 610:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Greed.Spawn.4"), Color.Goldenrod);
                        break;
                    case 755:
                        if(!NPC.AnyNPCs(ModContent.NPCType<GreedHead>()))
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<GreedHead>(), true, NPC.Center, ModContent.GetInstance<GreedHead>().DisplayName.Value, false);
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Greed.Spawn.5"), Color.Goldenrod);
                            }
                            NPC.active = false;
                        }
                        break;
                }
            }
        }

        public override bool CheckActive()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<GreedHead>()))
            {
                return false;
            }
            NPC.active = false;
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            if (++NPC.frameCounter >= 4)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= frameHeight * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Rectangle SunFrame = new Rectangle(0, 0, 70, 70);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position + new Vector2(0, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, 0, NPC.spriteDirection, 4, SunFrame, NPC.GetAlpha(AAColor.COLOR_WHITEFADE1), true);
            return false;
        }
    }
}
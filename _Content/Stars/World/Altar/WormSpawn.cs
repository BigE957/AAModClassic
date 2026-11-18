using AAModClassic.Assets;
using AAModClassic.Base;
using AAModClassic.Music;
using AAModClassic.UI.Dialogue;
using AAModClassic.UI.Dialogue.DisplayEffects;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars.World.Altar
{
    public class WormSpawn : ModNPC
    {
        public override string Texture => AssetDirectory.General.Nothing;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Heavenly Voice");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();

            WorldTextSystem.TextDataSetup += ModifyEquinoxCutsceneText;
        }

        private void ModifyEquinoxCutsceneText(string textKey, ref DialogueTextData data)
        {
            if (textKey == "Mods.AAModClassic.WorldText.EquinoxCutscene.Singleplayer")
                data.Pages[^1].Lines[0] = data.Pages[^1].Lines[0].Replace("{0}", Main.LocalPlayer.name);
        }

        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 46;
            NPC.alpha = 255;
            Music = MusicManagementSystem.MusicSlots["Equinox_Intro"];
            NPC.lifeMax = 1;
            NPC.boss = true;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10000000;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (!NPC.HasNPCTarget)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];
            NPC.Center = player.Center - new Vector2(0, 300f);
            NPC.netOffset = Vector2.Zero;

            if (!NPC.AnyNPCs(ModContent.NPCType<DBPortal>()))
            {
                WormAltar_Tile.SpawnBoss(player, ModContent.NPCType<DBPortal>(), false, player.Center);
            }
            if (!NPC.AnyNPCs(ModContent.NPCType<NCPortal>()))
            {
                WormAltar_Tile.SpawnBoss(player, ModContent.NPCType<NCPortal>(), false, player.Center);
            }

            NPC.ai[0]++;

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                switch (NPC.ai[0])
                {
                    case 175:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            string type = Main.netMode == NetmodeID.SinglePlayer ? "Singleplayer" : "Multiplayer";
                            WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.EquinoxCutscene." + type, NPC.Center, 0, 10, effects: new NearbyBossText());
                        }
                        break;
                    case 1880:
                        AAWorld.WormActive = true;
                        NPC.active = false;
                        NPC.netUpdate = true;
                        break;
                }
            }
            else
            {
                string s = Main.netMode == NetmodeID.SinglePlayer ? "" : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.s");
                switch (NPC.ai[0])
                {
                    case 175:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn1"), new Color(0, 255, 181));
                        break;
                    case 235:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn2") + s + Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn3"), new Color(43, 178, 245)); break;
                    case 380:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn4"), new Color(0, 255, 181));
                        break;
                    case 540:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn5"), new Color(43, 178, 245));
                        break;
                    case 720:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn6") + s + Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn7"), new Color(43, 178, 245));
                        break;
                    case 780:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn8"), new Color(0, 255, 181));
                        break;
                    case 990:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn9"), new Color(43, 178, 245));
                        break;
                    case 1200:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn10"), new Color(0, 255, 181));
                        break;
                    case 1480:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn11"), new Color(0, 255, 181));
                        break;
                    case 1600:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            string name = Main.netMode == NetmodeID.SinglePlayer ? player.name : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.heroes");
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormSpawn12") + name + ".", new Color(0, 255, 181));
                        }
                        AAWorld.WormActive = true;
                        break;
                    case 1880:
                        AAWorld.WormActive = true;
                        NPC.active = false;
                        NPC.netUpdate = true;
                        break;
                }
            }
        }

        public override bool PreAI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                return false;
            }
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }
    }
}
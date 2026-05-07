using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Bunny._PostMoonlord.NPCs._BossRajahA
{
    public class SupremeRajahDefeat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[NPC.type] = 9;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 130;
            NPC.height = 220;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.defense = 90;
            NPC.lifeMax = 50000;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 1000f;
            NPC.dontTakeDamage = true;
            NPC.boss = true;
            NPC.netAlways = true;
            Music = MusicManagementSystem.MusicSlots["Silence"];
            NPC.noTileCollide = false;
        }

        public override void AI()
        {
            if (NPC.velocity.Y == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[0]++;
            }

            if (NPC.ai[0] == 120)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.1"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 240)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.2"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 360)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.3"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 480)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.4"), 107, 137, 179, true);
            }
            if (NPC.ai[0] >= 600)
            {
                NPC.ai[1] = 1;
                Music = MusicManagementSystem.MusicSlots["Rajah_Epilogue"];
                NPC.netUpdate = true;
            }
            if (NPC.ai[0] == 600)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat("...", 107, 137, 179, true);
            }
            if (NPC.ai[0] >= 840)
            {
                NPC.ai[1] = 2;
                NPC.netUpdate = true;
            }
            if (NPC.ai[0] == 840)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.5"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 960)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.6"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1080)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.7"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1200)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.8"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1380)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.9"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1540)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.10"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1660)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                        BaseUtility.Chat(Language.GetOrRegister("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.11.Singleplayer").FormatWith(Main.LocalPlayer.name), 107, 137, 179, true);
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.11.Multiplayer"), 107, 137, 179, true);
                }
            }
            if (NPC.ai[0] == 1780)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.12"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1900)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.13"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 2020)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.14"), 107, 137, 179, true);
            }
            if (NPC.ai[0] >= 2180)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.15"), 107, 137, 179, true);
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Rajah.Awakened.Defeat.First.Status"), Color.Green, true);
                int p = Projectile.NewProjectile(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.ProjectileType<SupremeRajahLeave>(), 0, 0, Main.myPlayer);
                Main.projectile[p].position = NPC.position;
                NPC.active = false;
                NPC.netUpdate = true;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[1] == 0)
            {
                if (NPC.frameCounter++ > 15)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                }
                if (NPC.frame.Y > frameHeight * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
            else if (NPC.ai[1] == 1)
            {
                NPC.frame.Y = frameHeight * 4;
            }
            else if (NPC.ai[1] == 2)
            {
                if (NPC.frameCounter++ > 15)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                }
                if (NPC.frame.Y > frameHeight * 8 || NPC.frame.Y < frameHeight * 5)
                {
                    NPC.frame.Y = frameHeight * 5;
                }
            }
        }
    }
}
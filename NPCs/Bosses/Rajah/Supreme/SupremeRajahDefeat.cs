using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Localization;
using AAModClassic.Music;


namespace AAModClassic.NPCs.Bosses.Rajah.Supreme
{
    [AutoloadBossHead]
    public class SupremeRajahDefeat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[NPC.type] = 9;
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
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat1"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 240)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat2"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 360)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat3"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 480)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat4"), 107, 137, 179, true);
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
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat5"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 960)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat6"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1080)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat7"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1200)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat8"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1380)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat9"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1540)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat10"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1660)
            {
                string Name;
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    Name = "Terrarians";
                }
                else
                {
                    Name = Main.LocalPlayer.name;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat11") + Name + "?", 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1780)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat12"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 1900)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat13"), 107, 137, 179, true);
            }
            if (NPC.ai[0] == 2020)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat14"), 107, 137, 179, true);
            }
            if (NPC.ai[0] >= 2180)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat15"), 107, 137, 179, true);
                AAWorld.downedRajahsRevenge = true;
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SupremeRajahDefeat16"), Color.Green, true);
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
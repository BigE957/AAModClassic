using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Effects;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Bosses.Anubis.Forsaken
{
    public class FATransition : ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Anubis Legendscribe");
            Main.npcFrameCount[NPC.type] = 15;
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 1000;
            NPC.aiStyle = -1;
            NPC.defense = 1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.boss = true;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.damage = 0;
            NPC.value = 0;
            Music = MusicManagementSystem.MusicSlots["Silence"];
        }

        public override void AI()
        {
            NPC.dontTakeDamage = true;

            NPC.ai[3] = 39;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.velocity.Y == 0)
                {
                    NPC.ai[1]++;
                    if (NPC.ai[1] == 120)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AnubisTransition1"), Color.Gold);
                        Music = MusicManagementSystem.MusicSlots["Anubis_Awakened"];
                    }

                    if (NPC.ai[1] == 240)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AnubisTransition2"), Color.Gold);
                    }

                    if (NPC.ai[1] == 360)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AnubisTransition3"), Color.Gold);
                    }

                    if (NPC.ai[1] == 480)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AnubisTransition4"), Color.Gold);
                    }

                    if (NPC.ai[1] == 600)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AnubisTransition5"), Color.Gold);
                    }

                    if (NPC.ai[1] == 720)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AnubisTransition6"), Color.Gold);
                    }

                    if (NPC.ai[1] == 840)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AnubisTransition7"), Color.ForestGreen);
                    }

                    if (NPC.ai[1] >= 900)
                    {
                        int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 0, Main.myPlayer, 0, 10);
                        Main.projectile[b].Center = NPC.Center;
                        NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<ForsakenAnubis>());
                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[1] < 540)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 6)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                }
                if (NPC.frame.Y > frameHeight * 4)
                {
                    NPC.frame.Y = 0;
                }
            }
            if (NPC.ai[1] == 540)
            {
                NPC.frame.Y = frameHeight * 5;
            }
            if (NPC.ai[1] == 730)
            {
                NPC.frame.Y = frameHeight * 6;
            }
            if (NPC.ai[1] == 740)
            {
                NPC.frame.Y = frameHeight * 7;
            }
            if (NPC.ai[1] == 750)
            {
                NPC.frame.Y = frameHeight * 8;
            }
            if (NPC.ai[1] == 760)
            {
                NPC.frame.Y = frameHeight * 9;
            }
            if (NPC.ai[1] == 770)
            {
                NPC.frame.Y = frameHeight * 10;
            }
            if (NPC.ai[1] == 780)
            {
                NPC.frame.Y = frameHeight * 11;
            }
            if (NPC.ai[1] == 790)
            {
                NPC.frame.Y = frameHeight * 12;
            }
            if (NPC.ai[1] == 800)
            {
                NPC.frame.Y = frameHeight * 13;
            }
            if (NPC.ai[1] >= 840)
            {
                NPC.frame.Y = frameHeight * 14;
            }
        }
    }
}
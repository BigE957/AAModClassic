using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.AH
{
    public class AHDeath : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sisters Defeat");
            Terraria.ID.NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            NPC.width = 100;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            Music = MusicManagementSystem.MusicSlots["Sisters_Intro"];

            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }
        public override void AI()
        {
            NPC.ai[1]++;
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            if (AAConfigClient.Instance.NoBossDialogue)
            {
                AAWorld.downedSisters = true;
                NPC.active = false;
            }

            NPC.Center = player.Center;

            if (NPC.ai[1] == 100)          //if the timer has gotten to 7.5 seconds, this happens (60 = 1 second)
            {
                if (AAWorld.downedSisters)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.1"), new Color(102, 20, 48));
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.First.1"), new Color(72, 78, 117));
                }
            }

            if (NPC.ai[1] == 300)
            {
                if (AAWorld.downedSisters)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.2"), new Color(72, 78, 117));
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.First.2.Front") + (player.Male ? Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.male") : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.fimale")) + Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.First.2.Back"), new Color(102, 20, 48));
                }
            }

            if (NPC.ai[1] == 500)
            {
                if (AAWorld.downedSisters)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.3"), new Color(102, 20, 48));
                    NPC.active = false;
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.First.3"), new Color(72, 78, 117));
                }
            }
            
            if (NPC.ai[1] == 700)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.First.4"), new Color(102, 20, 48));
                AAWorld.downedSisters = true;
                NPC.active = false;
            }
        }
    }
}
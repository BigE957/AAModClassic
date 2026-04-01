using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ChaosSissy");

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
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AHDeath1"), new Color(102, 20, 48));
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AHDeath2"), new Color(72, 78, 117));
                }
            }

            if (NPC.ai[1] == 300)
            {
                if (AAWorld.downedSisters)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AHDeath3"), new Color(72, 78, 117));
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AHDeath4") + (player.Male ? Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.male") : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.fimale")) + Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AHDeath5"), new Color(102, 20, 48));
                }
            }

            if (NPC.ai[1] == 500)
            {
                if (AAWorld.downedSisters)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AHDeath6"), new Color(102, 20, 48));
                    NPC.active = false;
                    AAWorld.downedSisters = true;
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AHDeath7"), new Color(72, 78, 117));
                }
            }
            
            if (NPC.ai[1] == 700)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.AHDeath8"), new Color(102, 20, 48));
                AAWorld.downedSisters = true;
                NPC.active = false;
            }
        }
    }
}
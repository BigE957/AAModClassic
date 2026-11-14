using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu;
using AAModClassic.Base;
using AAModClassic.Music;
using AAModClassic.UI.Dialogue;
using AAModClassic.UI.Dialogue.DisplayEffects;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu
{
    public class CthulhuPortal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Portal");
            this.HideFromBestiary();

        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.alpha = 255;
            NPC.damage = 0;
            Music = MusicManagementSystem.MusicSlots["IZDeath"];
            NPC.boss = true;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public bool Spawned = false;
        public int Speechtimer = 0;

        public override void AI()
        {
            if (NPC.timeLeft <= 10)
            {
                NPC.timeLeft = 10;
            }
            Speechtimer++;

            NPC.scale = 1f - NPC.alpha / 255f;
            NPC.rotation += .15f;

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                switch (Speechtimer)
                {
                    case 180:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.SoulOfCthulhu.Transition", NPC, uptime: 30, effects: new NearbyBossText(600, 1000));
                        break;

                    case 1730:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            SummonSoul();
                        
                        Spawned = true;
                        break;
                }
                if (Speechtimer >= 360)
                {
                    if (!Spawned)
                    {
                        NPC.alpha -= 1;

                        if (NPC.alpha <= 0)
                            NPC.alpha = 0;
                    }
                    else
                        NPC.alpha += 3;
                }
            }
            else
            {
                switch (Speechtimer)
                {
                    case 180:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Transition.1"), Color.DarkCyan);
                        break;

                    case 360:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Transition.2"), Color.DarkCyan);
                        break;

                    case 540:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Transition.3"), Color.DarkCyan);
                        break;

                    case 720:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Transition.4"), Color.DarkCyan);
                        break;

                    case 900:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Transition.5"), Color.DarkCyan);
                        break;

                    case 1080:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Transition.6"), Color.DarkCyan);
                        break;

                    case 1260:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Transition.7"), Color.DarkCyan);
                        break;

                    case 1440:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Transition.8"), Color.DarkCyan);
                        break;

                    case 1620:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            SummonSoul();
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Transition.9"), Color.DarkCyan);
                        }
                        Spawned = true;
                        break;
                }

                if (Speechtimer >= 360)
                {
                    if (!Spawned)
                    {
                        NPC.alpha -= 1;

                        if (NPC.alpha <= 0)
                            NPC.alpha = 0;
                    }
                    else
                        NPC.alpha += 3;
                }
            }
        }

        public void SummonSoul()
        {
            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Transition.Status"), Color.Magenta);
            int npcID = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Cthulhu>());
            Main.npc[npcID].Center = NPC.Center;
            Main.npc[npcID].netUpdate = true;
            NPC.active = false;
        }
    }
}
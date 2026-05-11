using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu;
using AAModClassic.Music;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu
{
    public class CthulhuPortal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Portal");

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

            if (Speechtimer == 180)
            {
                Main.NewText("...you utter fool...", Color.DarkCyan);
            }

            if (Speechtimer == 360)
            {
                Main.NewText("thanks to you breaking that disgusting old ship’s wheel...", Color.DarkCyan);
            }

            if (Speechtimer >= 360)
            {
                if (!Spawned)
                {
                    NPC.alpha -= 1;

                    if (NPC.alpha <= 0)
                    {
                        NPC.alpha = 0;
                    }
                }
                else
                {
                    NPC.alpha += 3;
                }
            }

            if (Speechtimer == 540)
            {
                Main.NewText("...I am now free...", Color.DarkCyan);
            }

            if (Speechtimer == 720)
            {
                Main.NewText("I should thank you, you simple-minded mortal...", Color.DarkCyan);
            }

            if (Speechtimer == 900)
            {
                Main.NewText("However, you stand in the way between me and this world’s impending destruction...", Color.DarkCyan);
            }

            if (Speechtimer == 1080)
            {
                Main.NewText("And after all, you DID kill my brother...such a shame he’s gone.", Color.DarkCyan);
            }

            if (Speechtimer == 1260)
            {
                Main.NewText("...so you must die.", Color.DarkCyan);
            }

            if (Speechtimer == 1440)
            {
                Main.NewText("YOU SHALL BE SLAIN BY ME, CTHULHU, COSMIC CALAMITY!", Color.DarkCyan);
            }

            if (Speechtimer == 1620)
            {
                Main.NewText("PREPARE FOR YOU AND YOUR WORLD’S CATASTROPHIC DEMISE!", Color.DarkCyan);
                SummonSoul();
                Spawned = true;
            }

        }

        public void SummonSoul()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Main.NewText("Cthulhu has been Awakened", Color.DarkCyan);
                int npcID = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Cthulhu>());
                Main.npc[npcID].Center = NPC.Center;
                Main.npc[npcID].netUpdate = true;
            }

            NPC.active = false;
        }
    }
}
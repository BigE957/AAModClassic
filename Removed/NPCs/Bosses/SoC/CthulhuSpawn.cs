using AAModClassic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAModClassic.Removed.NPCs.Bosses.SoC
{
    public class CthulhuSpawn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Quantum Portal");

        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.alpha = 255;
            NPC.damage = 0;
            Music = Mod.GetSoundSlot(SoundType.Music, "Removed/Sounds/Music/SoC");
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

        public override void AI()
        {
            NPC.scale = 1f - NPC.alpha / 255f;
            NPC.rotation += .1f;
            if (NPC.alpha <= 0 && !Spawned)
            {
                SummonSoul();
                Spawned = true;
                NPC.alpha = 0;
            }
            if (!Spawned)
            {
                NPC.alpha -= 3;
            }
            if (Spawned)
            {
                NPC.alpha += 3;
                if (NPC.alpha >= 255)
                {
                    NPC.active = false;
                }
            }
        }

        public void SummonSoul()
        {
            if (Main.netMode != 1)
            {
                Main.NewText("The Soul of Cthulhu shreds through reality into this world", Color.DarkCyan);
                int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("SoC").Type, 0);
                Main.npc[npcID].Center = NPC.Center;
                Main.npc[npcID].netUpdate = true;
            }

            NPC.active = false;
        }
    }
}
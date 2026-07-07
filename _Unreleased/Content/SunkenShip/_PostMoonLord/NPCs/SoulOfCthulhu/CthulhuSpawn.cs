using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu
{
    public class CthulhuSpawn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Quantum Portal");
            this.HideFromBestiary();

        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.alpha = 255;
            NPC.damage = 0;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["SoulOfCthulhu"];
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
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Spawn"), new Color(175, 75, 255));
                int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SoulOfCthulhu>(), 0);
                Main.npc[npcID].Center = NPC.Center;
                Main.npc[npcID].netUpdate = true;
                Main.npc[npcID].target = NPC.target;
            }

            NPC.active = false;
        }
    }
}
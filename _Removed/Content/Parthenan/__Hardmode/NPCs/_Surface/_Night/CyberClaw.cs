using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRetriever;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs._Surface._Night
{
    public class CyberClaw : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cyber Claw");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 24;
            NPC.friendly = false;
            NPC.damage = 35;
            NPC.defense = 4;
            NPC.lifeMax = 300;
            //TODO
            //NPC.HitSound = new LegacySoundStyle(3, 4, SoundType.Sound);
            //NPC.DeathSound = new LegacySoundStyle(4, 14, SoundType.Sound);
            NPC.value = 100f;
            NPC.knockBackResist = 0.6f;
            NPC.noGravity = true;
        }

        public override void AI()
        {
            NPC.noGravity = true;
            BaseAI.AIEye(NPC, ref NPC.ai, false, true, 0.13f, 0.08f, 2f, 1.1f, 1.2f, 1.2f);
            if (NPC.velocity.X > 0f)
            {
                NPC.spriteDirection = 1;
                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);
            }
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = -1;
                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 3.14f;
            }
            if (NPC.frameCounter++ >= 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 38;
                if (NPC.frame.Y > 38 * 3)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPCExtensions.BeenKilled<Retriever>())
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.08f;
            }
            else
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.FulguriteDust>(), hit.HitDirection, -1f, 0);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 15; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.FulguriteDust>(), hit.HitDirection, -1f, 0);
                }
            }
        }

        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FulguriteShard").Type, Main.rand.Next(2));
        }
    }
}
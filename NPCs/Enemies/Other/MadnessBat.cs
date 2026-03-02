using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AAMod.NPCs.Enemies.Other
{
    public class MadnessBat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Madness Flier");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 5;
            NPC.defense = 4;
            NPC.lifeMax = 20;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.knockBackResist = 0.5f;
            NPC.value = Item.sellPrice(0, 0, 8, 30);
            NPC.npcSlots = 0f;
            NPC.lavaImmune = true;
            NPC.netAlways = true;
            NPC.aiStyle = 14;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.CaveBat;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("MadnessBatBanner").Type;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y > (frameHeight * 3))
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }

        public override void PostAI()
        {
            Player player = Main.player[NPC.target];

            if (player.Center.X > NPC.Center.X)
            {
                NPC.spriteDirection = -1;
            }
            else
            {
                NPC.spriteDirection = 1;
            }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe || Main.hardMode)
            {
                return 0f;
            }
            if (!Main.dayTime)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.1f;
            }
            return SpawnCondition.Underground.Chance * 0.1f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.Next(2) == 0 ? ModContent.DustType<Dusts.InfinityOverloadR>() : ModContent.DustType<Dusts.InfinityOverloadP>(), hitDirection, -1f, 0);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 15; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.Next(2) == 0 ? ModContent.DustType<Dusts.InfinityOverloadR>() : ModContent.DustType<Dusts.InfinityOverloadP>(), hitDirection, -1f, 0);
                }
            }
        }

        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("MadnessFragment").Type, Main.rand.Next(1,2));
        }

    }
}
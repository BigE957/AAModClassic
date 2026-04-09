using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Boss.MushroomMonarch;
using AAModClassic.NPCs.Bosses.MushroomMonarch;
using AAModClassic.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Enemies.Mushroom
{
    public class SmallFrog : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fungus Frog");
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 28;
            NPC.aiStyle = -1;
            NPC.damage = 8;
            NPC.defense = 6;
            NPC.lifeMax = 50;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.alpha = 255;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<Items.Banners.FungusFrogBanner>();
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool isDead = NPC.life <= 0;
            if (isDead) 
            {

            }
            for (int m = 0; m < (isDead ? 35 : 6); m++)
            {
                int dustType = ModContent.DustType<Dusts.MushDust>();
                Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default, isDead ? 2f : 1.5f);
            }
        }
        
        public override void AI()
        {
            NPC.TargetClosest(true);
            if (NPC.alpha > 0)
            {
                NPC.alpha -= 4;
            }
            else
            {
                NPC.alpha = 0;
            }
            Player player = Main.player[NPC.target];
            if (NPC.velocity.Y != 0)
            {
                if (NPC.velocity.X < 0)
                {
                    NPC.spriteDirection = -1;
                }
                else if (NPC.velocity.X > 0)
                {
                    NPC.spriteDirection = 1;
                }
            }
            else
            {
                if (player.position.X < NPC.position.X)
                {
                    NPC.spriteDirection = -1;
                }
                else if (player.position.X > NPC.position.X)
                {
                    NPC.spriteDirection = 1;
                }
            }
            if (NPC.ai[0] < -10) NPC.ai[0] = -10;
            BaseAI.AISlime(NPC, ref NPC.ai, false, 60, 3f, -2f, 6f, -4f);
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.Y < 0)
            {
                NPC.frame.Y = frameHeight * 4;
            }
            else if (NPC.velocity.Y > 0)
            {
                NPC.frame.Y = frameHeight * 5;
            }
            else if (NPC.ai[0] < -15f)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.ai[0] > -15f)
            {
                NPC.frame.Y = frameHeight;
            }
            else if (NPC.ai[0] > -10f)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else if (NPC.ai[0] > -5f)
            {
                NPC.frame.Y = frameHeight * 3;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneMush && NPCExtensions.BeenKilled<MushroomMonarch>() ? .3f : 0f;
        }

        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Mushium>(), Main.rand.Next(1, 5));
        }
    }
}
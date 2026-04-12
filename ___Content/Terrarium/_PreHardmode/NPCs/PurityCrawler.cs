using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Terrarium._PreHardmode.NPCs
{
    public class PurityCrawler : ModNPC
    {
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Purity Crawler");
			Main.npcFrameCount[NPC.type] = 5;
		}

        public bool Val = false;
        public int[] subNPCs = new int[0];
        public int swapTicks = 0, swapTicksMax = 20;

        public override void SetDefaults()
		{
            NPC.lifeMax =  60;
            NPC.defense = 5;
            NPC.damage = 10;
            NPC.width = 26;
            NPC.height = 20;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            NPC.alpha = 255;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.PurityCrawlerBanner>();
        }

        public override void OnKill()
        {
            if (Main.rand.NextBool(4))
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Materials.TerraShard>());
            }
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Terra, 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
            BaseAI.AIZombie(NPC, ref NPC.ai, false, false, 0, 0.07f, 3f, 3, 4, 60, true, 10, 60, true, null, false);
        }
    }
}

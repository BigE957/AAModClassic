using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Void
{
    public class Searcher1 : ModNPC
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stone Searcher");
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            NPC.width = 35;
            NPC.height = 35;
            NPC.value = BaseUtility.CalcValue(0, 0, 5, 50);
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 80;
            NPC.defense = 30;
            NPC.damage = 15;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.knockBackResist = 0.5f;
            NPC.noGravity = true;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("StoneSearcherBanner").Type;

        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool isDead = NPC.life <= 0;
            for (int m = 0; m < (isDead ? 25 : 5); m++)
            {
                int dustType = ModContent.DustType<VoidDust>();
                Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, isDead ? 2f : 1.1f);
            }

            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/SearcherGore1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/SearcherGore2"), 1f);
            }
        }

        public override void AI()
        {
            BaseAI.AIEater(NPC, ref NPC.ai, .022f, 4, .6f, false, true);
            Player player = Main.player[NPC.target];
            bool playerActive = player != null && player.active && !player.dead;
            BaseAI.LookAt(playerActive ? player.Center : (NPC.Center + NPC.velocity), NPC, 0);         
        }

        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("DoomiteScrap").Type, Main.rand.Next(3));
        }
    }
}
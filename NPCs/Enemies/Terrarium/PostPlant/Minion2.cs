using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Terrarium.PostPlant
{
    public class Minion2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Crawler");
			Main.npcFrameCount[NPC.type] = 5;
		}

		public override void SetDefaults()
		{
            NPC.lifeMax =  350;
            NPC.defense = 20;
            NPC.damage = 50;
            NPC.width = 26;
            NPC.height = 20;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            NPC.alpha = 255;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<TerraCrawlerBanner>();
        }

        public override void AI()
        {
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<SummonDust>(), 0f, 0f, 100, default, 2f);
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
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 20;
                if (NPC.frame.Y > (20 * 4))
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Terrablaze>(), 300);
        }
    }
}

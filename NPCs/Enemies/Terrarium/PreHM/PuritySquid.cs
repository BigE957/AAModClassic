using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Terrarium.PreHM
{
    public class PuritySquid : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Purity Squid");
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
            NPC.lifeMax = 60;
            NPC.defense = 20;
            NPC.damage = 10;
            NPC.width = 26;
            NPC.height = 20;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            NPC.alpha = 255;
            NPC.noTileCollide = false;
            NPC.noGravity = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<Items.Banners.PuritySquidBanner>();
        }
        
        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            BaseAI.AIElemental(NPC, ref NPC.ai, null, 120, false, true, 800, 400, 180, 2);

            if (NPC.ai[0] == 2f)
            {
                NPC.alpha += 12;
                if (NPC.alpha > 255)
                {
                    NPC.alpha = 255;
                }
            }
            else
            {
                NPC.alpha -= 12;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }

            NPC.rotation = NPC.velocity.X / 15f;

            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 36;
                if (NPC.frame.Y > (36 * 3))
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }

        public override void OnKill()
        {
            if (Main.rand.NextBool(4))
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<TerraShard>());
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Terra, 0f, 0f, 0);
                }
            }
        }
    }
}

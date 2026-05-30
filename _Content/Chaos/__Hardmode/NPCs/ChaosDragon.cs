using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.NPCs
{
    public class ChaosDragon : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Dragon");
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
            NPC.lifeMax =  200;
            NPC.defense = 20;
            NPC.damage = 50;
            NPC.width = 26;
            NPC.height = 20;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.ChaosDragonBanner>();
            SpawnModBiomes = new int[2] { ModContent.GetInstance<InfernoBiome>().Type, ModContent.GetInstance<MireBiome>().Type };
        }

        public override void AI()
        {
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.DiscordLight>(), 0f, 0f, 100);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = false;
                }
            }
            NPC.alpha -= 3;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
            BaseAI.AIFlier(NPC, ref NPC.ai, true, 0.4f, 0.04f, 6f, 1.5f, true, 300);
            Player player = Main.player[NPC.target];
            if (player.Center.X > NPC.Center.X)
            {
                NPC.spriteDirection = 1;
            }
            else
            {
                NPC.spriteDirection = -1;
            }
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 96;
                if (NPC.frame.Y > 96 * 3)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.YamataDust>();
                int dust2 = ModContent.DustType<Dusts.YamataDust>();
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }
        
    }
}

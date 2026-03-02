using AAModClassic.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Mire
{
    public abstract class MireSandShark : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mire Sand Shark");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.SandShark];
		}

		public override void SetDefaults()
		{
            NPC.CloneDefaults(NPCID.SandShark);
            AnimationType = NPCID.SandShark;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("MireSandSharkBanner").Type;
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<AbyssiumDust>(), 0f, 0f, 200, default, 0.8f);
                Main.dust[dustIndex].velocity *= 0.3f;
			}
		}
	}
}

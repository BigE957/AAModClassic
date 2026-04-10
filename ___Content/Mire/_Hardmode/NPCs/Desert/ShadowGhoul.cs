using AAModClassic.Items.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.NPCs.Desert
{
    public class ShadowGhoul : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shadow Ghoul");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DesertGhoul];
		}

		public override void SetDefaults()
		{
            NPC.CloneDefaults(NPCID.DesertGhoul);
            AnimationType = NPCID.DesertGhoul;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<MireGhoulBanner>();
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.AbyssiumDust>(), 0f, 0f, 200, default, 0.8f);
                Main.dust[dustIndex].velocity *= 0.3f;
			}
		}
	}
}

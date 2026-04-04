using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Inferno
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class InfernoGhoul : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infernal Ghoul");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DesertGhoul];
		}

		public override void SetDefaults()
		{
            NPC.CloneDefaults(NPCID.DesertGhoul);
            AnimationType = NPCID.DesertGhoul;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<InfernoGhoulBanner>();
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.IncineriteDust>(), 0f, 0f, 200, default, 0.8f);
                Main.dust[dustIndex].velocity *= 0.3f;
			}
		}
	}
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Inferno
{
    public abstract class InfernoSandShark : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Inferno Sand Shark");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.SandShark];
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.SandShark);
            AnimationType = NPCID.SandShark;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("InfernoSandSharkBanner").Type;
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



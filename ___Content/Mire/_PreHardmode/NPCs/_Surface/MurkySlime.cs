using AAModClassic.Items.Banners;
using AAModClassic.Items.Throwing;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.NPCs._Surface
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class MurkySlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Murky Slime");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
		}

		public override void SetDefaults()
		{
            NPC.aiStyle = NPCAIStyleID.Slime;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.width = 32;
			NPC.height = 26;
			NPC.damage = 5;
			NPC.defense = 2;
			NPC.lifeMax = 20;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 60f;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.CorruptSlime;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<MireSlimeBanner>();
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
		
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MurkyGel>(), 1, 5, 15));
        }
	}
}

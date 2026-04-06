using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Globals;

namespace AAModClassic.NPCs.Enemies.Mushroom
{
    public class MushroomCrab : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushroom Crab");
            Main.npcFrameCount[NPC.type] = 5;
		}

		public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 34;
            NPC.aiStyle = NPCAIStyleID.Fighter;
            NPC.damage = 16;
            NPC.defense = 20;
            NPC.lifeMax = 140;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            AnimationType = NPCID.AnomuraFungus;
            NPC.knockBackResist = 0.3f;
            NPC.value = 1300f;
            NPC.buffImmune[31] = false;
            NPC.npcSlots = 0.3f;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<Items.Banners.MushroomCrabBanner>();
        }

        public override void AI()
        {
            AAAI.InfernoFighterAI(NPC, ref NPC.ai, true, false, -1, 0.13f, 3f, 3, 4, 60, true, 10, 60, true, null, false);
        }

        public override void HitEffect(NPC.HitInfo hit)
		{

            int dust1 = ModContent.DustType<Dusts.MushDust>();
            if (NPC.life <= 0)
			{
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
            }
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneMush ? .4f : 0f;
        }

        public override void OnKill()
		{
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Mushroom);
        }
	}
}
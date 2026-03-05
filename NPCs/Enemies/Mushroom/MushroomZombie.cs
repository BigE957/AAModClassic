using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Globals;

namespace AAModClassic.NPCs.Enemies.Mushroom
{
    public class MushroomZombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushroom Zombie");
            Main.npcFrameCount[NPC.type] = 3;
		}

		public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Fighter;
            NPC.damage = 9;
            NPC.defense = 12;
            NPC.lifeMax = 90;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            AnimationType = NPCID.ZombieMushroomHat;
            NPC.knockBackResist = 0.3f;
            NPC.value = 1200f;
            NPC.buffImmune[31] = false;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("MushroomZombieBanner").Type;
        }

        public override void AI()
        {
            AAAI.InfernoFighterAI(NPC, ref NPC.ai, true, true, 1, 0.07f, 1f, 3, 4, 60, true, 10, 60, true, null, false);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneMush ? .7f : 0f;
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

		public override void OnKill()
		{
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Mushroom);
        }
	}
}
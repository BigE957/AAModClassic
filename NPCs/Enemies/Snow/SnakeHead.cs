using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Items.BossSummons;
using AAModClassic.NPCs.Bosses.Serpent;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Snow
{
    public class SnakeHead : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Snow Serpent");
		}

		public override void SetDefaults()
		{
			NPC.damage = 20;
			NPC.npcSlots = 5f;
            NPC.damage = 35;
            NPC.width = 20;
            NPC.height = 20;
            NPC.defense = 13;
            NPC.lifeMax = 250;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            AnimationType = NPCID.GiantWormHead;
            NPC.behindTiles = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.netAlways = true;
            NPC.value = Item.sellPrice(0, 0, 10, 0);
            NPC.buffImmune[BuffID.Frostburn] = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<Items.Banners.SnakeBanner>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneSnow &&
                NPC.downedBoss3 && 
                !Main.dayTime ? .1f : 0f;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
			AAAI.AIWorm(NPC, new int[]{ ModContent.NPCType<SnakeHead>(), ModContent.NPCType<SnakeBody>(), ModContent.NPCType<SnakeTail>() }, 9, 8f, 12f, 0.1f, false, false);
            
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = 1;

            }
            else
            {
                NPC.spriteDirection = -1;
            }
        }
        
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Chilled, 200, true);
			}
			else
			{
				target.AddBuff(BuffID.Chilled, 100, true);
			}
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.IceDust>(), hit.HitDirection, -1f, 0);
            }
            if (NPC.life == 0)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.SnowDustLight>(), hit.HitDirection, -1f, 0);
                }
            }
        }

        public override bool PreKill()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<SerpentHead>()))
            {
                return false;
            }
            return base.PreKill();
        }
        public override void OnKill()
        {
            if (Main.rand.Next(4) == 0)
            {
                NPC.DropLoot(ModContent.ItemType<SubzeroCrystal>());
            }
        }
    }
}
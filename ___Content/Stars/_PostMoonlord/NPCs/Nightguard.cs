using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Materials;
using AAModClassic.NPCs.Bosses.Equinox;

namespace AAModClassic.___Content.Stars._PostMoonlord.NPCs
{
    public class Nightguard : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Night Guard");
            Main.npcFrameCount[NPC.type] = 1;
		}

		public override void SetDefaults()
		{
            NPC.width = 38;
            NPC.height = 38;
            NPC.value = 0;
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1200;
            NPC.defense = 120;
            NPC.damage = 80;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
            NPC.knockBackResist = 0.3f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.NightGuardBanner>();
		}

		public override void HitEffect(NPC.HitInfo hit)
		{		
			bool isDead = NPC.life <= 0;
			for (int m = 0; m < (isDead ? 25 : 5); m++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.NightcrawlerDust>(), NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, isDead ? 2f : 1.1f);
			}
		}

		float shootAI = 0;
		public override void AI()
		{
			BaseAI.AISkull(NPC, ref NPC.ai, false, 6f, 350f, 0.1f, 0.15f);
			Player player = Main.player[NPC.target];
			bool playerActive = player != null && player.active && !player.dead;
			BaseAI.LookAt(playerActive ? player.Center : NPC.Center + NPC.velocity, NPC, 0);		
			if(Main.netMode != NetmodeID.MultiplayerClient && playerActive)
			{
				shootAI++;
				if(shootAI >= 90)
				{
					shootAI = 0;
					int projType = ModContent.ProjectileType<Moonray>();					
					if(Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						BaseAI.FireProjectile(player.Center, NPC, projType, (int)(NPC.damage * 0.25f), 0f, 2f);
				}
			}
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (AAWorld.Darkmatter < 5)
            {
                return 0f;
            }
            return SpawnCondition.Underground.Chance * 0.1f;
        }

        public override void OnKill()
        {
			if(AAWorld.downedEquinox)
			{
				for (int Ammount = 0; Ammount < Main.rand.Next(3); Ammount++)
				{
					NPC.DropLoot(ModContent.ItemType<DarkEnergy>());
				}
			}
        }

        public override Color? GetAlpha(Color drawColor)
		{
			Color c = Color.White * (Main.mouseTextColor / 255f);
			c.A = 255;
			return c;
		}		
	}
}
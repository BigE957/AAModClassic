using AAModClassic.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AAModClassic.NPCs.Enemies.Other
{
    public class MadnessSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Madness Slime");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = NPCAIStyleID.Slime;
			NPC.damage = 7;
            NPC.width = 30;
			NPC.height = 22;
			NPC.defense = 4;
			NPC.lifeMax = 25;
			NPC.knockBackResist = 0f;
			AnimationType = NPCID.CorruptSlime;
			NPC.value = Item.sellPrice(0, 0, 5, 0);
			NPC.alpha = 60;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<Items.Banners.MadnessSlimeBanner>();
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || Main.hardMode)
			{
				return 0f;
			}
			return SpawnCondition.OverworldDaySlime.Chance * 0.1f;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.NextBool(2) ? ModContent.DustType<Dusts.InfinityOverloadR>() : ModContent.DustType<Dusts.InfinityOverloadP>(), hit.HitDirection, -1f, 0);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 15; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.NextBool(2) ? ModContent.DustType<Dusts.InfinityOverloadR>() : ModContent.DustType<Dusts.InfinityOverloadP>(), hit.HitDirection, -1f, 0);
				}
			}
		}
		
		public override void OnKill()
		{
			Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<MadnessFragment>(), Main.rand.Next(1, 2));
		}
	}
}
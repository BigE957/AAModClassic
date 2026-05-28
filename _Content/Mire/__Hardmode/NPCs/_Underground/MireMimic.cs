using AAModClassic._Content.Mire.__Hardmode.Items.Accessories;
using AAModClassic._Content.Mire.__Hardmode.Items.Weapons;
using AAModClassic._Content.Mire.World.Biomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AAModClassic._Content.Mire.__Hardmode.NPCs._Underground
{
    public class MireMimic : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mire Mimic");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[475];
		}

		public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 42;
            NPC.damage = 50;
			NPC.defense = 8;
			NPC.lifeMax = 3500;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 240000f;
            NPC.knockBackResist = .30f;
            NPC.aiStyle = NPCAIStyleID.BiomeMimic;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.BigMimicHallow;
            SpawnModBiomes = [ModContent.GetInstance<UndergroundMireBiome>().Type];
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;
            if (spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneMire && Main.hardMode && !spawnInfo.PlayerSafe)
            {
                return SpawnCondition.UndergroundMimic.Chance;
            }
            return 0f;
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, Vector2.Zero, 13);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, Vector2.Zero, 12);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, Vector2.Zero, 11);
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(ModContent.ItemType<BotchedBand>(), ModContent.ItemType<BackScratcher>(), ModContent.ItemType<Bubbleshot>()));
        }
    }
}
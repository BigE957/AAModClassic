using AAModClassic._Content.Inferno.__Hardmode.Items.Accessories;
using AAModClassic._Content.Inferno.__Hardmode.Items.Weapons;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.__Hardmode.Items.Accessories;
using AAModClassic._Content.Mire.__Hardmode.Items.Weapons;
using AAModClassic._CrossMod.Thorium.Weapons.Healer;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;

namespace AAModClassic._Content.Inferno.__Hardmode.NPCs._Underground
{
    public class InfernoMimic : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Inferno Mimic");
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
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            SpawnModBiomes = [ModContent.GetInstance<UndergroundInfernoBiome>().Type];
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;
            if (spawnInfo.Player.GetModPlayer<ZAAPlayer>().ZoneInferno && Main.hardMode && !spawnInfo.PlayerSafe)
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
            LeadingConditionRule notUnofficialRule = new(new NotUnofficial());

            notUnofficialRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<OrnateBand>(), ModContent.ItemType<SunHalberd>()));

            npcLoot.Add(notUnofficialRule);

            LeadingConditionRule unofficialRule = new(new Unofficial());

            unofficialRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<OrnateBand>(), ModContent.ItemType<SunHalberd>(), ModContent.ItemType<DragonsClaw>()));

            unofficialRule.OnSuccess(ItemDropRule.Common(ItemID.GreaterHealingPotion, 1, 5, 10));

            unofficialRule.OnSuccess(ItemDropRule.Common(ItemID.GreaterManaPotion, 1, 5, 15));

            npcLoot.Add(unofficialRule);
        }
    }
}
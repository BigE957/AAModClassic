using AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.BossStandard;
using AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.Consumables;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Daybringer;
using AAModClassic.UI.Core.BestiaryBackgrounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Nightcrawler
{
    [AutoloadBossHead]		
	public class NightcrawlerHead : DaybringerHead
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nightcrawler");
            Main.npcFrameCount[NPC.type] = 1;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                PortraitPositionXOverride = 24,
                Position = new Vector2(56, 36),
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }		
		
		public override void SetDefaults()
		{
            base.SetDefaults();
			nightcrawler = true;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.Nightcrawler")
            ]);

            bestiaryEntry.AddTags(new NightcrawlerBestiaryBackground());
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NightcrawlerTrophy>(), 10));

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<EquinoxWormsTreasureBag>()));

            LeadingConditionRule notExpert = new(new Conditions.NotExpert());

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DarkEnergy>(), 1, 30, 75));

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<NightcrawlerMask>(), 7));

            LeadingConditionRule starGenned = new(new RadiumStarsGenerated());

            starGenned.OnSuccess(ItemDropRule.Common(ModContent.ItemType<StarIdol>(), 4));

            npcLoot.Add(starGenned);
            npcLoot.Add(notExpert);
        }
    }
}
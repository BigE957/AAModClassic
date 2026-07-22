using AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.BossStandard;
using AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.Consumables;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Daybringer;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories;
using AAModClassic.UI.Core.BestiaryBackgrounds;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;

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
            NPCID.Sets.BossBestiaryPriority.Add(Type);
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
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<EquinoxWormsTreasureBag>()));

            LeadingConditionRule lastWorm = new(new LastWorm());
            LeadingConditionRule loreCondition = new(new LoreItemDropCondition(() => AAWorld.downedEquinox));
            lastWorm.OnSuccess(loreCondition).OnSuccess(new PerPlayerDropRule(ModContent.ItemType<EquinoxWormsLore>(), 1));

            npcLoot.Add(lastWorm);

            LeadingConditionRule masterMode = new(new LastWormInMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EquinoxWormsRelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NightcrawlerTrophy>(), 10));

            LeadingConditionRule notExpert = new(new Conditions.NotExpert());

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DarkEnergy>(), 1, 30, 75));

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<NightcrawlerMask>(), 7));

            LeadingConditionRule starGenned = new(new RadiumStarsGenerated());

            starGenned.OnSuccess(ItemDropRule.Common(ModContent.ItemType<StarIdol>(), 4));

            npcLoot.Add(starGenned);
            npcLoot.Add(notExpert);

            LeadingConditionRule anceintsDownAndRemoved = new(new PostLateAncientsAndRemovedWorld());

            anceintsDownAndRemoved.OnSuccess(ItemDropRule.Common(ModContent.ItemType<TimeStone>(), 50));

            npcLoot.Add(anceintsDownAndRemoved);
        }
    }
}
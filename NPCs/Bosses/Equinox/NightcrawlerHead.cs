using AAModClassic.___Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Items.Boss.Equinox;
using AAModClassic.Items.Vanity.Mask;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Equinox
{
    [AutoloadBossHead]		
	public class NightcrawlerHead : DaybringerHead
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nightcrawler");
            Main.npcFrameCount[NPC.type] = 1;			
		}		
		
		public override void SetDefaults()
		{
            base.SetDefaults();
			nightcrawler = true;
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NCTrophy>(), 10));

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<EquinoxBag>()));

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
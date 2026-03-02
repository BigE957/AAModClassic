using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Equinox
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
			bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("EquinoxBag").Type;
		}
    }
}
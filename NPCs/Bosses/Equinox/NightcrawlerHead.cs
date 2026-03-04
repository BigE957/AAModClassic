using Terraria;
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
    }
}
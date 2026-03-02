using Terraria.ModLoader;
namespace AAModClassic.NPCs.Bosses.Yamata
{
    [AutoloadBossHead]
    public class YamataHeadF2 : YamataHeadF1
    {
        public override void SetDefaults()
        {
			base.SetDefaults();
			leftHead = true;
        }
	}
}

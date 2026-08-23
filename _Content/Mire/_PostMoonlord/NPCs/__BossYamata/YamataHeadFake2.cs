using Terraria.ModLoader;
namespace AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata
{
    [AutoloadBossHead]
    public class YamataHeadFake2 : YamataHeadFake1
    {
        public override void SetDefaults()
        {
			base.SetDefaults();
			leftHead = true;
            NPC.BossBar = Main.BigBossProgressBar.NeverValid;
        }
	}
}

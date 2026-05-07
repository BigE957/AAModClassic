using AAModClassic._Content.Acropolis.__Hardmode.NPCs;

namespace AAModClassic._Content.Acropolis.__Hardmode.NPCs._BossAthena
{
	public class SeraphA : Seraph
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Seraph Guard");		
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.lifeMax = 130;
        }

        public override bool PreKill() => false;
    }
}
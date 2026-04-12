using AAModClassic.___Content.Acropolis._Hardmode.NPCs;

namespace AAModClassic.NPCs.Bosses.Athena
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
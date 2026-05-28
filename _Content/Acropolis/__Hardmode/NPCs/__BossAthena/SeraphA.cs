using Terraria.GameContent.Bestiary;

namespace AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena
{
	public class SeraphA : Seraph
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Seraph Guard");		
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.SeraphGuard")
            ]);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.lifeMax = 130;
        }

        public override bool PreKill() => false;
    }
}
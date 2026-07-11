using AAModClassic.UI.World;
using AAModClassic.Utilities.Interfaces;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena
{
	public class SeraphA : Seraph, IBannerNPC
	{
        public int OverrideBannerNPCType => ModContent.NPCType<Seraph>();

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.SeraphGuard")
            ]);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                NPC.lifeMax = 130;
        }

        public override bool PreKill() => false;
    }
}
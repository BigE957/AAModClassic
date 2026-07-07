using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu
{
    public class RealityBent_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Riftbent");
            // Description.SetDefault("The space around you is being distorted");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<ZAAPlayer>().riftbent = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>().riftBent = true;
        }
    }
}

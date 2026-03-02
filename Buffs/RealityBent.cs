using AAModClassic.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class RealityBent : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Riftbent");
            // Description.SetDefault("The space around you is being distorted");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<AAPlayer>().riftbent = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>().riftBent = true;
        }
    }
}

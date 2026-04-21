using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
	//TODO: unused. remove when FFlames is ported to here
    public class ForsakenFlames_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Forsaken Flames");
			// Description.SetDefault("Your sins manifest upon your flesh as flames");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().FFlames = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>().FFlames = true;
        }
    }
}

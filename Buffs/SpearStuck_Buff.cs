using AAModClassic;
using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class SpearStuck_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Speared");
			// Description.SetDefault("There's a spear stuck in you. Ouch.");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

		public override void Update(Player player, ref int buffIndex)
		{
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            player.GetModPlayer<AAPlayer>().Spear = true;
        }

		public override void Update(NPC npc, ref int buffIndex)
		{
            AAModGlobalNPC GNPC = npc.GetGlobalNPC<AAModGlobalNPC>();

            GNPC.Spear = true;
        }
	}
}

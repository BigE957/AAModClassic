using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.Buffs
{
    public class Terrablaze_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terrablaze");
			// Description.SetDefault("Incoming damage increased");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().terraBlaze = true;
            player.statDefense -= 25;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<AAModGlobalNPC>().terraBlaze = true;
		}
	}

	public class TerrablazeNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		private bool previousTerrablaze = false;

        public override bool PreAI(NPC npc)
        {
			if(previousTerrablaze != npc.GetGlobalNPC<AAModGlobalNPC>().terraBlaze)
			{
				if(npc.GetGlobalNPC<AAModGlobalNPC>().terraBlaze)
                    npc.defense -= 25;
				else
                    npc.defense += 25;
            }
			previousTerrablaze = npc.GetGlobalNPC<AAModGlobalNPC>().terraBlaze;
            return true;
        }
	}
}

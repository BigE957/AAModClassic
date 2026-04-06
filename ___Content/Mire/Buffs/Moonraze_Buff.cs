using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.Buffs
{
    public class Moonraze_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Moonraze");
            // Description.SetDefault("Incinerated by lunar rays");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<AAModGlobalNPC>().Moonraze = true;
		}
	}
}

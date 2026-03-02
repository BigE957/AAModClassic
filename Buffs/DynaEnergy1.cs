using AAModClassic.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class DynaEnergy1 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dyna-Energy");
			Main.debuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<AAModGlobalNPC>().DynaEnergy1 = true;
        }
	}
}

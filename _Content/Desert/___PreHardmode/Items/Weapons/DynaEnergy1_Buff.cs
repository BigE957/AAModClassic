using AAModClassic.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.Items.Weapons
{
    public class DynaEnergy1_Buff : ModBuff
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

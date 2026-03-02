using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Electrified : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Electrified");
			Main.debuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<AAModGlobalNPC>().Electrified = true;
		}
	}
}

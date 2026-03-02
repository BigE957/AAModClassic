using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Stunned : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stunned");
			// Description.SetDefault("It's small but has a fiery temper");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.velocity *= 0.01f;
		}
	}
}
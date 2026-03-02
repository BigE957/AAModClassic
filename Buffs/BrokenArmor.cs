using AAModClassic.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class BrokenArmor : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Broken Armor");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>().BrokenArmor = true;
        }
    }
}

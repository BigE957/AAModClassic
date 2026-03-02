using AAModClassic;
using AAModClassic.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class CursedHellfire : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cursed Hellfire");
			// Description.SetDefault("Your flesh and blood are burning away");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().CursedHellfire = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>().CursedHellfire = true;
        }
    }
}

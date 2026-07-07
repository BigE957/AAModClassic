using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.Buffs
{
    public class DiscordianInferno_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Discordian Inferno");
            // Description.SetDefault("Your soul is tearing itself apart");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ZAAPlayer>().discordInferno = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<AAModGlobalNPC>().DiscordInferno = true;
		}
	}
}

using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DiscordInferno : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Discordian Inferno");
            // Description.SetDefault("Your soul is tearing itself apart");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AAPlayer>().discordInferno = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<AAModGlobalNPC>().DiscordInferno = true;
		}
	}
}

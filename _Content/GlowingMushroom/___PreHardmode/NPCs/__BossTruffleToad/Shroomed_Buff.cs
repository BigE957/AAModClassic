using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad
{
    public class Shroomed_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("SHROOM'D");
			// Description.SetDefault("You've been shroomed");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

		public override void Update(Player player, ref int buffIndex)
		{
            if (player.wingTimeMax <= 0)
            {
                player.wingTimeMax = 0;
            }
            player.wingTimeMax /= 8;
            player.GetModPlayer<AAPlayer>().shroomed = true;
        }
        
	}
}

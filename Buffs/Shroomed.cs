using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Shroomed : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("SHROOM'D");
			// Description.SetDefault("You've been shroomed");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
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

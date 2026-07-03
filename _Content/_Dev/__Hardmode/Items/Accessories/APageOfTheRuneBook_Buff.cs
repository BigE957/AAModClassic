using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Accessories
{
    public class APageOfTheRuneBook_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rune");
			// Description.SetDefault("Summons runes to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			bool flag = player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBook_BunnyRune>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBook_DiscordRune>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBook_EnergyRune>()] > 0;
			if (flag)
			{
				modPlayer.WeakCCRune = true;
			}
			if (!modPlayer.WeakCCRune && !modPlayer.CCBook)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}
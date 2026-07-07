using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Accessories
{
    public class APageOfTheRuneBookEffect_Buff : ModBuff
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
			ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
			bool flag = player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBookEffect_BunnyRune>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBookEffect_DiscordRune>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBookEffect_EnergyRune>()] > 0;
			if (!flag)
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
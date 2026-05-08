using AAModClassic._Content._EX._PostMoonlord.Items.Weapons.RuneBook;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class CCRune_Buff : ModBuff
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
			bool flag = player.ownedProjectileCounts[ModContent.ProjectileType<BunnyRune>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<DiscordRune>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<EnergyRune>()] > 0;
			bool flag2 = player.ownedProjectileCounts[ModContent.ProjectileType<TerraRune>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<ChaosRune>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<VoidRune>()] > 0;
			if (flag)
			{
				modPlayer.WeakCCRune = true;
			}
			if (flag2)
			{
				modPlayer.CCRune = true;
			}
			if (!modPlayer.WeakCCRune && !modPlayer.CCRune && !modPlayer.CCBook && !modPlayer.CCBookEX)
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
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class PerfectChaosRitual_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Xiao Doragon");
			// Description.SetDefault("Summons a small chaos dragon to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<PerfectChaosRitual_XiaoDoragon>()] > 0)
			{
				modPlayer.Xiao = true;
			}
			if (!modPlayer.Xiao)
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
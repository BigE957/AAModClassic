using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content._Dev._PostMoonlord.Items.Weapons
{
    public class DapperAcorn_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dapper Squirrel");
			// Description.SetDefault("Now with funny hats");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DapperAcorn_DapperSquirrel1>()] + player.ownedProjectileCounts[ModContent.ProjectileType<DapperAcorn_DapperSquirrel2>()] > 0)
			{
				modPlayer.DapperSquirrel = true;
			}
			if (!modPlayer.DapperSquirrel)
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
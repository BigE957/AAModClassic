using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class DapperSquirrel : ModBuff
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
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DapperSquirrel1").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("DapperSquirrel2").Type] > 0)
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
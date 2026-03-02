using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class FlameSoul : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flame Soul");
			// Description.SetDefault("The weaker you are, the harder it fights");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("FlameSoul").Type] > 0)
			{
				modPlayer.FlameSoul = true;
			}
			if (!modPlayer.FlameSoul)
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
using AAModClassic.Items.Summoning.Minions;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class EnderMinion_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ender Minion");
			// Description.SetDefault("Summons a conflagrate construct to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<EnderMinion>()] > 0)
			{
				modPlayer.enderMinion = true;
			}
			if (!modPlayer.enderMinion)
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
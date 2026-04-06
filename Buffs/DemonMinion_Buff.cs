using AAModClassic;
using AAModClassic.Items.Summoning.Minions;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class DemonMinion_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Demon Buddy");
			// Description.SetDefault("Summons a demon to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DemonMinion>()] > 0)
			{
				modPlayer.DemonMinion = true;
			}
			if (!modPlayer.DemonMinion)
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
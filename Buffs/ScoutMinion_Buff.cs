using AAModClassic;
using AAModClassic.Items.Summoning.Minions;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ScoutMinion_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Void Scout");
			// Description.SetDefault("Summons a Void Scout to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ScoutMinion>()] > 0)
			{
				modPlayer.ScoutMinion = true;
			}
			if (!modPlayer.ScoutMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 2;
			}
		}
	}
}
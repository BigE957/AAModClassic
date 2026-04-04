using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class XiaoDoragon : ModBuff
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
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<XiaoDoragon>()] > 0)
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
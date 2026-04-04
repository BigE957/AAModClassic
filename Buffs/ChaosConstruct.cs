using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ChaosConstruct : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Construct");
			// Description.SetDefault("Summons a chaos construct to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ChaosConstruct>()] > 0)
			{
				modPlayer.ChaosConstruct = true;
			}
			if (!modPlayer.ChaosConstruct)
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
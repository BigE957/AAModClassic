using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class HallowedPrism_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hallow Prism");
			// Description.SetDefault("Summons a prism to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HallowedPrism>()] > 0)
			{
				modPlayer.HallowedPrism = true;
			}
			if (!modPlayer.HallowedPrism)
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
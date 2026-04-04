using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class SockPuppet : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sock Puppet");
			// Description.SetDefault("Summons a Sock Puppet to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SockPuppet>()] > 0)
			{
				modPlayer.Sock = true;
			}
			if (!modPlayer.Sock)
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
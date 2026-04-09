using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class RabbitcopterSoldier_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rabbitcopter Soldier");
			// Description.SetDefault("Summons a Rabbitcopter Soldier to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Rajah.RabbitcopterSoldier>()] > 0)
			{
				modPlayer.Rabbitcopter = true;
			}
			if (!modPlayer.Rabbitcopter)
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
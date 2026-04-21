using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons
{
    public class RajahsRage_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Royal Rabbit");
			// Description.SetDefault("Summons a Royal Rabbit to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<RajahsRage_RoyalRabbit>()] > 0)
			{
				modPlayer.RabbitcopterR = true;
			}
			if (!modPlayer.RabbitcopterR)
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
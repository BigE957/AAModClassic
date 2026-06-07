using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.__Hardmode.Items.Weapons
{
    public class DevilStaff_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Devil Servant");
			// Description.SetDefault("Summons a devil to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DevilStaff_DevilServant>()] > 0)
			{
				modPlayer.DevilMinion = true;
			}
			if (!modPlayer.DevilMinion)
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
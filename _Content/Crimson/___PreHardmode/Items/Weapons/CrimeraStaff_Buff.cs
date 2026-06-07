using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Crimson.___PreHardmode.Items.Weapons
{
    public class CrimeraStaff_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crimtane Crimera");
			// Description.SetDefault("Summons a crimtane crimera to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<CrimeraStaff_CrimtaneCrimera>()] > 0)
			{
				modPlayer.CrimeraMinion = true;
			}
			if (!modPlayer.CrimeraMinion)
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
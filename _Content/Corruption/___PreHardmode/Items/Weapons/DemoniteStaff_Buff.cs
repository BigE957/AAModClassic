using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Corruption.___PreHardmode.Items.Weapons
{
    public class DemoniteStaff_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Demon Eater");
			// Description.SetDefault("Summons a demonite eater to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DemoniteStaff_DemonEater>()] > 0)
			{
				modPlayer.EaterMinion = true;
			}
			if (!modPlayer.EaterMinion)
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
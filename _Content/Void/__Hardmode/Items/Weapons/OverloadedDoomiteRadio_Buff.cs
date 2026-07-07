using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.__Hardmode.Items.Weapons
{
    public class OverloadedDoomiteRadio_Buff : ModBuff
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
			ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<OverloadedDoomiteRadio_VoidScout>()] > 0)
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
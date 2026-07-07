using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Weapons
{
    public class DoomiteRadio_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomite Radio Probe");
			// Description.SetDefault("Summons a doomite radio probe to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DoomiteRadio_DoomiteRadioProbe>()] > 0)
			{
				modPlayer.DoomiteProbeC = true;
			}
			if (!modPlayer.DoomiteProbeC)
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
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Weapons
{
    public class VoidProbeControlUnit_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mini Probe");
			// Description.SetDefault("Summons a void probe to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<VoidProbeControlUnit_MiniProbe>()] > 0)
			{
				modPlayer.ProbeMinion = true;
			}
			if (!modPlayer.ProbeMinion)
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
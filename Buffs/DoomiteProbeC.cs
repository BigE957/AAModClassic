using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DoomiteProbeC : ModBuff
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
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DoomiteProbeC").Type] > 0)
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
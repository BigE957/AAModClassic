using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class DoomiteProbe : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomite Probe");
			// Description.SetDefault("Summons a doomite probe to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DoomiteProbe").Type] > 0)
			{
				modPlayer.DoomiteProbe = true;
			}
			if (!modPlayer.DoomiteProbe)
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
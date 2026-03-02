using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class ProbeMinion : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Void Probe");
			// Description.SetDefault("Summons a void probe to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ProbeMinion").Type] > 0)
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
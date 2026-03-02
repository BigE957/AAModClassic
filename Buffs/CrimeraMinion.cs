using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class CrimeraMinion : ModBuff
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
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("CrimeraMinion").Type] > 0)
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
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Squirrel : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Squirrel");
			// Description.SetDefault("Throws nuts");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Squirrel1").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("Squirrel2").Type] > 0)
			{
				modPlayer.Squirrel = true;
			}
			if (!modPlayer.Squirrel)
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
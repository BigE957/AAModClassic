using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class TrueHallowedPrism : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("True Hallow Prism");
			// Description.SetDefault("Summons a holy prism to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("TrueHallowedPrism").Type] > 0)
			{
				modPlayer.TrueHallowedPrism = true;
			}
			if (!modPlayer.TrueHallowedPrism)
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
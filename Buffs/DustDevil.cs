using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DustDevil : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dust Devil");
			// Description.SetDefault("Summons a dust devil to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DustDevil").Type] > 0)
			{
				modPlayer.dustDevil = true;
			}
			if (!modPlayer.dustDevil)
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
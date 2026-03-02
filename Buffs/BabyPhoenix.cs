using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class BabyPhoenix : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Baby Phoenix");
			// Description.SetDefault("Summons a baby phoenix to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BabyPhoenix").Type] > 0)
			{
				modPlayer.BabyPhoenix = true;
			}
			if (!modPlayer.BabyPhoenix)
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
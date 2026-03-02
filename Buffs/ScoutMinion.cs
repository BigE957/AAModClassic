using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class ScoutMinion : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Void Scout");
			// Description.SetDefault("Summons a Void Scout to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ScoutMinion").Type] > 0)
			{
				modPlayer.ScoutMinion = true;
			}
			if (!modPlayer.ScoutMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 2;
			}
		}
	}
}
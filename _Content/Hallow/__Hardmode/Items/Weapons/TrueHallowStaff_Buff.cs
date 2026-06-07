using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hallow.__Hardmode.Items.Weapons
{
    public class TrueHallowStaff_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("True Hallowed Prism");
			// Description.SetDefault("Summons a holy prism to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<TrueHallowStaff_TrueHallowedPrism>()] > 0)
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
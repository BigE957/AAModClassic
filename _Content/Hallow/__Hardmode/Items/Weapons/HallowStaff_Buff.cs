using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hallow.__Hardmode.Items.Weapons
{
    public class HallowStaff_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hallowed Prism");
			// Description.SetDefault("Summons a prism to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HallowStaff_HallowedPrism>()] > 0)
			{
				modPlayer.HallowedPrism = true;
			}
			if (!modPlayer.HallowedPrism)
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
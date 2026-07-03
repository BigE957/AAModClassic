using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Accessories
{
    public class TheBookOfRunes_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rune");
			// Description.SetDefault("Summons runes to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			bool flag2 = player.ownedProjectileCounts[ModContent.ProjectileType<TheBookOfRunes_TerraRune>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<TheBookOfRunes_ChaosRune>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<TheBookOfRunes_VoidRune>()] > 0;
			if (flag2)
			{
				modPlayer.CCRune = true;
			}
			if (!modPlayer.CCRune && !modPlayer.CCBookEX)
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
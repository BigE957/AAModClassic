using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class CCRune : ModBuff
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
			bool flag = player.ownedProjectileCounts[Mod.Find<ModProjectile>("BunnyRune").Type] > 0 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("DiscordRune").Type] > 0 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("EnergyRune").Type] > 0;
			bool flag2 = player.ownedProjectileCounts[Mod.Find<ModProjectile>("TerraRune").Type] > 0 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("ChaosRune").Type] > 0 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("VoidRune").Type] > 0;
			if (flag)
			{
				modPlayer.WeakCCRune = true;
			}
			if (flag2)
			{
				modPlayer.CCRune = true;
			}
			if (!modPlayer.WeakCCRune && !modPlayer.CCRune && !modPlayer.CCBook && !modPlayer.CCBookEX)
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
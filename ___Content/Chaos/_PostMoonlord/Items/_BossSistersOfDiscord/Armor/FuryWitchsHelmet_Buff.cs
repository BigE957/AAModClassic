using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    public class FuryWitchsHelmet_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flame Soul");
			// Description.SetDefault("The weaker you are, the harder it fights");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<FuryWitchsHelmet_FlameSoul>()] > 0)
			{
				modPlayer.FlameSoul = true;
			}
			if (!modPlayer.FlameSoul)
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
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    public class FuryWitchsHelmetSetMinionEffect_Buff : ModBuff
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
			ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<FuryWitchsHelmetSetMinionEffect_FlameSoul>()] <= 0)
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
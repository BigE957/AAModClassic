using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class TerraWizard : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Wizard");
			// Description.SetDefault("Magic");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<TerraWizard>()] > 0)
			{
				modPlayer.TerraMinion = true;
			}
			if (!modPlayer.TerraMinion)
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
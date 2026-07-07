using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Weapons
{
    public class TerraStaff_Buff : ModBuff
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
			ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<TerraStaff_TerraWizard>()] > 0)
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
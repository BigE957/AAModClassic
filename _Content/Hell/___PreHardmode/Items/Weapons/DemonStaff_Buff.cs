using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.___PreHardmode.Items.Weapons
{
    public class DemonStaff_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Demon Buddy");
			// Description.SetDefault("Summons a demon to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DemonStaff_DemonServant>()] > 0)
			{
				modPlayer.DemonMinion = true;
			}
			if (!modPlayer.DemonMinion)
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
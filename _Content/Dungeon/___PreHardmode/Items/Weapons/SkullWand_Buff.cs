using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Dungeon.___PreHardmode.Items.Weapons
{
    public class SkullWand_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aquatic Skull");
			// Description.SetDefault("Summons a dungeon skull to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SkullWand_AquaticSkull>()] > 0)
			{
				modPlayer.SkullMinion = true;
			}
			if (!modPlayer.SkullMinion)
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
using AAModClassic;
using AAModClassic.Projectiles.EFish;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Fishnado_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fishnado");
			// Description.SetDefault("Summons a fishnado to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Fishnado>()] > 0)
			{
				modPlayer.Fishnado = true;
			}
			if (!modPlayer.Fishnado)
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
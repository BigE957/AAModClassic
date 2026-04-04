using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ChaosClaw : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Discordian Claw");
			// Description.SetDefault("Summons a discordian claw to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AbyssClaw>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<BlazeClaw>()] > 0)
            {
				modPlayer.ChaosClaw = true;
			}
			if (!modPlayer.ChaosClaw)
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
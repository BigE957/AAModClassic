using AAModClassic.___Content.Chaos._PostMoonlord.Items.Weapons;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ChaosClaw_Buff : ModBuff
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
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ChaosBaton_AbyssClaw>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<ChaosBaton_BlazeClaw>()] > 0)
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
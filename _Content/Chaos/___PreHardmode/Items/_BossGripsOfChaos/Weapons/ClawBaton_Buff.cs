using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Weapons
{
    public class ClawBaton_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grips of Chaos");
			// Description.SetDefault("Summons a chaos claw to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ClawBaton_DragonClaw>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<ClawBaton_HydraClaw>()] > 0)
            {
				modPlayer.GripMinion = true;
			}
			if (!modPlayer.GripMinion)
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
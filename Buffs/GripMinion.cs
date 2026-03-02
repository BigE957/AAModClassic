using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class GripMinion : ModBuff
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
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DragonClaw").Type] > 0 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("HydraClaw").Type] > 0)
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
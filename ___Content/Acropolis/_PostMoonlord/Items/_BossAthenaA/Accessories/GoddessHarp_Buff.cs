using AAModClassic.Base.BaseMod.Base;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Acropolis._PostMoonlord.Items._BossAthenaA.Accessories
{
    public class GoddessHarp_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Athena");
			// Description.SetDefault("'I'll help you, but but I'll still thrash you someday.'");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<GoddessHarp_Athena>()] > 0 && BasePlayer.HasAccessory(player, ModContent.ItemType<GoddessHarp>(), true, false))
			{
				modPlayer.Athena = true;
			}
            else
            {
                modPlayer.Athena = false;
            }
			if (!modPlayer.Athena)
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
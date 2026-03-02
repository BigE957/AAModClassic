using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Boss.Athena.Olympian;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Athena : ModBuff
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
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Athena").Type] > 0 && BasePlayer.HasAccessory(player, ModContent.ItemType<GoddessHarp>(), true, false))
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
using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Boss.Athena;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Seraph : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Seraph");
			// Description.SetDefault("Small but feisty");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Seraph").Type] > 0 && BasePlayer.HasAccessory(player, ModContent.ItemType<SeraphHarp>(), true, false))
			{
				modPlayer.Seraph = true;
			}
            else
            {
                modPlayer.Seraph = false;
            }
			if (!modPlayer.Seraph)
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
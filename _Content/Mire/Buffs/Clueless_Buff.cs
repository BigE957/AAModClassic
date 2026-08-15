using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.Buffs
{
    public class Clueless_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fog");
			// Description.SetDefault("You can't see a thing");
			Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            if (player.GetModPlayer<ZAAPlayer>().ZoneMire && Main.dayTime && !AADowned.DownedYamata && !player.ZoneUnderworldHeight && !player.ZoneRockLayerHeight)
            {
                player.GetModPlayer<ZAAPlayer>().Clueless = true;
                player.buffTime[buffIndex] = 5;
                player.blind = true;
            }
		}
	}
}
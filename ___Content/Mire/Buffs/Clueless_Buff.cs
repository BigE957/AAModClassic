using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.Buffs
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
            if (player.GetModPlayer<AAPlayer>().ZoneMire && Main.dayTime && !AAWorld.downedYamata && !player.ZoneUnderworldHeight && !player.ZoneRockLayerHeight)
            {
                player.GetModPlayer<AAPlayer>().Clueless = true;
                player.buffTime[buffIndex] = 5;
                player.blind = true;
            }
		}
	}
}
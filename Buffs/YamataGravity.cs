using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class YamataGravity : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Gravity");
			// Description.SetDefault("'YOU AIN'T GETTIN' AWAY FROM ME, PUNK!'");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().YamataGravity = true;
		}
	}
}
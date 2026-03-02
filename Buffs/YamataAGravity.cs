using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class YamataAGravity : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("True Abyssal Gravity");
			// Description.SetDefault("'REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE'");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().YamataAGravity = true;
		}
	}
}
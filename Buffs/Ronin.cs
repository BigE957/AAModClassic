using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Ronin : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ronin");
            // Description.SetDefault("You wont take any damage");
            Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().Ronin = true;
		}
    }
}
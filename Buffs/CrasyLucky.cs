using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class CrasyLucky : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lucky?");
            // Description.SetDefault("You feel the world around you become strange");
            Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().CrasyLucky = true;
		}
    }
}
using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Yanked : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yanked");
            // Description.SetDefault("'YOU AREN'T GOING ANYWHERE, YOU LITTLE SISSY!'");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.wingTime = 0;
            player.velocity.Y += 10;
            player.GetModPlayer<AAPlayer>().Yanked = true;
        }
    }
}

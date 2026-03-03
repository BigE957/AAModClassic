using AAModClassic;
using Terraria;
using Terraria.ID;
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
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.wingTime = 0;
            player.velocity.Y += 10;
            player.GetModPlayer<AAPlayer>().Yanked = true;
        }
    }
}

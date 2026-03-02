using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class DestinedToDie : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Destined To Die");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AAPlayer>().DestinedToDie = true;
        }
    }
}

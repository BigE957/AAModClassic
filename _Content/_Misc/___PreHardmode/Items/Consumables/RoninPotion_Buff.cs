using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.___PreHardmode.Items.Consumables
{
    public class RoninPotion_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ronin");
            // Description.SetDefault("You wont take any damage");
            Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<ZAAPlayer>().Ronin = true;
		}
    }
}
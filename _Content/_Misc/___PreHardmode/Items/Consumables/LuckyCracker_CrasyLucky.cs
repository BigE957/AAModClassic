using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.___PreHardmode.Items.Consumables
{
    /// <summary>
    /// the misspelling stays
    /// </summary>
    public class LuckyCracker_CrasyLucky : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lucky?");
            // Description.SetDefault("You feel the world around you become strange");
            Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().CrasyLucky = true;
		}
    }
}
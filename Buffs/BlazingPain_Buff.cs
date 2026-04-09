using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class BlazingPain_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scorching Pain");
			// Description.SetDefault("Fire debuffs inflict double damage on you");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer mp = player.GetModPlayer<AAPlayer>();
            mp.AkumaPain = true;
        }
	}
}
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.Buffs
{
    public class TerrasGuidance_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra's Guidance");
			// Description.SetDefault(@"Your feet glow with the power of the terrarium, preventing fall damage");
			Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
            player.noFallDmg = true;
            player.nightVision = true;
		}
	}
}
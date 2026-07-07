using Terraria.ModLoader;
using Terraria;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Accessories
{
	public class TheBookOfTheLaw_InvokedCaligula : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Invoked Caligula");
			// Description.SetDefault("Your claw hit the enemy crasily and heal you.");
			Main.pvpBuff[Type] = true;
			Main.debuff[Type] = false;
			Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().InvokedCaligula = true;
			
			player.buffTime[buffIndex] = 20;
        }
	}
}
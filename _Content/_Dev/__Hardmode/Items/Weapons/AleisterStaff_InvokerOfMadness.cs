using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Weapons
{
	public class AleisterStaff_InvokerOfMadness : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Invoker of Madness");
			// Description.SetDefault("The Crasy Invoker");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().InvokerMadness = true;
        }
	}
}
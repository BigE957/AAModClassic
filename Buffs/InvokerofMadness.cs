using Terraria.ModLoader;
using Terraria;
using AAMod.Items.Dev.Invoker;

namespace AAMod.Buffs
{
	public class InvokerofMadness : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Invoker of Madness");
			// Description.SetDefault("The Crasy Invoker");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<InvokerPlayer>().InvokerMadness = true;
        }
	}
}
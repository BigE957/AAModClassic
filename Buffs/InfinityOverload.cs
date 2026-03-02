using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class InfinityOverload : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elemental Overload");
			// Description.SetDefault("The elements lash out against your very being");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().infinityOverload = true;
		}
	}
}

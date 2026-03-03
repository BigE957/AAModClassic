using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class UnstableSoul : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Unstable Soul");
			// Description.SetDefault("You are now etheral \n" + "You have more invincibility frames, but less defense");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }

		public override void Update(Player player, ref int buffIndex)
		{
            player.longInvince = true;
            player.statDefense -= 10;
            player.armorEffectDrawShadowLokis = true;
		}
	}
}

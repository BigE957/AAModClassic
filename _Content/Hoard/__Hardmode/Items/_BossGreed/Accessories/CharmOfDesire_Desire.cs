using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Accessories
{
    public class CharmOfDesire_Desire : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Desire");
			// Description.SetDefault(@"MORE COINS, MORE POWER!!!");
		}

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
            player.GetDamage(DamageClass.Generic) += player.GetModPlayer<CharmOfDesirePlayer>().BonusDamage / 100f;
        }
	}
}
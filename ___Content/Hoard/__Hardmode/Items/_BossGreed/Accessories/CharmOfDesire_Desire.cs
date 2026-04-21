using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Hoard.__Hardmode.Items._BossGreed.Accessories
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
            player.GetModPlayer<AAPlayer>().Greed1 = true;
            player.GetDamage(DamageClass.Generic) += player.GetModPlayer<AAPlayer>().GreedyDamage / 100f;
        }
	}
}
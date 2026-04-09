using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Greed
{
    public class Charm_Buff : ModBuff
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
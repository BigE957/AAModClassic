using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Accessories
{
    public class TalismanOfDesire_RuthlessDesire : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ruthless Desire");
			// Description.SetDefault("MONEY MONEY MONEY!!!");
		}

		public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
            player.GetModPlayer<AAPlayer>().Greed2 = true;
            player.GetDamage(DamageClass.Generic) += player.GetModPlayer<AAPlayer>().GreedyDamage / 100f;
        }
	}
}
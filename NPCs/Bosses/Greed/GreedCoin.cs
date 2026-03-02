using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Greed
{
    public class GreedCoin : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gold Coin");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GoldCoin);
            Projectile.friendly = false;
            Projectile.hostile = true;
        }
    }
}
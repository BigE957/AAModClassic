using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.NPCs._BossGreedA
{
    public class GreedCoinA : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Platinum Coin");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GoldCoin);
            Projectile.friendly = false;
            Projectile.hostile = true;
        }
    }
}
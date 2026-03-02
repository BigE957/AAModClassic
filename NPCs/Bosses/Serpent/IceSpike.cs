using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Serpent
{
    public class IceSpike : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Blizzard);
            Projectile.hostile = true;
            Projectile.friendly = false;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Ice Spike");
            Main.projFrames[Projectile.type] = 5;
		}
    }
}

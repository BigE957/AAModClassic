using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Magma : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(664);
            Projectile.hostile = true;
            Projectile.friendly = false;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magma");
        }
	
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 1000);
        }
    }
}

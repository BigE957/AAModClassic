using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    class Ryugen : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 28;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Arkhalis);
            AIType = ProjectileID.Arkhalis;
            Projectile.width = 132;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 120;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            //return Color.White;
            return new Color(0, 200, 0, 0) * (1f - (Projectile.alpha / 255f));
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class RadiumSetbonusBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = Projectile.height = 4;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 3;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.minion = true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 0;
            Projectile.localNPCImmunity[target.whoAmI] = -1;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return (targetHitbox.Center() - projHitbox.Center()).Length() < Projectile.ai[0] + Math.Min(targetHitbox.Width, targetHitbox.Height);
        }
    }
}

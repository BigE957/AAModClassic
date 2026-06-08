using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public abstract class StarHelmetMagePlayer_ArmorBonusLeechAbstract : ModProjectile
    {
        protected int dust = 0;
        bool runOnce = true;
        int effectPotency = 0;
        protected float potencyFactor = 1f;
        public virtual void PlayerBenefit(int potency, Player player)
        {
           
        }

        public override void AI()
        {
            if (runOnce)
            {
                runOnce = false;
                Projectile.localNPCImmunity[(int)Projectile.ai[0]] = 0;
            }
            if (effectPotency > 0)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dust);
                Player player = Main.player[Projectile.owner];
                Projectile.velocity = (player.Center - Projectile.Center).SafeNormalize(-Vector2.UnitY) * 12f;
                if (Collision.CheckAABBvAABBCollision(player.position, player.Size, Projectile.position, Projectile.Size))
                {
                    PlayerBenefit(effectPotency, player);
                    Projectile.Kill();
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 0;
            Projectile.localNPCImmunity[target.whoAmI] = -1;
            effectPotency = (int)(hit.Damage * potencyFactor);
            if (effectPotency > 0)
            {
                Projectile.timeLeft = 120;
            }


        }
    }
}

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using AAModClassic._Content.Chaos.Buffs;
using AAModClassic._Content.Mire.Buffs;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons
{
    public class DraconicRipper_ShenDoragonTooth : ModProjectile
    {
        public int type = 0;
        public bool ToothSpawned;
        public override void SetStaticDefaults() //Sets the display name
        {
            // DisplayName.SetDefault("Shen Doragon Tooth");
        }

        public override void SetDefaults() // Clones the bullet defaults
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI() // Executes methods below
        {
            FadeIn();
            FaceDirection();
        }

        public void FadeIn() // Gives the projectile a fade-in effect
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 15; // Decrease alpha, increasing visibility.
            }
        }

        public void FaceDirection() // Forces the bullet to face the direction of travel
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2; // projectile sprite faces up
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(type == 1 ? BuffID.Daybreak : type == 2 ? ModContent.BuffType<Moonraze_Buff>() : ModContent.BuffType<DiscordianInferno_Buff>(), 60);
        }
    }
}

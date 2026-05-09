using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class SoulSiphon_Proj : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Siphon");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 360;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 2;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 2)
                {
                    Projectile.frame = 0;
                }
            }
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[1]++;
                Projectile.alpha -= 5;
            }
            else
            {
                Projectile.alpha += 3;
                Projectile.velocity *= .98f;
            }
            
            if (Projectile.ai[1] > 180)
            {
                Projectile.ai[0] = 1;
            }

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.type == NPCID.TargetDummy)
            {
                return;
            }
            float Heal = damageDone * 0.075f;
            if ((int)Heal == 0)
            {
                return;
            }
            if (Main.LocalPlayer.lifeSteal <= 0f)
            {
                return;
            }
            Main.LocalPlayer.lifeSteal -= Heal;
            int num2 = Projectile.owner;
            Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X, target.position.Y, 0f, 0f, ModContent.ProjectileType<SoulSiphon_Heal>(), 0, 0f, Projectile.owner, num2, Heal);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = oldVelocity.X * -1f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = oldVelocity.Y * -1f;
            }
            return false;
        }
    }
}
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class AsgardianLanceShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = ProjAIStyleID.Beam;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = false;
            AIType = ProjectileID.WaterBolt;
        }

        public override void PostAI()
        {
            Projectile.alpha = 0;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Asgardian Ice");
		}

        public override void OnKill(int timeLeft)
        {
            float spread = 12f * 0.0174f;
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 2;
            double offsetAngle;
            int i;
            for (i = 0; i < 2; i++)
            {
                offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), Mod.Find<ModProjectile>("AsgardianIce").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 6f), (float)(-Math.Cos(offsetAngle) * 6f), Mod.Find<ModProjectile>("AsgardianIce").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 120);
        }
    }
}
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class TrueFleshClaymoreShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;  
            Projectile.width = 32;
            Projectile.height = 32;
			Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
			Projectile.hostile = false;
            Projectile.timeLeft = 900;
        }
		
		public override void AI()
		{
            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.3f / 255f, (255 - Projectile.alpha) * 0.3f / 255f, (255 - Projectile.alpha) * 0f / 255f);
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GoldCoin, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GoldCoin, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GoldCoin, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            Projectile.rotation += Projectile.direction * 0.4f;
            Projectile.spriteDirection = Projectile.direction;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Ichor;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GoldCoin, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flesh Beam");
        }
        
	    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
           target.AddBuff(BuffID.Ichor, 300);
        }

        public override bool? CanCutTiles()
        {
            return true;
        }
    }
}

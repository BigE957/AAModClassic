using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions.Terra
{
    public class SummonShot : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Summon Blast");
            Main.projFrames[Projectile.type] = 4;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (Projectile.position.Y > Main.player[Projectile.owner].position.Y - 300f)
            {
                Projectile.tileCollide = true;
            }
            if (Projectile.position.Y < Main.worldSurface * 16.0)
            {
                Projectile.tileCollide = true;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
            Vector2 position = Projectile.Center + (Vector2.Normalize(Projectile.velocity) * 10f);
            Dust dust20 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.SummonDust>(), 0f, 0f, 0)];
            dust20.position = position;
            dust20.velocity = (Projectile.velocity.RotatedBy(1.5707963705062866) * 0.33f) + (Projectile.velocity / 4f);
            dust20.position += Projectile.velocity.RotatedBy(1.5707963705062866);
            dust20.fadeIn = 0.5f;
            dust20.noGravity = true;
            dust20 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.SummonDust>(), 0f, 0f, 0)];
            dust20.position = position;
            dust20.velocity = (Projectile.velocity.RotatedBy(-1.5707963705062866) * 0.33f) + (Projectile.velocity / 4f);
            dust20.position += Projectile.velocity.RotatedBy(-1.5707963705062866);
            dust20.fadeIn = 0.5f;
            dust20.noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.SummonDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.SummonDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Terrablaze>(), 600);
        }
        

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}
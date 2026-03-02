using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.PostPlant
{
    public class MagicBlast : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Magic Blast");
            Main.projFrames[Projectile.type] = 4;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
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
            Dust dust20 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.TMagicDust>(), 0f, 0f, 0, default, 1f)];
            dust20.position = position;
            dust20.velocity = (Projectile.velocity.RotatedBy(1.5707963705062866, default) * 0.33f) + (Projectile.velocity / 4f);
            dust20.position += Projectile.velocity.RotatedBy(1.5707963705062866, default);
            dust20.fadeIn = 0.5f;
            dust20.noGravity = true;
            dust20 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.TMagicDust>(), 0f, 0f, 0, default, 1f)];
            dust20.position = position;
            dust20.velocity = (Projectile.velocity.RotatedBy(-1.5707963705062866, default) * 0.33f) + (Projectile.velocity / 4f);
            dust20.position += Projectile.velocity.RotatedBy(-1.5707963705062866, default);
            dust20.fadeIn = 0.5f;
            dust20.noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - (spread / 2);
            double deltaAngle = spread / 8f;
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.TMagicDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.TMagicDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
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
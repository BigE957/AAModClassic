using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class MagicBlastF : ModProjectile
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
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
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
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int m = 0; m < 20; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.InfinityOverloadB>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)20 * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;

                int dustID2 = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.InfinityOverloadB>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID2].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)20 * 6.28f);
                Main.dust[dustID2].noLight = false;
                Main.dust[dustID2].noGravity = true;
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
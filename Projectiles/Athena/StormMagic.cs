using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Athena
{
    public class StormMagic : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Varian Burst");
            Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 36;
			Projectile.friendly = false; 
			Projectile.hostile = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 20;
            Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 2)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 3, 0, 0);
            BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, .5f, 1f, 10, false, 0f, 0f, new Color(35, 23, 87), frame, 3);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 3, frame, Color.White, false);
            return false;
        }

        public override void OnKill(int timeleft)
        {
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            int p = Projectile.NewProjectile((int)Projectile.Center.X, (int)Projectile.Center.Y, 0, 0, ProjectileID.Electrosphere, Projectile.damage, Projectile.knockBack, Main.myPlayer);
            Main.projectile[p].Center = Projectile.Center;
            for (int num468 = 0; num468 < 10; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Electric, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
            
        }
    }
}

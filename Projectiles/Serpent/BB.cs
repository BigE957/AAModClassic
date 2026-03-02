
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Serpent

{
    public class BB : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("BB");
        }


        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenBoomerang);
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Player p = Main.player[Projectile.owner];
            BaseAI.AIBoomerang(Projectile, ref Projectile.ai, p.position, p.width, p.height, true, 16f, 20, Projectile.ai[0] == 0 ? 0.8f : 1.2f, .3f, false);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.IceDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
            }
            target.AddBuff(BuffID.Chilled, 200);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.IceDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
            }
            return true;
        }
    }
}

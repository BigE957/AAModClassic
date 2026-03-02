using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Akuma
{
    public class FireProjEX : ModProjectile
    {
        public int noTileHitCounter = 120;

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 2;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
			if (Projectile.direction == 1) 	Projectile.rotation += 0.1f;
			else Projectile.rotation -= 0.1f;
			
            if (Projectile.position.Y > Main.player[Projectile.owner].position.Y - 300f)
            {
                Projectile.tileCollide = true;
            }
            if (Projectile.position.Y < Main.worldSurface * 16.0)
            {
                Projectile.tileCollide = true;
            }
            Vector2 position = Projectile.Center + (Vector2.Normalize(Projectile.velocity) * 10f);
            for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0);
                
                Main.dust[num190].scale *= 1.3f;
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for(int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, 1, ModContent.DustType<Dusts.AkumaADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("FireProjEXBoom").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Mod.GetTexture("Projectiles/Akuma/FireProjEX1");
			if (Projectile.ai[0] == 2f) texture = Mod.GetTexture("Projectiles/Akuma/FireProjEX2");
			if (Projectile.ai[0] == 3f) texture = Mod.GetTexture("Projectiles/Akuma/FireProjEX3");
			if (Projectile.ai[0] == 4f) texture = Mod.GetTexture("Projectiles/Akuma/FireProjEX4");
			if (Projectile.ai[0] == 5f) texture = Mod.GetTexture("Projectiles/Akuma/FireProjEX5");
            spriteBatch.Draw(texture, new Vector2(Projectile.Center.X - Main.screenPosition.X, Projectile.Center.Y - Main.screenPosition.Y + 2),
                        new Rectangle(0, 0, texture.Width, texture.Height), Color.White, Projectile.rotation,
                        new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f), 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}
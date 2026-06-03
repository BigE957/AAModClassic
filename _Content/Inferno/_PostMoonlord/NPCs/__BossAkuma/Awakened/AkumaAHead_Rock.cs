using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened
{
    public class AkumaAHead_Rock : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Volcano Rock");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            Projectile.velocity.Y += .03f;
            if (Projectile.position.Y > Main.player[Projectile.owner].position.Y - 300f)
			{
				Projectile.tileCollide = true;
			}
			if (Projectile.position.Y < Main.worldSurface * 16.0)
			{
				Projectile.tileCollide = true;
			}
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.velocity.Y *= 1.01f;
            SoundEngine.PlaySound(SoundID.Item124);
            Projectile.position.X = Projectile.position.X + Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
			Projectile.width = (int)(128f * Projectile.scale);
			Projectile.height = (int)(128f * Projectile.scale);
			Projectile.position.X = Projectile.position.X - Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
			for (int num336 = 0; num336 < 8; num336++)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, new Color(255, Main.DiscoG, 0), 1.5f);
			}
			for (int num337 = 0; num337 < 32; num337++)
			{
				int num338 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, new Color(255, Main.DiscoG, 0), 2.5f);
				Main.dust[num338].noGravity = true;
				Main.dust[num338].velocity *= 3f;
				num338 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, new Color(255, Main.DiscoG, 0), 1.5f);
				Main.dust[num338].velocity *= 2f;
				Main.dust[num338].noGravity = true;
			}
			for (int num339 = 0; num339 < 2; num339++)
			{
				int num340 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + new Vector2(Projectile.width * Main.rand.Next(100) / 100f, Projectile.height * Main.rand.Next(100) / 100f) - Vector2.One * 10f, default, Main.rand.Next(61, 64), 1f);
				Main.gore[num340].velocity *= 0.3f;
				Gore expr_B4D2_cp_0 = Main.gore[num340];
				expr_B4D2_cp_0.velocity.X += Main.rand.Next(-10, 11) * 0.05f;
				Gore expr_B502_cp_0 = Main.gore[num340];
				expr_B502_cp_0.velocity.Y += Main.rand.Next(-10, 11) * 0.05f;
			}
			if (Projectile.owner == Main.myPlayer)
			{
				Projectile.localAI[1] = -1f;
				Projectile.maxPenetrate = 0;
				Projectile.Damage();
			}
			for (int num341 = 0; num341 < 5; num341++)
			{
				int num342 = Utils.SelectRandom(Main.rand, new int[]
				{
                    ModContent.DustType<Dusts.AkumaADust>(),
                    ModContent.DustType<Dusts.AkumaDust>()
                });
				int num343 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, num342, 2.5f * Projectile.direction, -2.5f, 0, new Color(255, Main.DiscoG, 0), 1f);
				Main.dust[num343].alpha = 200;
				Main.dust[num343].velocity *= 2.4f;
				Main.dust[num343].scale += Main.rand.NextFloat();
			}

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<AkumaAHead_Boom>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }

        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            string RockType = Utils.SelectRandom(Main.rand, new string[]
            {
                "_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Awakened/AkumaRock", "_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Awakened/AkumaRock1", "_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Awakened/AkumaRock2"
            });

            Texture2D Rock = ModContent.Request<Texture2D>(RockType);
            float rot = projectile.rotation;
            BaseDrawing.DrawTexture(spriteBatch, ModContent.Request<Texture2D>(RockType), 0, projectile, projectile.GetAlpha(Color.White));
            return false;
        }*/
    }
}
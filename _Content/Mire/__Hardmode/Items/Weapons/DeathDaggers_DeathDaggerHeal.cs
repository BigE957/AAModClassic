using System;
using AAModClassic.Assets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Weapons
{
    public class DeathDaggers_DeathDaggerHeal : ModProjectile
    {
        public override string Texture => AssetDirectory.General.Nothing;
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Heal");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 480;
        }

        public override void AI()
        {
			Projectile.velocity.X *= 1.01f;
			Projectile.velocity.Y *= 1.01f;
			int num487 = (int)Projectile.ai[0];
			Vector2 vector36 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
			float num489 = Main.player[num487].Center.X - vector36.X;
			float num490 = Main.player[num487].Center.Y - vector36.Y;
			float num491 = (float)Math.Sqrt(num489 * num489 + num490 * num490);
			if (num491 < 50f && Projectile.position.X < Main.player[num487].position.X + Main.player[num487].width && Projectile.position.X + Projectile.width > Main.player[num487].position.X && Projectile.position.Y < Main.player[num487].position.Y + Main.player[num487].height && Projectile.position.Y + Projectile.height > Main.player[num487].position.Y)
			{
				if (Projectile.owner == Main.myPlayer)
				{
					Main.player[num487].HealEffect(1, false);
					Main.player[num487].statLife += 1;
					if (Main.player[num487].statLife > Main.player[num487].statLifeMax2)
					{
						Main.player[num487].statLife = Main.player[num487].statLifeMax2;
					}
					NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, num487, 1, 0f, 0f, 0, 0, 0);
				}
				Projectile.Kill();
            }
            float num488 = 5.5f;
            num491 = num488 / num491;
            num489 *= num491;
            num490 *= num491;
            Projectile.velocity.X = (Projectile.velocity.X * 15f + num489) / 16f;
            Projectile.velocity.Y = (Projectile.velocity.Y * 15f + num490) / 16f;
            for (int num493 = 0; num493 < 3; num493++)
            {
                float num494 = Projectile.velocity.X * 0.334f * num493;
                float num495 = -(Projectile.velocity.Y * 0.334f) * num493;
                int num496 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, default, 1.1f);
                Main.dust[num496].noGravity = true;
                Main.dust[num496].velocity *= 0f;
                Dust expr_153E2_cp_0 = Main.dust[num496];
                expr_153E2_cp_0.position.X -= num494;
                Dust expr_15401_cp_0 = Main.dust[num496];
                expr_15401_cp_0.position.Y -= num495;
            }
            for (int num497 = 0; num497 < 5; num497++)
            {
                float num498 = Projectile.velocity.X * 0.2f * num497;
                float num499 = -(Projectile.velocity.Y * 0.2f) * num497;
                int num500 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, default, 1.3f);
                Main.dust[num500].noGravity = true;
                Main.dust[num500].velocity *= 0f;
                Dust expr_154F9_cp_0 = Main.dust[num500];
                expr_154F9_cp_0.position.X -= num498;
                Dust expr_15518_cp_0 = Main.dust[num500];
                expr_15518_cp_0.position.Y -= num499;
            }
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			Projectile.position.X = Projectile.position.X + Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.position.X = Projectile.position.X - Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
			for (int num621 = 0; num621 < 10; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, default, 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.NextBool(2))
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 15; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, default, 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, default, 2f);
				Main.dust[num624].velocity *= 2f;
			}
        }
    }
}
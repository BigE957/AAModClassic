using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Ammo
{
    public class M79Round_Proj : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("M79 Round");
		}
		
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(133);
			Projectile.aiStyle = ProjAIStyleID.Explosive;
			AIType = ProjectileID.GrenadeI;
        }

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.type = ProjectileID.GrenadeI;
			Projectile.timeLeft = 3;
			return true;
		}
		
		public override void OnKill(int timeLeft)
		{
			Projectile.type = ProjectileID.GrenadeI;
			SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
			int num3;
			for (int num729 = 0; num729 < 30; num729 = num3 + 1)
			{
				int num730 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
				Dust dust = Main.dust[num730];
				dust.velocity *= 1.4f;
				num3 = num729;
			}
			for (int num731 = 0; num731 < 20; num731 = num3 + 1)
			{
				int num732 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3.5f);
				Main.dust[num732].noGravity = true;
				Dust dust = Main.dust[num732];
				dust.velocity *= 7f;
				num732 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
				dust = Main.dust[num732];
				dust.velocity *= 3f;
				num3 = num731;
			}
			if(!Main.dedServ)
				for (int num733 = 0; num733 < 2; num733 = num3 + 1)
				{
					float scaleFactor9 = 0.4f;
					if (num733 == 1)
						scaleFactor9 = 0.8f;
					
					int num734 = Gore.NewGore(Projectile.GetSource_Death(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
					Gore gore = Main.gore[num734];
					gore.velocity *= scaleFactor9;
					Gore var_503_191DA_cp_0_cp_0 = Main.gore[num734];
					var_503_191DA_cp_0_cp_0.velocity.X += 1f;
					Gore var_503_1920A_cp_0_cp_0 = Main.gore[num734];
					var_503_1920A_cp_0_cp_0.velocity.Y += 1f;
					num734 = Gore.NewGore(Projectile.GetSource_Death(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
					gore = Main.gore[num734];
					gore.velocity *= scaleFactor9;
					Gore var_503_192A4_cp_0_cp_0 = Main.gore[num734];
					var_503_192A4_cp_0_cp_0.velocity.X -= 1f;
					Gore var_503_192D4_cp_0_cp_0 = Main.gore[num734];
					var_503_192D4_cp_0_cp_0.velocity.Y += 1f;
					num734 = Gore.NewGore(Projectile.GetSource_Death(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
					gore = Main.gore[num734];
					gore.velocity *= scaleFactor9;
					Gore var_503_1936E_cp_0_cp_0 = Main.gore[num734];
					var_503_1936E_cp_0_cp_0.velocity.X += 1f;
					Gore var_503_1939E_cp_0_cp_0 = Main.gore[num734];
					var_503_1939E_cp_0_cp_0.velocity.Y -= 1f;
					num734 = Gore.NewGore(Projectile.GetSource_Death(), new Vector2(Projectile.position.X, Projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
					gore = Main.gore[num734];
					gore.velocity *= scaleFactor9;
					Gore var_503_19438_cp_0_cp_0 = Main.gore[num734];
					var_503_19438_cp_0_cp_0.velocity.X -= 1f;
					Gore var_503_19468_cp_0_cp_0 = Main.gore[num734];
					var_503_19468_cp_0_cp_0.velocity.Y -= 1f;
					num3 = num733;
				}
		}
    }
}
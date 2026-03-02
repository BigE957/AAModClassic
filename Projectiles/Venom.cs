using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Venom : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Venom");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 6;
            Projectile.extraUpdates = 2;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
			Projectile.scale -= 0.002f;
			if (Projectile.scale <= 0f)
			{
				Projectile.Kill();
			}
			if (Projectile.ai[0] <= 3f)
			{
				Projectile.ai[0] += 1f;
				return;
			}
			Projectile.velocity.Y = Projectile.velocity.Y + 0.075f;
			for (int num151 = 0; num151 < 3; num151++)
			{
				float num152 = Projectile.velocity.X / 3f * num151;
				float num153 = Projectile.velocity.Y / 3f * num151;
				int num154 = 14;
				int num155 = Dust.NewDust(new Vector2(Projectile.position.X + num154, Projectile.position.Y + num154), Projectile.width - num154 * 2, Projectile.height - num154 * 2, ModContent.DustType<Dusts.AcidDust>(), 0f, 0f, 100);
				Main.dust[num155].noGravity = true;
				Main.dust[num155].velocity *= 0.1f;
				Main.dust[num155].velocity += Projectile.velocity * 0.5f;
				Dust expr_6A04_cp_0 = Main.dust[num155];
				expr_6A04_cp_0.position.X -= num152;
				Dust expr_6A1F_cp_0 = Main.dust[num155];
				expr_6A1F_cp_0.position.Y -= num153;
			}
			if (Main.rand.Next(8) == 0)
			{
				int num156 = 16;
				int num157 = Dust.NewDust(new Vector2(Projectile.position.X + num156, Projectile.position.Y + num156), Projectile.width - num156 * 2, Projectile.height - num156 * 2, ModContent.DustType<Dusts.AcidDust>(), 0f, 0f, 100, default, 0.5f);
				Main.dust[num157].velocity *= 0.25f;
				Main.dust[num157].velocity += Projectile.velocity * 0.5f;
				return;
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.immune[Projectile.owner] = 8;
        	target.AddBuff(BuffID.Poisoned, 400);
        }
    }
}
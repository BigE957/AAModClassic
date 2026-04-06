using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Weapons
{
    public class Depthsprayer_Proj : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetDefaults()
        {
            Projectile.width = 10;
			Projectile.height = 16;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.alpha = 255;
			Projectile.penetrate = 5;
			Projectile.extraUpdates = 2;
			Projectile.ignoreWater = true;
			Projectile.DamageType = DamageClass.Magic;
            
        }
		
        public override void AI()
        {
			Projectile.scale -= 0.001f;
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
			int num3;
			for (int num153 = 0; num153 < 3; num153 = num3 + 1)
			{
				float num154 = Projectile.velocity.X / 3f * num153;
				float num155 = Projectile.velocity.Y / 3f * num153;
				int num156 = 14;
				int num157 = Dust.NewDust(new Vector2(Projectile.position.X + num156, Projectile.position.Y + num156), Projectile.width - num156 * 2, Projectile.height - num156 * 2, ModContent.DustType<Dusts.HydraDust>(), 0f, 0f, 100);
				Main.dust[num157].noGravity = true;
				Dust dust = Main.dust[num157];
				dust.velocity *= 0.1f;
				dust = Main.dust[num157];
				dust.velocity += Projectile.velocity * 0.5f;
				Dust var_2_69A9_cp_0_cp_0 = Main.dust[num157];
				var_2_69A9_cp_0_cp_0.position.X -= num154;
				Dust var_2_69C3_cp_0_cp_0 = Main.dust[num157];
				var_2_69C3_cp_0_cp_0.position.Y -= num155;
				num3 = num153;
			}
			if (Main.rand.Next(8) == 0)
			{
				int num158 = 16;
				int num159 = Dust.NewDust(new Vector2(Projectile.position.X + num158, Projectile.position.Y + num158), Projectile.width - num158 * 2, Projectile.height - num158 * 2, ModContent.DustType<Dusts.HydraDust>(), 0f, 0f, 100, default, 0.5f);
				Dust dust = Main.dust[num159];
				dust.velocity *= 0.25f;
				dust = Main.dust[num159];
				dust.velocity += Projectile.velocity * 0.5f;
				return;
			}
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.immune[Projectile.owner] = 6;
            target.AddBuff(ModContent.BuffType<Buffs.HydraToxin_Buff>(), 300);
        }
    }
}

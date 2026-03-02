using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Ammo
{
    public class DaybreakBullet : ModProjectile
    {
        
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.light = 0.5f;
            Projectile.alpha = 30;
            Projectile.extraUpdates = 2;
            Projectile.scale = 1.3f;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.Bullet;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Antimatter");
		}

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, .1f, .5f, 1f);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int num565 = 0; num565 < 7; num565++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 1.5f);
            }
            for (int num566 = 0; num566 < 3; num566++)
            {
                int num567 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100);
                Main.dust[num567].noGravity = true;
                Main.dust[num567].velocity *= 3f;
                num567 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100);
                Main.dust[num567].velocity *= 2f;
            }
            int num568 = Gore.NewGore(new Vector2(Projectile.position.X - 10f, Projectile.position.Y - 10f), default, Main.rand.Next(61, 64), 1f);
            Main.gore[num568].velocity *= 0.3f;
            Gore expr_12836_cp_0 = Main.gore[num568];
            expr_12836_cp_0.velocity.X += Main.rand.Next(-10, 11) * 0.05f;
            Gore expr_12866_cp_0 = Main.gore[num568];
            expr_12866_cp_0.velocity.Y += Main.rand.Next(-10, 11) * 0.05f;
            if (Projectile.owner == Main.myPlayer)
            {
                Projectile.localAI[1] = -1f;
                Projectile.maxPenetrate = 0;
                Projectile.position.X = Projectile.position.X + (Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y + (Projectile.height / 2);
                Projectile.width = 120;
                Projectile.height = 120;
                Projectile.position.X = Projectile.position.X - (Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y - (Projectile.height / 2);
                Projectile.Damage();
            }
        }




        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 5;
            { }
            target.AddBuff(BuffID.Daybreak, 200);
            int proj = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("FireProjBoom").Type, Projectile.damage / 6, Projectile.knockBack, Projectile.owner, 0f, 0f);
            Main.projectile[proj].melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
            Main.projectile[proj].DamageType = DamageClass.Ranged;

        }
    }
}

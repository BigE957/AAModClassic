using System;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Weapons
{
    public class DeathDagger : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Death Dagger");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
        	Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 20f)
			{
				Projectile.alpha += 3;
				Projectile.damage = (int)(Projectile.damage * 0.95);
				Projectile.knockBack = (int)(Projectile.knockBack * 0.95);
			}
			if (Projectile.ai[0] < 20f)
			{
				Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
			}
			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
			}
        	float num472 = Projectile.Center.X;
			float num473 = Projectile.Center.Y;
			bool flag17 = false;
			if (flag17)
			{
				float num483 = 18f;
				Vector2 vector35 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
				float num484 = num472 - vector35.X;
				float num485 = num473 - vector35.Y;
				float num486 = (float)Math.Sqrt(num484 * num484 + num485 * num485);
				num486 = num483 / num486;
				num484 *= num486;
				num485 *= num486;
				Projectile.velocity.X = (Projectile.velocity.X * 20f + num484) / 21f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num485) / 21f;
				return;
			}
            if (Main.rand.Next(6) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<AbyssDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }
        
        public override void OnKill(int timeLeft)
        {
            for (int num303 = 0; num303 < 3; num303++)
			{
				int num304 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AbyssDust>(), 0f, 0f, 100, default, 0.8f);
				Main.dust[num304].noGravity = true;
				Main.dust[num304].velocity *= 1.2f;
				Main.dust[num304].velocity -= Projectile.oldVelocity * 0.3f;
			}
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(ModContent.BuffType<Buffs.HydraToxin>(), 120);
        	if (target.type == NPCID.TargetDummy)
			{
				return;
			}
        	float num = damageDone * 0.075f;
			if ((int)num == 0)
			{
				return;
			}
			if (Main.LocalPlayer.lifeSteal <= 0f)
			{
				return;
			}
			Main.LocalPlayer.lifeSteal -= num;
			int num2 = Projectile.owner;
			Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X, target.position.Y, 0f, 0f, ModContent.ProjectileType<DeathDaggerHeal>(), 0, 0f, Projectile.owner, num2, num);
        }
    }
}
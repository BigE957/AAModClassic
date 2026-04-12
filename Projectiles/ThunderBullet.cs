using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ThunderBullet : ModProjectile
	{
        //Thank you Qwerty3.14 for letting us use his Oricalcum bullet code.
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Thundershot");
		}

		public override void SetDefaults()
		{
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            AIType = ProjectileID.Bullet;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 5;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.extraUpdates = 8;
        }

        public bool runOnce = true;
        float maxSpeed;
        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Electric, -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].noGravity = false;
            }
            if (runOnce)
            {
                maxSpeed = Projectile.velocity.Length();
                runOnce = false;
            }
        }
        public bool firstHit = true;

        NPC ConfirmedTarget;
        NPC possibleTarget;
        float distance;
        float maxDistance = 2000;
        bool foundTarget;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            SoundEngine.PlaySound(SoundID.Item124);
            Projectile.localNPCImmunity[target.whoAmI] = -1;
            target.immune[Projectile.owner] = 0;
            target.AddBuff(ModContent.BuffType<Electrified_Buff>(), 500);
            if(target.life<=0)
           {
              Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<ThunderBoom>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);             
            SoundEngine.PlaySound(SoundID.Item124);
           }
            for (int k = 0; k < 200; k++)
            {
                possibleTarget = Main.npc[k];
                distance = (possibleTarget.Center - Projectile.Center).Length();
                if (distance < maxDistance && possibleTarget.active && !possibleTarget.dontTakeDamage && Projectile.localNPCImmunity[k] >= 0 && !possibleTarget.friendly && possibleTarget.lifeMax > 5 && !possibleTarget.immortal && Collision.CanHit(Projectile.Center, 0, 0, possibleTarget.Center, 0, 0))
                {
                    ConfirmedTarget = Main.npc[k];
                    foundTarget = true;


                    maxDistance = (ConfirmedTarget.Center - Projectile.Center).Length();
                }

            }
            if (foundTarget)
            {
                Projectile.velocity = PolarVector(maxSpeed, (ConfirmedTarget.Center - Projectile.Center).ToRotation());

            }
            else
            {
                Projectile.Kill();
            }
            foundTarget = false;
            maxDistance = 1000;
        }

        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 5; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Electric, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 0f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Electric, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}


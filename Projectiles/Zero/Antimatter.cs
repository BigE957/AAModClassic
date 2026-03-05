using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Zero
{
    public class Antimatter : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        //Thank you Qwerty3.14 for letting us use his Oricalcum bullet code.
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.extraUpdates = 50;
            Projectile.timeLeft = 1000;
            Projectile.penetrate = 3;
            Projectile.tileCollide = false;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Antimatter");
		}

        public bool runOnce = true;
        float maxSpeed;
        public override void AI()
        {
            if (runOnce)
            {
                maxSpeed = Projectile.velocity.Length();
                runOnce = false;
            }
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int num447 = 0; num447 < 4; num447++)
                {
                    Vector2 vector33 = Projectile.position;
                    vector33 -= Projectile.velocity * (num447 * 0.25f);
                    Projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 200);
                    Main.dust[num448].position = vector33;
                    Main.dust[num448].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[num448].velocity *= 0.2f;
                    Main.dust[num448].noGravity = true;
                }
                return;
            }
        }
        public bool firstHit = true;

        NPC ConfirmedTarget;
        NPC possibleTarget;
        float distance;
        float maxDistance = 1200;
        bool foundTarget;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            SoundEngine.PlaySound(soundID.Item124);
            Projectile.localNPCImmunity[target.whoAmI] = -1;
            target.immune[Projectile.owner] = 0;

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
            maxDistance = 300;
        }

        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }

    }
}

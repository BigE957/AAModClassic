using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    public class GRocket2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("R0CKET");
            Main.projFrames[Projectile.type] = 3;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            CooldownSlot = 1;
            Projectile.damage = 6;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.9f / 255f, (255 - Projectile.alpha) * 0f / 255f, (255 - Projectile.alpha) * 0.4f / 255f);

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[1]++;
            if (Projectile.localAI[1] >= 60)
            {

                if (Projectile.localAI[0] == 0f)
                {
                    AdjustMagnitude(ref Projectile.velocity);
                    Projectile.localAI[0] = 1f;
                }
                Vector2 move = Vector2.Zero;
                float distance = 800f;
                bool target = false;
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    if (Main.npc[k].active)
                    {
                        Vector2 newMove = Main.npc[k].Center - Projectile.Center;
                        float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                        if (distanceTo < distance)
                        {
                            move = newMove;
                            distance = distanceTo;
                            target = true;
                        }
                    }
                }
                if (target)
                {
                    AdjustMagnitude(ref move);
                    Projectile.velocity = (10 * Projectile.velocity + move) / 11f;
                    AdjustMagnitude(ref Projectile.velocity);
                }
            }
        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 13f)
            {
                vector *= 12f / magnitude;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Glitch"), (int)Projectile.Center.X, (int)Projectile.Center.Y);
            float spread = 12f * 0.0174f;
			double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y)- spread/2;
	    	double Angle = spread/4f;
	    	double offsetAngle;
	    	int i;
	    	if (Projectile.owner == Main.myPlayer)
	    	{
		    	for (i = 0; i < 4; i++ )
		    	{
		   			offsetAngle = startAngle + Angle * ( i + i * i ) / 2f  + 32f * i;
		        	Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 2f ), (float)( Math.Cos(offsetAngle) * 6f ), Mod.Find<ModProjectile>("GBlast").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
		        	Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 2f ), (float)( -Math.Cos(offsetAngle) * 6f ), Mod.Find<ModProjectile>("GBlast").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
		    	}
	    	}
        }
    }
}
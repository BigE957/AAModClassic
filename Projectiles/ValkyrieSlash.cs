using System;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ValkyrieSlash : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Valkyrie Slash");
			Main.projFrames[Projectile.type] = 28;
		}
    	
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Arkhalis);
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.aiStyle = ProjAIStyleID.HeldProjectile;
			Projectile.netUpdate = true;
            AIType = ProjectileID.Arkhalis;
        }
        public override void AI()
        {
			if (Main.myPlayer == Projectile.owner)
            {
                //Do net updatey thing. Syncs this projectile.
				if (Main.rand.NextBool(3))
                {
                 int num30 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 100, default, 2f);
                    Main.dust[num30].noGravity = true;
                    Main.dust[num30].position -= Projectile.velocity;
                }
                Projectile.netUpdate = true;
                Vector2 mouse = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
                if (Main.player[Projectile.owner].Center.Y < mouse.Y)
                {
                    float Xdis = Main.LocalPlayer.Center.X - mouse.X;  // change myplayer to nearest player in full version
                    float Ydis = Main.LocalPlayer.Center.Y - mouse.Y; // change myplayer to nearest player in full version
                    float Angle = (float)Math.Atan(Xdis / Ydis);
                    float DistXT = (float)(Math.Sin(Angle) * 29);
                    float DistYT = (float)(Math.Cos(Angle) * 29);
                    Projectile.position.X = Main.player[Projectile.owner].Center.X + DistXT - 30;
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y + DistYT - 30;
                }
                if (Main.player[Projectile.owner].Center.Y >= mouse.Y)
                {
                    float Xdis = Main.LocalPlayer.Center.X - mouse.X;  // change myplayer to nearest player in full version
                    float Ydis = Main.LocalPlayer.Center.Y - mouse.Y; // change myplayer to nearest player in full version
                    float Angle = (float)Math.Atan(Xdis / Ydis);
                    float DistXT = (float)(Math.Sin(Angle) * 29);
                    float DistYT = (float)(Math.Cos(Angle) * 29);
                    Projectile.position.X = Main.player[Projectile.owner].Center.X + (0 - DistXT) - 30;
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y + (0 - DistYT) - 30;
                }
            }
			
        }
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 12;
		}

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return projHitbox.Intersects(targetHitbox);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Snow;
        }
    }
}
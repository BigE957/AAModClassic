using System;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Projectiles
{
    public class TrueTerraRose : ModProjectile
	{
		public static Color lightColor = new Color(0, 150, 50);

		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Terra Rose");
			Main.projFrames[Projectile.type] = 2;
		}	

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 320;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.damage = 1;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Magic;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 9;
        }

		public override void AI()
		{
			BaseAI.AIVilethorn(Projectile, 50, 4, 20);
			if (Projectile.ai[1] == 20)
			{
				Projectile.frame = 0;
			}
			else
			{
				Projectile.frame = 1;
			}
		}

		public override void PostAI()
		{
			if (Main.netMode != NetmodeID.Server && Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
			{
				for (int j = 0; j < 4; j++)
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Cobalt, Projectile.velocity.X * 0.025f, Projectile.velocity.Y * 0.025f, 107, Color.White, j == 0 ? 1.1f : 1.2f);
				}
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, 34, 34, 0, 0);

			Color newLightColor = new Color(Math.Max(0, lightColor.R + Math.Min(0, -Projectile.alpha + 20)), Math.Max(0, lightColor.G + Math.Min(0, -Projectile.alpha + 20)), Math.Max(0, lightColor.B + Math.Min(0, -Projectile.alpha + 20)));
			BaseDrawing.AddLight(Projectile.Center, newLightColor);
			BaseDrawing.DrawTexture(sb, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 2, frame, Projectile.GetAlpha(Color.White), true);
			return false;
		}
	}
}
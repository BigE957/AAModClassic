using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother
{
    public class Broodmother_MagmaBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magma Ball");
        }

        public override void SetDefaults()
        {
            Projectile.height = 22;
            Projectile.width = 22;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
			Projectile.rotation += Projectile.velocity.Length() * 0.025f;
            Projectile.velocity.Y += .15f;

            bool explode = false;
            for(int i = 0; i < 255 && !explode; i++)
            {
                if(Main.player[i].active && Math.Abs(Main.player[i].Center.X - Projectile.Center.X) + Math.Abs(Main.player[i].Center.Y - Projectile.Center.Y) < 66)
                {
                    explode = true;
                }
            }

            Vector2 tile = new Vector2(Projectile.Center.X, Projectile.Center.Y + Projectile.height / 2);
            bool tileCheck = TileID.Sets.Platforms[Main.tile[(int)(tile.X / 16), (int)(tile.Y / 16)].TileType];
            if (tileCheck) 
            {
                Projectile.velocity.X = 0f;
                Projectile.velocity.Y = 0f;
                if(explode) Projectile.Kill();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bool explode = false;
            for(int i = 0; i < 255 && !explode; i++)
            {
                if(Main.player[i].active && Math.Abs(Main.player[i].Center.X - Projectile.Center.X) + Math.Abs(Main.player[i].Center.Y - Projectile.Center.Y) < 66)
                {
                    explode = true;
                }
            }
            if(explode) Projectile.Kill();
            return explode;
        }
		
        public override void OnKill(int timeLeft)
        {
            for (int num468 = 0; num468 < 30; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, 1, ModContent.DustType<Dusts.BroodmotherDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.BroodmotherDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				int projID = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Top.X, Projectile.Top.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<Broodmother_MagmaExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[projID].Bottom = Projectile.Bottom + new Vector2(0, 10);
				Main.projectile[projID].netUpdate = true;
			}
        }

		public override Color? GetAlpha(Color lightColor)
		{
			return ColorUtils.COLOR_GLOWPULSE;
		}
    }
}
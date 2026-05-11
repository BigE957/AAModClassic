using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu
{
    internal class CthulhuNuke : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            // DisplayName.SetDefault("Reality Bomb");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.scale = 1.1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 60;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            if (Projectile.timeLeft > 0)
            {
                Projectile.timeLeft--;
            }
            if (Projectile.timeLeft == 0)
            {
                Projectile.Kill();
            }

            Projectile.frameCounter++;
            if (Projectile.frameCounter > 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            
            for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 0, default(Color), 1f);

                Main.dust[num190].scale *= 1.3f;
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
        }

        private bool TileHit = false;

        private bool PlayerHit = false;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            TileHit = true;
            return true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            PlayerHit = true;
        }


        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, ModContent.DustType<CthulhuDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0, default(Color), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, ModContent.DustType<CthulhuDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0, default(Color), 1f);
                Main.dust[num469].velocity *= 2f;
            }
            if (TileHit)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y + 20, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<CthulhuStrike>(), Projectile.damage, 1, 255);
            }
            if (PlayerHit)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y + 20, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<Cthulhusplosion>(), Projectile.damage, 1, 255);
            }
            
        }
    }
}
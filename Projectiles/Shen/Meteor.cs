using AAModClassic.___Content.Chaos.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Shen
{
    public class Meteor : ModProjectile
    {
        public int noTileHitCounter = 120;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 150);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.extraUpdates = 2;
            Projectile.aiStyle = 0;
        }

        public bool EnemyHit = false;
        public bool TileHit = false;

        public override void AI()
        {
            if (Projectile.position.Y > Main.player[Projectile.owner].position.Y - 300f)
            {
                Projectile.tileCollide = true;
            }
            if (Projectile.position.Y < Main.worldSurface * 16.0)
            {
                Projectile.tileCollide = true;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
            Vector2 position = Projectile.Center + (Vector2.Normalize(Projectile.velocity) * 10f);
            for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.Discord_Dust>(), 0f, 0f, 0);
                
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            TileHit = true;
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            EnemyHit = true;
            target.AddBuff(ModContent.BuffType<DiscordianInferno_Buff>(), 600);
        }

        public override void OnKill(int timeLeft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, 1, ModContent.DustType<Dusts.Discord_Dust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.Discord_Dust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            if (TileHit)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 30, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<MeteorStrike_Proj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Magic;
            }
            if (EnemyHit)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 30, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<MeteorBoom>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Magic;
            }
        }

        

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}
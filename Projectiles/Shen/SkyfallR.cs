using AAModClassic.___Content.Mire.Buffs;
using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Shen
{
    public class SkyfallR : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skyfall");

            Main.projFrames[Projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 5;
            Projectile.penetrate = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.alpha = 255;
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
            Vector2 position = Projectile.Center + (Vector2.Normalize(Projectile.velocity) * 10f);
            bool flag5 = WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.position.X / 16, (int)Projectile.position.Y / 16));
            Dust dust19 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 0, default, 1f)];
            dust19.position = Projectile.Center;
            dust19.velocity = Vector2.Zero;
            dust19.noGravity = true;
            Dust dust18 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 0, default, 1f)];
            dust18.position = Projectile.Center;
            dust18.velocity = Vector2.Zero;
            dust18.noGravity = true;
            if (flag5)
            {
                dust19.noLight = true;
                dust18.noLight = true;
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
            target.AddBuff(ModContent.BuffType<Moonraze_Buff>(), 600);
        }

        public override void OnKill(int timeLeft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, 1, ModContent.DustType<Dusts.YamataADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            if (TileHit)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 30, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<MeteorStrikeRed>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Ranged;
            }
            if (EnemyHit)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 30, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<MeteorBoomRed>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Ranged;
            }
        }
    }
}
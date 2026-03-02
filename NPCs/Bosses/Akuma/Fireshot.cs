using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Akuma
{
    public class FireShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;      
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LaserMachinegunLaser);
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.aiStyle = 1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.alpha = 50;
            Projectile.scale = 1f;
            Projectile.timeLeft = 100;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 2)
                {
                    Projectile.frame = 0;
                }
            }
            if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = (float)Math.Atan2(-Projectile.velocity.Y, -Projectile.velocity.X);
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            }

            int dustId = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height + 5, ModContent.DustType<Dusts.InfinityOverloadB>(), Projectile.velocity.X * 0.2f,
                Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188), 2f);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height + 5, ModContent.DustType<Dusts.InfinityOverloadB>(), Projectile.velocity.X * 0.2f,
                Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188), 2f);
            Main.dust[dustId3].noGravity = true;
        }

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188));
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("Flare").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }

        
    }
}

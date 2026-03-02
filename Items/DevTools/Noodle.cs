using System;
using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.DevTools
{
    public class Noodle : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("[DEV] Noodle");
        }
    	
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LightBeam);
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 300;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 0;
        }

        public override void AI()
        {
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
            int num557 = 8;
            //dust!
            int dustId = Dust.NewDust(new Vector2(Projectile.position.X + num557, Projectile.position.Y + num557), Projectile.width - num557 * 2, Projectile.height - num557 * 2, DustID.AncientLight, 0f, 0f, 0);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(Projectile.position.X + num557, Projectile.position.Y + num557), Projectile.width - num557 * 2, Projectile.height - num557 * 2, DustID.AncientLight, 0f, 0f, 0);
            Main.dust[dustId3].noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int num579 = 0; num579 < 20; num579++)
            {
                int num580 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.AncientLight, -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num580].noGravity = true;
                Main.dust[num580].velocity *= 2f;
                num580 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.AncientLight, -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100);
                Main.dust[num580].velocity *= 2f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<InfinityOverload>(), 300);
        }
    }
}
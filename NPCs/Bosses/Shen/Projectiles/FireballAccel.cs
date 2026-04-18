using System;
using AAModClassic.___Content.Inferno.Buffs;
using AAModClassic.___Content.Mire.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Shen.Projectiles
{
    public class FireballAccelR : ModProjectile
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Shen/Projectiles/FireballAccelR";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fireball");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void PostAI()
        {
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.timeLeft = 360;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
            CooldownSlot = 1;
        }

        public override void AI()
        {
            Projectile.velocity *= 1f + Math.Abs(Projectile.ai[0]);

            Vector2 acceleration = Projectile.velocity.RotatedBy(Math.PI / 2);
            acceleration *= Projectile.ai[1];
            Projectile.velocity += acceleration;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 180);
        }
    }

    public class FireballAccelB : ModProjectile
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Shen/Projectiles/FireballAccelB";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fireball");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void PostAI()
        {
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.timeLeft = 360;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
            CooldownSlot = 1;
        }

        public override void AI()
        {
            Projectile.velocity *= 1f + Math.Abs(Projectile.ai[0]);

            Vector2 acceleration = Projectile.velocity.RotatedBy(Math.PI / 2);
            acceleration *= Projectile.ai[1];
            Projectile.velocity += acceleration;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataDust>(), 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 180);
        }
    }
}
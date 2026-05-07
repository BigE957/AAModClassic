using System;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs._BossShen.Projectiles
{
    public class FireballFragR : ModProjectile
    {
        public override string Texture => "AAModClassic/_Content/Chaos/_PostMoonlord/NPCs/_BossShen/Projectiles/FireballFragR";
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
            Projectile.timeLeft = 30;
            Projectile.aiStyle = -1;
            CooldownSlot = 1;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 vel = Vector2.Normalize(Projectile.velocity);
                const float ai = 0.01f;
                for (int i = 0; i < 8; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 4);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelR>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), ai);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelR>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), -ai);
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 180);
        }
    }

    public class FireballFragB : ModProjectile
    {
        public override string Texture => "AAModClassic/_Content/Chaos/_PostMoonlord/NPCs/_BossShen/Projectiles/FireballFragB";
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
            Projectile.timeLeft = 30;
            Projectile.aiStyle = -1;
            CooldownSlot = 1;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataDust>(), 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 vel = Vector2.Normalize(Projectile.velocity);
                const float ai = 0.01f;
                for (int i = 0; i < 8; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 4);
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelB>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), ai);
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelB>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), -ai);
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 180);
        }
    }
}
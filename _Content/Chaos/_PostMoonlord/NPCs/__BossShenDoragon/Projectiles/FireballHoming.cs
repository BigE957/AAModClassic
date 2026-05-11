using System;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Projectiles
{
    public class FireballHomingR : ModProjectile
    {
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
            Projectile.scale = 4f;
            Projectile.aiStyle = -1;
            CooldownSlot = 1;
        }

        public override void AI()
        {
            Projectile.velocity = Projectile.DirectionTo(Main.player[(int)Projectile.ai[0]].Center) * Projectile.ai[1];
            if (++Projectile.localAI[0] > 60)
            {
                Projectile.localAI[0] = 0;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 vel = Vector2.Normalize(Projectile.velocity);
                    const float ai = 0.015f;
                    for (int i = 0; i < 16; ++i)
                    {
                        vel = vel.RotatedBy(Math.PI / 8);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelR>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelR>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                    }
                }
            }
            Projectile.scale -= 3f / 300f;
            if (Projectile.scale <= 1)
                Projectile.Kill();
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
                const float ai = 0.015f;
                for (int i = 0; i < 16; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 8);
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelR>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelR>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                }
            }
        }


        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 180);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 4, 0, 0);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, Color.White, true);
            return false;
        }
    }

    public class FireballHomingB : ModProjectile
    {
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
            Projectile.scale = 4f;
            Projectile.aiStyle = -1;
            CooldownSlot = 1;
        }

        public override void AI()
        {
            Projectile.velocity = Projectile.DirectionTo(Main.player[(int)Projectile.ai[0]].Center) * Projectile.ai[1];
            if (++Projectile.localAI[0] > 60)
            {
                Projectile.localAI[0] = 0;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 vel = Vector2.Normalize(Projectile.velocity);
                    const float ai = 0.015f;
                    for (int i = 0; i < 16; ++i)
                    {
                        vel = vel.RotatedBy(Math.PI / 8);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelB>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelB>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                    }
                }
            }
            Projectile.scale -= 3f / 300f;
            if (Projectile.scale <= 1)
                Projectile.Kill();
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
                const float ai = 0.015f;
                for (int i = 0; i < 16; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 8);
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelB>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, vel, ModContent.ProjectileType<FireballAccelB>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 180);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 4, 0, 0);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, Color.White, true);
            return false;
        }
    }
}
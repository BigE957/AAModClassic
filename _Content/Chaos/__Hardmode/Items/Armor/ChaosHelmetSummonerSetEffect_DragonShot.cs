using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Armor
{
    public class ChaosHelmetSummonerSetEffect_DragonShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dynabomb");
            Main.projFrames[Projectile.type] = 5;
        }
    	
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 40;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override void AI()
        {
            int dustType = Main.rand.NextBool(2) ? ModContent.DustType<Dusts.InfinityOverloadR>() : ModContent.DustType<Dusts.InfinityOverloadB>();
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 1)
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
            if (Projectile.alpha <= 0)
            {
                for (int num107 = 0; num107 < 3; num107++)
                {
                    int num108 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType);
                    Main.dust[num108].noGravity = true;
                    Main.dust[num108].velocity *= 0.3f;
                    Main.dust[num108].noLight = true;
                }
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 55;
                Projectile.scale = 1.3f;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                    float num109 = 16f;
                    int num110 = 0;
                    while (num110 < num109)
                    {
                        Vector2 vector14 = Vector2.UnitX * 0f;
                        vector14 += -Vector2.UnitY.RotatedBy(num110 * (6.28318548f / num109)) * new Vector2(1f, 4f);
                        vector14 = vector14.RotatedBy(Projectile.velocity.ToRotation());
                        int num111 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, dustType);
                        Main.dust[num111].scale = 1.5f;
                        Main.dust[num111].noLight = true;
                        Main.dust[num111].noGravity = true;
                        Main.dust[num111].position = Projectile.Center + vector14;
                        Main.dust[num111].velocity = Main.dust[num111].velocity * 4f + Projectile.velocity * 0.3f;
                        num110++;
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Main.rand.NextBool() ? ModContent.BuffType<DragonFire_Buff>() : ModContent.BuffType<HydraToxin_Buff>(), 180);
        }

        public override void OnKill(int timeLeft)
        {
            int dustType = Main.rand.NextBool(2) ? ModContent.DustType<Dusts.InfinityOverloadR>() : ModContent.DustType<Dusts.InfinityOverloadB>();
            Projectile.position = Projectile.Center;
            Projectile.width = Projectile.height = 160;
            Projectile.Center = Projectile.position;
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;
            Projectile.Damage();
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Vector2 position = Projectile.Center + Vector2.One * -20f;
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 4; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, dustType);
                Main.dust[num86].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
            }
            for (int num87 = 0; num87 < 20; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, dustType, 0f, 0f, 200);
                Main.dust[num88].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                num88 = Dust.NewDust(position, num84, height3, dustType, 0f, 0f, 100);
                Main.dust[num88].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].color = AAColor.Shen2 * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 20; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, dustType, 0f, 0f, 0);
                Main.dust[num90].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation()) * num84 / 2f;
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += Projectile.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 70; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, dustType, 0f, 0f, 0);
                Main.dust[num92].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation()) * num84 / 2f;
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += Projectile.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }
    }
}
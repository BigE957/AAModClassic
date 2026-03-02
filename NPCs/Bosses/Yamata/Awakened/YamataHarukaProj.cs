using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    public class YamataHarukaProj : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Haruka Yamata");
            Main.projFrames[Projectile.type] = 11;
		}

        public override void SetDefaults()
        {
            Projectile.width = 82;
            Projectile.height = 74;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 480;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
        }

        const float dashTime = 90;
        public override void AI()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<YamataA>()))
            {
                Projectile.Kill();
                return;
            }
            int ai0 = (int)Projectile.ai[0];
            if (ai0 < 0 || ai0 >= Main.maxPlayers)
            {
                Projectile.Kill();
                return;
            }

            Player player = Main.player[ai0];
            if (Projectile.Center.X > player.Center.X) Projectile.direction = 1;
            else Projectile.direction = -1;
            Projectile.spriteDirection = Projectile.direction;
            if (++Projectile.ai[1] <= dashTime) //move beside player
            {
                Vector2 target = player.Center;
                target.X += Projectile.Center.X < player.Center.X ? -400 : 400;
                MoveToPoint(target);
                if (Projectile.ai[1] == dashTime) //dash
                {
                    Projectile.velocity = Projectile.DirectionTo(player.Center) * 22;
                    Projectile.netUpdate = true;
                }
            }
            else
            {
                Projectile.velocity *= 0.98f;
                if (Projectile.ai[1] > dashTime + 60)
                {
                    Projectile.ai[1] = 0;
                    Projectile.netUpdate = true;
                }
            }
        }

        public override void PostAI()
        {
            if (Projectile.frameCounter++ > 5)
            {
              Projectile.frameCounter = 0;
              Projectile.frame++;
            }

            if (Projectile.ai[1] <= dashTime)
            {
                if (Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
            else
            {
                if (Projectile.frame < 4)
                {
                    Projectile.frame = 4;
                }
                if (Projectile.frame >= 11)
                {
                    Projectile.frame = 7;
                }
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 16f;
            if (Vector2.Distance(Projectile.Center, point) > 500)
                moveSpeed = 25f;
            float velMultiplier = 1f;
            Vector2 dist = point - Projectile.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            Projectile.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            Projectile.velocity *= moveSpeed;
            Projectile.velocity *= velMultiplier;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Vector2 position = Projectile.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, 240, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 200, default, 3.7f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataDust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Crimson * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = Projectile.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += Projectile.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataDust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = Projectile.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += Projectile.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Buffs.HydraToxin>(), 300);
        }
    }
}

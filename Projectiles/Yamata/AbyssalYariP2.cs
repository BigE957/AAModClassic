using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AAMod.Projectiles.Yamata
{
    public class AbyssalYariP2 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 75;
            Projectile.height = 75;
            Projectile.scale = 1f;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 90;
            Projectile.extraUpdates = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 8;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssal Yari");
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.DarkRed.R / 100, Color.DarkRed.G / 100, Color.DarkRed.B / 180);
            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;
            
			if(Projectile.timeLeft < 60)
			{
				Projectile.velocity.Y += Projectile.velocity.Y > 0f ? 0.04f : -0.04f;
				if(Projectile.velocity.Y <= -8f) Projectile.velocity.Y = -8f;
				if(Projectile.velocity.Y >= 8f) Projectile.velocity.Y = 8f;
			}
			Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + .25f * (float)Math.PI;
			for (int i = 0; i < 3; i++)
			{
				int d = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Mod.Find<ModDust>("YamataADust").Type, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
				if (Main.rand.Next(6) != 0)
				{
					Main.dust[d].noGravity = true;
					Main.dust[d].velocity.X *= 2f;
					Main.dust[d].velocity.Y *= 2f;
				}else
				{
					Main.dust[d].noGravity = true;
					Main.dust[d].velocity.X *= 1.2f;
					Main.dust[d].velocity.Y *= 1.2f;
				}
			}
        }

        public override void OnHitNPC (NPC target, NPC.HitInfo hit, int damageDone)
		{
            target.AddBuff(Mod.Find<ModBuff>("Moonraze").Type, 500);
        }		

        public override void OnKill(int timeLeft)
        {
            Projectile.position = Projectile.Center;
            Projectile.width = Projectile.height = 80;
            Projectile.Center = Projectile.position;
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;
            Projectile.Damage();
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
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 200, default, 3.7f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 100, default, 1.5f);
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
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = Projectile.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += Projectile.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = Projectile.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += Projectile.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, new Color(Color.White.R, Color.White.G, Color.White.B, 20 * k), Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}

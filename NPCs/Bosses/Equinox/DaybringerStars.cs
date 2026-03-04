using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Equinox
{
    public class DaybringerStars : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Daybringer Star");
		}

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
			Projectile.extraUpdates = 2;
			Projectile.timeLeft = 1800;
            CooldownSlot = 1;
        }	
        public override void AI()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), .98f, .96f, .67f);
			if(Projectile.localAI[0] ++ == 5)
            {
                SpawnDust();
            }

            if(Projectile.timeLeft <= 0)
            {
                Projectile.Kill();
            }

            Player player = Main.player[(int)Projectile.ai[1]];

            if(Projectile.ai[0] == 0)
            {
                Projectile.Center = player.Center + new Vector2(Projectile.ai[0], -300f);
            }
            else
            {
                Projectile.Center = player.Center + new Vector2(Projectile.ai[0], 200f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SpawnDust();
            if(Main.rand.Next(2) == 0)
            {
                int a = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0f, -12f), Mod.Find<ModProjectile>("DayBringerBlast").Type, Projectile.damage, 3);
                int b = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0f, 12f), Mod.Find<ModProjectile>("DayBringerBlast").Type, Projectile.damage, 3);
                int c = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(-12f, 0), Mod.Find<ModProjectile>("DayBringerBlast").Type, Projectile.damage, 3);
                int d = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(12f, 0), Mod.Find<ModProjectile>("DayBringerBlast").Type, Projectile.damage, 3);
            }
            else
            {
                int a = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(8f, -8f), Mod.Find<ModProjectile>("DayBringerBlast").Type, Projectile.damage, 3);
                int b = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(8f, 8f), Mod.Find<ModProjectile>("DayBringerBlast").Type, Projectile.damage, 3);
                int c = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(-8f, 8f), Mod.Find<ModProjectile>("DayBringerBlast").Type, Projectile.damage, 3);
                int d = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(-8f, -8f), Mod.Find<ModProjectile>("DayBringerBlast").Type, Projectile.damage, 3);
            }
            Projectile.active = false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(250, 244, 171, 200);
        }

        public void SpawnDust()
        {
            Vector2 position = Projectile.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num86].color = new Color(250, 244, 171);
            }
            for (int num87 = 0; num87 < 7; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.Torch, 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                Main.dust[num88].color = new Color(250, 244, 171);
                num88 = Dust.NewDust(position, num84, height3, DustID.Torch, 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = new Color(250, 244, 171);
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * 8f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
		{
			Color color = Lighting.GetColor((int)(Projectile.position.X + Projectile.width * 0.5) / 16, (int)((Projectile.position.Y + Projectile.height * 0.5) / 16.0));
			Vector2 vector = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Rectangle rectangle = Utils.Frame(texture2D, 1, Main.projFrames[Projectile.type], 0, Projectile.frame);
			Color alpha = Projectile.GetAlpha(color);
			Vector2 origin = Utils.Size(rectangle) / 2f;
			float scaleFactor = (float)Math.Cos(6.2831855f * (Projectile.localAI[0] / 60f)) + 3f + 3f;
			for (float num = 0f; num < 2; num += 1f)
			{
				SpriteBatch spriteBatch2 = Main.spriteBatch;
				Texture2D texture = texture2D;
				Vector2 value = vector;
				Vector2 unitY = Vector2.UnitY;
				spriteBatch2.Draw(texture, value + Utils.RotatedBy(unitY, 0, default) * (num == 0? scaleFactor * 2 : scaleFactor), new Rectangle?(rectangle), num == 0? (alpha * 0.4f) : alpha, Projectile.rotation, origin, Projectile.scale * (num == 0? 1.2f : 1), SpriteEffects.None, 0f);
			}
			return false;
		}

    }
}
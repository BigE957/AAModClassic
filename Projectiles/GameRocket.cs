using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class GameRocket : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Game Rocket");
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.scale = 0.9f;
        }

        public override void AI()
        {
            float num1 = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
			float num2 = Projectile.localAI[0];
			if (num2 == 0.0)
			{
				Projectile.localAI[0] = num1;
				num2 = num1;
			}
			float num3 = Projectile.position.X;
			float num4 = Projectile.position.Y;
			float num5 = 250f;
			bool flag2 = false;
			int num6 = 0;
			if (Projectile.ai[1] == 0.0)
			{
				for (int index = 0; index < 200; ++index)
				{
					if (Main.npc[index].CanBeChasedBy(this, false) && (Projectile.ai[1] == 0.0 || Projectile.ai[1] == (double)(index + 1)))
					{
						float num7 = Main.npc[index].position.X + Main.npc[index].width / 2;
						float num8 = Main.npc[index].position.Y + Main.npc[index].height / 2;
						float num9 = Math.Abs(Projectile.position.X + (Projectile.width / 2) - num7) + Math.Abs(Projectile.position.Y + (Projectile.height / 2) - num8);
						if (num9 < num5 && Collision.CanHit(new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2)), 1, 1, Main.npc[index].position, Main.npc[index].width, Main.npc[index].height))
						{
							num5 = num9;
							num3 = num7;
							num4 = num8;
							flag2 = true;
							num6 = index;
						}
					}
				}
				if (flag2)
					Projectile.ai[1] = num6 + 1;
				flag2 = false;
			}
			if (Projectile.ai[1] > 0.0)
			{
				int index = (int)(Projectile.ai[1] - 1.0);
				if (Main.npc[index].active && Main.npc[index].CanBeChasedBy(this, true) && !Main.npc[index].dontTakeDamage)
				{
					if (Math.Abs(Projectile.position.X + (Projectile.width / 2) - (Main.npc[index].position.X + Main.npc[index].width / 2)) + Math.Abs(Projectile.position.Y + (Projectile.height / 2) - (Main.npc[index].position.Y + Main.npc[index].height / 2)) < 1000.0)
					{
						flag2 = true;
						num3 = Main.npc[index].position.X + Main.npc[index].width / 2;
						num4 = Main.npc[index].position.Y + Main.npc[index].height / 2;
					}
				}
				else
					Projectile.ai[1] = 0.0f;
			}
			if (!Projectile.friendly)
				flag2 = false;
			if (flag2)
			{
				float num7 = num2;
				Vector2 vector2 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
				float num8 = num3 - vector2.X;
				float num9 = num4 - vector2.Y;
				float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
				float num11 = num7 / num10;
				float num12 = num8 * num11;
				float num13 = num9 * num11;
				int num14 = 8;
				Projectile.velocity.X = (Projectile.velocity.X * (num14 - 1) + num12) / num14;
				Projectile.velocity.Y = (Projectile.velocity.Y * (num14 - 1) + num13) / num14;
			}
        }

        public override void OnKill(int timeLeft)
        {
            int pieCut = 20;
            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Moo"), Projectile.Center);
            Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("GameBoom").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.InfinityOverloadR>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.InfinityOverloadR>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }
    }
}

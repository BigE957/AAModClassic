using AAModClassic._Content.Mire.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class TimeTeller_Time : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 12;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;  
            Projectile.width = 20;
            Projectile.height = 22;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 150;
        }
        private Color Gold = Color.Goldenrod;
        public bool AM;
        public bool PM;

        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.ai[0] != 0)
            {
                return TimeColor();
            }
            else
            {
                return Gold;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] != 0)
            {
                int Buff;
                if (Main.dayTime)
                {
                    Buff = BuffID.Daybreak;
                }
                else
                {
                    Buff = ModContent.BuffType<Moonraze_Buff>();
                }

                target.AddBuff(Buff, 180);
            }
        }

        public override void AI()
        {
            FindFrame();
            if (Projectile.ai[0] != 0)
            {
                if (Main.dayTime)
                {
                    Lighting.AddLight(Projectile.Center, new Vector3(Color.OrangeRed.R / 255f, Color.OrangeRed.G / 255, Color.OrangeRed.B / 255f));
                }
                else
                {
                    Lighting.AddLight(Projectile.Center, new Vector3(Color.Indigo.R / 255f, Color.Indigo.G / 255, Color.Indigo.B / 255f));
                }
            }
            else
            {
                Lighting.AddLight(Projectile.Center, new Vector3(Gold.R / 255f, Gold.G / 255f, Gold.B / 255f));
            }
            Projectile.velocity *= 0.985f;
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] > 30f)
            {
                Projectile.alpha += 10;
                if (Projectile.alpha >= 255)
                {
                    Projectile.alpha = 255;
                    Projectile.Kill();
                    return;
                }
            }
        }

        public void FindFrame()
        {
            double num4 = (float)Main.time;
            if (!Main.dayTime)
            {
                num4 += 54000.0;
            }
            num4 = num4 / 86400.0 * 24.0;
            double num5 = 7.5;
            num4 = num4 - num5 - 12.0;
            if (num4 < 0.0)
            {
                num4 += 24.0;
            }
            if (num4 > 1)
            {
                Projectile.frame = 0;
            }
            else if (num4 > 2)
            {
                Projectile.frame = 1;
            }
            else if (num4 > 3)
            {
                Projectile.frame = 2;
            }
            else if (num4 > 4)
            {
                Projectile.frame = 3;
            }
            else if (num4 > 5)
            {
                Projectile.frame = 4;
            }
            else if (num4 > 6)
            {
                Projectile.frame = 5;
            }
            else if (num4 > 7)
            {
                Projectile.frame = 6;
            }
            else if (num4 > 8)
            {
                Projectile.frame = 7;
            }
            else if (num4 > 9)
            {
                Projectile.frame = 8;
            }
            else if (num4 > 10)
            {
                Projectile.frame = 9;
            }
            else if (num4 > 11)
            {
                Projectile.frame = 10;
            }
            else if (num4 > 12)
            {
                Projectile.frame = 11;
            }
            else if (num4 > 13)
            {
                Projectile.frame = 0;
            }
            else if (num4 > 14)
            {
                Projectile.frame = 1;
            }
            else if (num4 > 15)
            {
                Projectile.frame = 2;
            }
            else if (num4 > 16)
            {
                Projectile.frame = 3;
            }
            else if (num4 > 17)
            {
                Projectile.frame = 4;
            }
            else if (num4 > 18)
            {
                Projectile.frame = 5;
            }
            else if (num4 > 19)
            {
                Projectile.frame = 6;
            }
            else if (num4 > 20)
            {
                Projectile.frame = 7;
            }
            else if (num4 > 21)
            {
                Projectile.frame = 8;
            }
            else if (num4 > 22)
            {
                Projectile.frame = 9;
            }
            else if (num4 > 23)
            {
                Projectile.frame = 10;
            }
            else
            {
                Projectile.frame = 11;
            }
        }

        public static Color TimeColor()
        {
            double num4 = (float)Main.time;
            if (!Main.dayTime)
            {
                num4 += 54000.0;
            }
            num4 = num4 / 86400.0 * 24.0;
            double num5 = 7.5;
            num4 = num4 - num5 - 12.0;
            if (num4< 0.0)
            {
                num4 += 24.0;
            }
            if (num4 >= 12.0)
            {
                return Color.Indigo;
            }
            else
            {
                return Color.OrangeRed;
            }
        }

    }
}

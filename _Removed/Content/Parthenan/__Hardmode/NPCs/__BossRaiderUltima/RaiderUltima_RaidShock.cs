using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRaiderUltima
{
    public class RaiderUltima_RaidShock : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.scale = .1f;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            if (Projectile.frameCounter++ > 7)
            {
                Projectile.frameCounter = 0;
                Projectile.frame += 1;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0]++ < 300)
            {
                if (Projectile.scale < 1)
                {
                    Projectile.scale += .05f;
                }
                if (Projectile.alpha > 0)
                {
                    Projectile.alpha -= 10;
                }
            }
            else
            {
                if (Projectile.scale > 0)
                {
                    Projectile.scale -= .1f;
                }
                else
                {
                    Projectile.active = false;
                }
                if (Projectile.alpha < 255)
                {
                    Projectile.alpha += 5;
                }
            }

        }
        

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item94, Projectile.position);
            int num290 = Main.rand.Next(3, 7);
            for (int num291 = 0; num291 < num290; num291++)
            {
                int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default, 2.1f);
                Main.dust[num292].velocity *= 2f;
                Main.dust[num292].noGravity = true;
            }
            if (Main.myPlayer == Projectile.owner)
            {
                Rectangle value19 = new Rectangle((int)Projectile.Center.X - 40, (int)Projectile.Center.Y - 40, 80, 80);
                for (int num293 = 0; num293 < 1000; num293++)
                {
                    if (num293 != Projectile.whoAmI && Main.projectile[num293].active && Main.projectile[num293].owner == Projectile.owner && Main.projectile[num293].type == 443 && Main.projectile[num293].getRect().Intersects(value19))
                    {
                        Main.projectile[num293].ai[1] = 1f;
                        Main.projectile[num293].velocity = (Projectile.Center - Main.projectile[num293].Center) / 5f;
                        Main.projectile[num293].netUpdate = true;
                    }
                }
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 443, Projectile.damage, 0f, Projectile.owner, 0f, 0f);
            }
        }

        

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 10)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content._Dev.__Hardmode.Items.Weapons
{
    public class AleisterStaff_InvokedHeal : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 3;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.DarkRed;
        }
        public override void AI()
        {
            int num492 = (int)Projectile.ai[0];
            float num493 = 4f;
            Vector2 vector39 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            float num494 = Main.player[num492].Center.X - vector39.X;
            float num495 = Main.player[num492].Center.Y - vector39.Y;
            float num496 = (float)Math.Sqrt(num494 * num494 + num495 * num495);
            if (num496 < 50f && Projectile.position.X < Main.player[num492].position.X + Main.player[num492].width && Projectile.position.X + Projectile.width > Main.player[num492].position.X && Projectile.position.Y < Main.player[num492].position.Y + Main.player[num492].height && Projectile.position.Y + Projectile.height > Main.player[num492].position.Y)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    int num497 = (int)Projectile.ai[1];
                    Main.player[num492].HealEffect(num497, true);
                    Player player = Main.player[num492];
                    player.statLife += num497;
                    if (Main.player[num492].statLife > Main.player[num492].statLifeMax2)
                    {
                        Main.player[num492].statLife = Main.player[num492].statLifeMax2;
                    }
                    NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, num492, num497, 0f, 0f, 0, 0, 0);
                }
                Projectile.Kill();
            }
            num496 = num493 / num496;
            num494 *= num496;
            num495 *= num496;
            Projectile.velocity.X = (Projectile.velocity.X * 15f + num494) / 16f;
            Projectile.velocity.Y = (Projectile.velocity.Y * 15f + num495) / 16f;
            for (int num502 = 0; num502 < 5; num502++)
            {
                float num503 = Projectile.velocity.X * 0.2f * num502;
                float num504 = -(Projectile.velocity.Y * 0.2f) * num502;
                int num505 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.SpectreStaff, 0f, 0f, 20, Color.OrangeRed, 1.3f);
                Main.dust[num505].noGravity = true;
                Main.dust[num505].velocity *= 0f;
                Main.dust[num505].position.X = Main.dust[num505].position.X - num503;
                Main.dust[num505].position.Y = Main.dust[num505].position.Y - num504;
            }
            return;
        }
    }
}
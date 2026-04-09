using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Anubis.Forsaken
{
    public class Crook : ModProjectile
	{
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 3600;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        int HealAmt = 0;

		public override void AI()
		{
            Player player = Main.player[Projectile.owner];
            for (int a = 0; a < HealAmt; a++)
            {
                int dustnumber = Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 100, default, 0);
                Main.dust[dustnumber].velocity *= 0.3f;
                Main.dust[dustnumber].noGravity = true; ;
            }

            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;

            Vector2 vector36 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            float num489 = player.Center.X - vector36.X;
            float num490 = player.Center.Y - vector36.Y;
            float num491 = (float)Math.Sqrt(num489 * num489 + num490 * num490);

            if (player.position == default) { player.position = Main.player[Projectile.owner].position; }
            if (player.width == -1) { player.width = Main.player[Projectile.owner].width; }
            if (player.height == -1) { player.height = Main.player[Projectile.owner].height; }
            Vector2 center = player.position + new Vector2(player.width * 0.5f, player.height * 0.5f);
            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = 8;
                SoundEngine.PlaySound(SoundID.Item7, Projectile.position);
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] >= 45)
                {
                    Projectile.ai[0] = 1f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
            }
            else
            {
                Projectile.tileCollide = false;
                float distPlayerX = center.X - Projectile.Center.X;
                float distPlayerY = center.Y - Projectile.Center.Y;
                float distPlayer = (float)Math.Sqrt(distPlayerX * distPlayerX + distPlayerY * distPlayerY);
                if (distPlayer > 3000f)
                {
                    Projectile.Kill();
                }

                distPlayer = 40 / distPlayer;
                distPlayerX *= distPlayer;
                distPlayerY *= distPlayer;
                if (Projectile.velocity.X < distPlayerX)
                {
                    Projectile.velocity.X += 10;
                    if (Projectile.velocity.X < 0f && distPlayerX > 0f) { Projectile.velocity.X += 10; }
                }
                else
                if (Projectile.velocity.X > distPlayerX)
                {
                    Projectile.velocity.X -= 10;
                    if (Projectile.velocity.X > 0f && distPlayerX < 0f) { Projectile.velocity.X -= 10; }
                }
                if (Projectile.velocity.Y < distPlayerY)
                {
                    Projectile.velocity.Y += 10;
                    if (Projectile.velocity.Y < 0f && distPlayerY > 0f) { Projectile.velocity.Y += 10; }
                }
                else
                if (Projectile.velocity.Y > distPlayerY)
                {
                    Projectile.velocity.Y -= 10;
                    if (Projectile.velocity.Y > 0f && distPlayerY < 0f) { Projectile.velocity.Y -= 10; }
                }
                if (Main.myPlayer == Projectile.owner)
                {
                    Rectangle rectangle = Projectile.Hitbox;
                    Rectangle value = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
                    if (rectangle.Intersects(value))
                    {
                        if (Projectile.owner == Main.myPlayer)
                        {
                            player.HealEffect(HealAmt, false);
                            player.statLife += 1;
                            if (player.statLife > player.statLifeMax2)
                            {
                                player.statLife = player.statLifeMax2;
                            }
                            NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, Projectile.owner, 1, 0f, 0f, 0, 0, 0);
                        }
                        Projectile.Kill(); 
                    }
                }
            }
            Projectile.rotation += .6f * Projectile.direction;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            HealAmt++;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 0);
            BaseDrawing.DrawAfterimage(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, 2f, 1f, 5, true, 0f, 0f, lightColor);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }
    }
}
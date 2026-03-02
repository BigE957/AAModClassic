using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class DaystormP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Daystorm");

            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = ProjAIStyleID.HeldProjectile;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.ai[0] += 1f;
            int num2 = 0;
            if (Projectile.ai[0] >= 40f)
            {
                num2++;
            }
            if (Projectile.ai[0] >= 80f)
            {
                num2++;
            }
            if (Projectile.ai[0] >= 120f)
            {
                num2++;
            }
            int num3 = 24;
            int num4 = 6;
            Projectile.ai[1] += 1f;
            bool flag = false;
            if (Projectile.ai[1] >= num3 - num4 * num2)
            {
                Projectile.ai[1] = 0f;
                flag = true;
            }
            Projectile.frameCounter += 1 + num2;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= 6)
                {
                    Projectile.frame = 0;
                }
            }
            if (Projectile.soundDelay <= 0)
            {
                Projectile.soundDelay = num3 - num4 * num2;
                if (Projectile.ai[0] != 1f)
                {
                    SoundEngine.PlaySound(SoundID.Item91, Projectile.position);
                }
            }
            if (Projectile.ai[1] == 1f && Projectile.ai[0] != 1f)
            {
                Vector2 vector2 = Vector2.UnitX * 24f;
                vector2 = vector2.RotatedBy(Projectile.rotation - 1.57079637f, default);
                Vector2 value = Projectile.Center + vector2;
                for (int i = 0; i < 2; i++)
                {
                    int num5 = Dust.NewDust(value - Vector2.One * 8f, 16, 16, DustID.IceTorch, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f, 100);
                    Main.dust[num5].velocity *= 0.66f;
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].scale = 1.4f;
                }
            }
            if (flag && Main.myPlayer == Projectile.owner)
            {
                bool flag2 = player.channel && player.CheckMana(player.inventory[player.selectedItem].mana, true, false) && !player.noItems && !player.CCed;
                if (flag2)
                {
                }
                else
                {
                    Projectile.Kill();
                }
            }
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D14 = Mod.GetTexture("Projectiles/Akuma/Daystorm");
            Texture2D Glow = Mod.GetTexture("Glowmasks/DaystormP_Glow");
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Color color25 = Lighting.GetColor((int)(Projectile.position.X + Projectile.width * 0.5) / 16, (int)((Projectile.position.Y + Projectile.height * 0.5) / 16.0));
            int num215 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y7 = num215 * Projectile.frame;
            Vector2 vector27 = (Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            float scale5 = 1f;
            if (Main.player[Projectile.owner].shroomiteStealth && Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].CountsAsClass(DamageClass.Ranged))
            {
                float num216 = Main.player[Projectile.owner].stealth;
                if (num216 < 0.03)
                {
                    num216 = 0.03f;
                }
                float arg_97B3_0 = (1f + num216 * 10f) / 11f;
                color25 *= num216;
                scale5 = num216;
            }
            if (Main.player[Projectile.owner].setVortex && Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].CountsAsClass(DamageClass.Ranged))
            {
                float num217 = Main.player[Projectile.owner].stealth;
                if (num217 < 0.03)
                {
                    num217 = 0.03f;
                }
                float arg_9854_0 = (1f + num217 * 10f) / 11f;
                color25 = color25.MultiplyRGBA(new Color(Vector4.Lerp(Vector4.One, new Vector4(0f, 0.12f, 0.16f, 0f), 1f - num217)));
                scale5 = num217;
            }
            Main.spriteBatch.Draw(texture2D14, vector27, new Rectangle?(new Rectangle(0, y7, texture2D14.Width, num215)), Projectile.GetAlpha(color25), Projectile.rotation, new Vector2(texture2D14.Width / 2f, num215 / 2f), Projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(Glow, vector27, new Rectangle?(new Rectangle(0, y7, texture2D14.Width, num215)), AAColor.Glow * scale5, Projectile.rotation, new Vector2(texture2D14.Width / 2f, num215 / 2f), Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}

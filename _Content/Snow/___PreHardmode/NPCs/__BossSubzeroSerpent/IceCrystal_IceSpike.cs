using AAModClassic.Backgrounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent
{
    public class IceCrystal_IceSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Spike");
            Main.projFrames[Projectile.type] = 30;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.tileCollide = true;
            Projectile.coldDamage = true;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            if (Projectile.alpha > 50)
            {
                Projectile.alpha -= 10;
            }

            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 50) { Projectile.velocity.Y += 1; }
            if (Projectile.velocity.Y > 16) { Projectile.velocity.Y = 16; }

            if (Projectile.frameCounter != 1)
            {
                Projectile.frameCounter = 1;
                Projectile.frame = Main.rand.Next(5);
                Main.NewText(Projectile.frame);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle newFrame = new Rectangle();
            newFrame.Width = TextureAssets.Projectile[Projectile.type].Width() / 6;
            newFrame.Height = TextureAssets.Projectile[Projectile.type].Height() / 5;
            newFrame.X = (int)Projectile.ai[1] * (TextureAssets.Projectile[Projectile.type].Width() / 6);
            newFrame.Y = Projectile.frame * (TextureAssets.Projectile[Projectile.type].Height() / 5);

            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, newFrame, Projectile.GetAlpha(lightColor), Projectile.rotation, newFrame.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
            for (int i = 0; i < 8; i++)
            {
                int dustID = Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<Dusts.SnowDust>(), Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 100, Color.White, 0.8f);
            }
        }
    }
}

using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened
{
    public class ShenDeathBoom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discordian Strike");     
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.penetrate = -1;
            Projectile.damage = 0;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            Projectile.alpha = 80;
        }

        bool draw = true;
        public override void AI()
        {
            if (!draw)
            {
                draw = true;
            }
            else
            {
                draw = false;
            }
            
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 7)
                {
                    Projectile.Kill();

                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y *= 0.00f;

        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (!draw)
            {
                return false;
            }
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 7, 0, 2);

            Texture2D Tex = TextureAssets.Projectile[Projectile.type].Value;
            if (Projectile.ai[0] == 1)
            {
                Tex = ModContent.Request<Texture2D>("AAModClassic/_Content/Chaos/_PostMoonlord/NPCs/_BossShen/ShenDeathBoomR").Value;
            }
            else
            if (Projectile.ai[0] == 1)
            {
                Tex = ModContent.Request<Texture2D>("AAModClassic/_Content/Chaos/_PostMoonlord/NPCs/_BossShen/ShenDeathBoomB").Value;
            }
            BaseDrawing.DrawTexture(Main.spriteBatch, Tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 7, frame, Projectile.GetAlpha(Color.White), true);
            return false;
        }
    }
}

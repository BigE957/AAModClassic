using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus
{
    public class FeudalFungusLeave: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Feudal Fungus");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.damage = 24;
            Projectile.width = 74;
            Projectile.height = 80;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 900;
            Projectile.alpha = 0;
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= 14)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.velocity *= 0;
            Projectile.alpha += 4;
            if (Projectile.alpha >= 255)
            {
                Projectile.active = false;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D glowTex = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/FeudalFungusIGoNow_Glow").Value;

            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 4, 0, 0);

            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, Projectile.GetAlpha(lightColor), true);
            BaseDrawing.DrawTexture(Main.spriteBatch, glowTex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, Projectile.GetAlpha(AAColor.Glow), true);
            return false;
        }
    }
}
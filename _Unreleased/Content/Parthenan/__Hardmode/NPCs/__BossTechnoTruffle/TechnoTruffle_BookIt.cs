using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Parthenan.__Hardmode.NPCs.__BossTechnoTruffle
{
    public class TechnoTruffle_BookIt : ModProjectile
    {
        public static Asset<Texture2D> Glowmask1;
        public static Asset<Texture2D> Glowmask2;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Techno Truffle");
            Main.projFrames[Projectile.type] = 4;

            Glowmask1 = ModContent.Request<Texture2D>(Texture + "_Glow1");
            Glowmask2 = ModContent.Request<Texture2D>(Texture + "_Glow2");
        }
        public override void SetDefaults()
        {
            Projectile.damage = 24;
            Projectile.width = 66;
            Projectile.height = 104;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 900;
        }
        public override void AI()
        {
            Color color = BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, BaseDrawing.GetLightColor(Projectile.position), BaseDrawing.GetLightColor(Projectile.position), Color.Violet, BaseDrawing.GetLightColor(Projectile.position), Color.Violet, BaseDrawing.GetLightColor(Projectile.position));

            Lighting.AddLight((int)(Projectile.Center.X + Projectile.width / 2) / 16, (int)(Projectile.position.Y + Projectile.height / 2) / 16, color.R / 255, color.G / 255, color.B / 255);
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y -= .1f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D glowTex = Glowmask1.Value;
            Texture2D glowTex1 = Glowmask2.Value;
            
            Color color = BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, BaseDrawing.GetLightColor(Projectile.position), BaseDrawing.GetLightColor(Projectile.position), Color.Violet, BaseDrawing.GetLightColor(Projectile.position), Color.Violet, BaseDrawing.GetLightColor(Projectile.position));
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, 66, 98);

            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, lightColor, true);
            BaseDrawing.DrawTexture(Main.spriteBatch, glowTex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, color, true);
            BaseDrawing.DrawTexture(Main.spriteBatch, glowTex1, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, Color.White, true);
            return false;
        }
    }
}
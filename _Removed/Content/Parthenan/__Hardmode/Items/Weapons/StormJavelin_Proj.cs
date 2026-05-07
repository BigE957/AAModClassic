using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Weapons
{
    public class StormJavelin_Proj : ModProjectile
    {
        public short customGlowMask = 0;

        public override void SetStaticDefaults()
        {
            //TODO
            /*
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[TextureAssets.GlowMask.Value.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Value.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i].Value;
                }
                glowMasks[glowMasks.Length - 1] = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + GetType().Name + "_Glow").Value;
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask.Value = glowMasks;
            }
            Projectile.glowMask = customGlowMask;
            */
            // DisplayName.SetDefault("Storm Javelin");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;      //this is how many enemy this projectile penetrate before desapear
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.BoneJavelin;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 2)
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 120f)       //how much time the projectile can travel before landing
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;    // projectile fall velocity
                Projectile.velocity.X = Projectile.velocity.X * 0.99f;    // projectile velocity
            }
        }
    }
}
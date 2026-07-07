using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Weapons
{
    public class StormJavelin_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Javelin");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
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
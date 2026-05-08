using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Weapons
{
    public class Bubbleshot_FuckingBubbles : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("FUCKING BUBBLES");
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BulletHighVelocity);
            AIType = ProjectileID.BulletHighVelocity;
        }
        public override void AI()
        {
            Dust dust1;
            Vector2 position = Projectile.position;
            dust1 = Main.dust[Dust.NewDust(position, 0, 0, ModContent.DustType<Dusts.MireBubbleDust>(), 4f, 0f, 46, default, 1f)];
            dust1.noGravity = true;
        }
    }
}
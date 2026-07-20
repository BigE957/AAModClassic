using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.__Hardmode.Items.Weapons
{
    public class ScorchedSaw_Proj : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<ScorchedSaw>().Texture;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LightDisc);
            Projectile.penetrate = 1;  
            Projectile.width = 32;
            Projectile.height = 32;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scorched Saw");
        }

        public override void AI()
        {
            Player p = Main.player[Projectile.owner];
            BaseAI.AIBoomerang(Projectile, ref Projectile.ai, p.position, p.width, p.height, true, 16f, 30, 1f, 2f, false);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Width(), TextureAssets.Projectile[Projectile.type].Height(), 0, 2);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }
    }
}

using Microsoft.Xna.Framework;
using Terraria;

using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.Projectiles
{
    public class TerraRoseShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;  
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 900;
            Projectile.DamageType = DamageClass.Magic;
        }
		
		public override void AI()
		{
			if (Main.rand.NextFloat() < 0.5f)
			{
				Vector2 position = Projectile.position;
                int dustId = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height + 5, DustID.Terra, Projectile.velocity.X * 0.2f,
                Projectile.velocity.Y * 0.2f, 100);
                Main.dust[dustId].noGravity = true;
			}
		}

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("TerraPetal");
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.COLOR_WHITEFADE1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, AAColor.COLOR_WHITEFADE1, true);
            return false;
        }
    }
}

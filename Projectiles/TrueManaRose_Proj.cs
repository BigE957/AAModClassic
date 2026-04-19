using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;


namespace AAModClassic.Projectiles
{
    public class TrueManaRose_Proj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("True Mana Rose");
			Main.projFrames[Projectile.type] = 2;
		}	

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 320;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.damage = 1;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Magic;
        }

		public override void AI()
		{
			BaseAI.AIVilethorn(Projectile, 50, 6, 15);
			if (Projectile.ai[1] == 15)
			{
				Projectile.frame = 0;
			}
			else
			{
				Projectile.frame = 1;
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, 34, 34, 0, 0);
			BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 2, frame, Projectile.GetAlpha(Color.White), true);
			return false;
		}
	}
}
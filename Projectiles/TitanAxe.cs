
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    class TitanAxe : ModProjectile
	{
		public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 2;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Titan Axe");
		}

        public override void AI()
        {
            Player p = Main.player[Projectile.owner];
            BaseAI.AIBoomerang(Projectile, ref Projectile.ai, p.position, p.width, p.height, true, 16f, 20, Projectile.ai[0] == 0 ? 0.7f : 1.3f, .4f, false);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Projectile.ai[0] = 1f;
            Projectile.velocity.X = -oldVelocity.X;
            Projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }
	}
}

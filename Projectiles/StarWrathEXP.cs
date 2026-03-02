using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class StarWrathEXP : ModProjectile
    {
        public override void SetDefaults()
        {
			Projectile.CloneDefaults(503);
			Projectile.aiStyle = ProjAIStyleID.FallingStar;
			AIType = ProjectileID.StarWrath;
			Projectile.tileCollide = false;
			Projectile.localNPCHitCooldown = -1;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Star Wrath EX");
        }
		
		public override void AI()
		{
			Projectile.tileCollide = false;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.type = ProjectileID.Bullet;
			return false;
		}
		
		public override bool PreKill(int timeLeft)
		{
			Projectile.type = ProjectileID.StarWrath;
			return true;
		}
        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, Color.White, true);
            return false;
        }
    }
}

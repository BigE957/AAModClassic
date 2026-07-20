using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class CosmicFury_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cosmic Fury");
        }

        public override void SetDefaults()
        {
			Projectile.CloneDefaults(503);
			Projectile.aiStyle = ProjAIStyleID.FallingStar;
			AIType = ProjectileID.StarWrath;
			Projectile.tileCollide = false;
			Projectile.localNPCHitCooldown = -1;
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
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Width(), TextureAssets.Projectile[Projectile.type].Height(), 0, 2);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, Color.White, true);
            return false;
        }
    }
}

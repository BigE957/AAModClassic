using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Djinn
{
    public class Djinnerang : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("yBoomerangP");
        }

        public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.timeLeft = 550;
			Projectile.extraUpdates = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player p = Main.player[Projectile.owner];
            BaseAI.AIBoomerang(Projectile, ref Projectile.ai, p.position, p.width, p.height, true, 10f, 50, 0.5f, 0.25f, false);
        }

        public override bool OnTileCollide(Vector2 velocityChange)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
            BaseAI.TileCollideBoomerang(Projectile, ref velocityChange, true);
            return false;
        }
    }
}

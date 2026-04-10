using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Weapons
{
    public class AbyssalArc_Proj : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            //TODO: ??????
            // DisplayName.SetDefault("AntimonBoomerangP");
        }

        public override void SetDefaults()
		{

			Projectile.width = 18;
			Projectile.height = 40;
			Projectile.aiStyle = ProjAIStyleID.Boomerang;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 5;
			Projectile.timeLeft = 600;
			Projectile.light = 0.9f;
			Projectile.extraUpdates = 1;
			
			
		}

        public override void AI()
        {
            Player p = Main.player[Projectile.owner];
            BaseAI.AIBoomerang(Projectile, ref Projectile.ai, p.position, p.width, p.height, true, 10f, 50, 1f, 0.75f, false);
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

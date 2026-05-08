using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Weapons
{
    public class Musharang_Proj : ModProjectile
	{

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 3600;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true;
            Projectile.damage = 1;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Ranged;
        }

		public override void AI()
		{
            Player p = Main.player[Projectile.owner];
			Projectile.tileCollide = !Main.expertMode;
			BaseAI.AIBoomerang(Projectile, ref Projectile.ai, p.position, p.width, p.height, true, 15f, 35, .6f, 0.4f);
		}

		public override bool OnTileCollide(Vector2 value2)
		{
			if (Main.netMode != NetmodeID.Server)
			{
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			}
			BaseAI.TileCollideBoomerang(Projectile, ref value2, true);
			return false;
		}
	}
}
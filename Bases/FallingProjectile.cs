using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod
{
    abstract class FallingProjectile : ModProjectile
    {
        public abstract string name { get; }
        public abstract int Tile { get; }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault(name);
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.damage = 0;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;
            Projectile.tileCollide = true;
            Projectile.aiStyle = ProjAIStyleID.FallingTile;
            AIType = ProjectileID.GoldCoinsFalling;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            if (Tile != -1)
            {
                WorldGen.PlaceTile((int)(Projectile.position.X / 16), (int)(Projectile.position.Y / 16), Tile);
            }
        }
    }
}

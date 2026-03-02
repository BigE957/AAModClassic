using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public class EnderMinion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.CloneDefaults(533); // ID for Deadly Sphere proj
            AIType = ProjectileID.DeadlySphere;
            Projectile.width = 62;
            Projectile.height = 62;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ender Minion");
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = oldVelocity.Y;
            }
            return false;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = (AAPlayer)player.GetModPlayer(Mod, "AAPlayer");
            if (player.dead)
            {
                modPlayer.enderMinion = false;
            }
            if (modPlayer.enderMinion)
            {
                Projectile.timeLeft = 2;
            }
            return true;
        }
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class ConflagrateScythe_ConflagrateConstructEX : ModProjectile
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
            Projectile.extraUpdates = 1;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ender Minion EX");
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

        }

        /*public override void AI()         // If you want dust to spawn from it
        {
            if (Main.rand.Next(1) == 0)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 1); //1 is where the dust id should go
            }
        }*/
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
            ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
            if (player.dead)
            {
                modPlayer.enderMinionEX = false;
            }
            if (modPlayer.enderMinionEX)
            {
                Projectile.timeLeft = 2;
            }
            return true;
        }
    }
}
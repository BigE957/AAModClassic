using AAModClassic.Base.BaseMod.Base;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Serpent
{
    public class IceSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Spike");
            Main.projFrames[Projectile.type] = 30;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.tileCollide = false;
            Projectile.coldDamage = true;
            Projectile.hostile = true;
            Projectile.friendly = false;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 50) { Projectile.velocity.Y += 1; }
            if (Projectile.velocity.Y > 16) { Projectile.velocity.Y = 16; }

            if (Projectile.frameCounter != 1)
            {
                Projectile.frameCounter = 1;
                Projectile.frame = Main.rand.Next(5) * (int)Projectile.ai[1];
            }
        }
    }
}

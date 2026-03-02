using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    public class RoyalKitten : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Royal Kitten");
            Main.projFrames[Projectile.type] = 11;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BlackCat);
            AIType = ProjectileID.BlackCat;
            Projectile.width = 36;
            Projectile.height = 38;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.blackCat = false;
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.dead)
            {
                modPlayer.RoyalKitten = false;
            }
            if (modPlayer.RoyalKitten)
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}



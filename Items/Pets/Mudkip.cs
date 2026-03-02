using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    public class Mudkip : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mudkip");
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
                modPlayer.Mudkip = false;
            }
            if (modPlayer.Mudkip)
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}



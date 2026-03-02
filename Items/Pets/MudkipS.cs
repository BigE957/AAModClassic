using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    public class MudkipS : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shiny Mudkip"); // Automatic from .lang files
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
            player.blackCat = false; // Relic from aiType
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.dead)
            {
                modPlayer.MudkipS = false;
            }
            if (modPlayer.MudkipS)
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}



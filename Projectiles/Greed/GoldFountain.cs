using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Greed
{
    public class GoldFountain : ModProjectile
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Greed/GreedSpawn";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gold Fountain");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                }
            }

            if (Projectile.timeLeft < 60)
            {
                Projectile.alpha += 5;
            }
            else
            {
                Projectile.alpha -= 5;
            }

            int FountainCount = AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<GoldFountain>());
            if (FountainCount < 1) FountainCount = 1;
            if (Main.netMode != NetmodeID.MultiplayerClient && Projectile.ai[0]++ >= 5 * FountainCount)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + 30f, Projectile.position.Y + 30f, Main.rand.Next(-3, 4), Main.rand.Next(-3, 10), ModContent.ProjectileType<Gold>(), Projectile.damage, 1, Projectile.owner, 0, 0);
                Projectile.ai[0] = 0;
                Projectile.netUpdate = true;
            }

            Player player = Main.player[Projectile.owner];
            if(player.inventory[player.selectedItem].type == ModContent.ItemType<GoldDigger>() && player.altFunctionUse == 2 && player.controlUseItem)
            {
                Projectile.Kill();
            }
        }
    }
}
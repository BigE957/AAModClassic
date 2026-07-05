using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Armor
{
    public class FulguriteHelmetSetPlayer_FulguriteRing : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 102;
            Projectile.height = 102;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.timeLeft = 180;
            Projectile.knockBack = 10;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.GetModPlayer<FulguriteHelmetSetPlayer>().FulguriteRingActive = true;
            Projectile.Center = player.Center;
            Projectile.direction = player.direction;
        }
        

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            SoundEngine.PlaySound(SoundID.Item94, Projectile.position);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
            return true;
        }
    }
}
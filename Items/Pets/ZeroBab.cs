using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class ZeroBab : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.LightPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BabySkeletronHead);
            AIType = ProjectileID.BabySkeletronHead;
            Projectile.width = 62;
            Projectile.height = 62;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.skeletron = false;
            return true;
        }

        public override void AI()
        {
            Lighting.AddLight((int)(Projectile.Center.X + Projectile.width / 2) / 16, (int)(Projectile.position.Y + Projectile.height / 2) / 16, 1f, 0.2f, 0.1f);
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.dead)
            {
                modPlayer.ZeroBab = false;
            }
            if (!modPlayer.ZeroBab)
            {
                Projectile.active = false;
            }

            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 4)
                {
                    Projectile.frame = 0;
                }
            }
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 5, 0, 0);

            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 5, frame, lightColor, true);
            BaseDrawing.DrawTexture(spriteBatch, Mod.GetTexture("Glowmasks/ZeroBab_Glow"), 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 5, frame, AAColor.COLOR_WHITEFADE1, true);
            return false;
        }
    }
}



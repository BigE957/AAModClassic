using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Pets
{
    public class CerberusWhistle_Cerberus : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 11;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Puppy);
            AIType = ProjectileID.Puppy;
            Projectile.width = 28;
            Projectile.height = 28;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.puppy = false;
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
            if (player.dead)
            {
                modPlayer.Cerberus = false;
            }
            if (!modPlayer.Cerberus)
            {
                Projectile.active = false;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float num149 = (TextureAssets.Projectile[Projectile.type].Width() - Projectile.width) * 0.5f + Projectile.width * 0.5f;

            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            int y15 = 40 * Projectile.frame;

            Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, new Vector2(Projectile.position.X - Main.screenPosition.X + num149 + -18, Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2 + Projectile.gfxOffY), new Rectangle?(new Rectangle(0, y15, TextureAssets.Projectile[Projectile.type].Width(), 40 - 1)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2(num149, Projectile.height / 2 + 8), Projectile.scale, spriteEffects, 0f);

            return false;
        }
    }
}



using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class Pine : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pine");
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 20;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void PostAI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
        }

        public override void OnKill(int timeleft)
        {
            for (int i = 0; i < Main.rand.Next(5, 10); i++)
            {
                int x = Main.rand.Next(-6, 6);
                int y = -Main.rand.Next(3, 5);
                int p = Projectile.NewProjectile(Projectile.position, new Vector2(x, y), ProjectileID.PineNeedleFriendly, Projectile.damage, Projectile.knockBack, Main.myPlayer);
                Main.projectile[p].Center = Projectile.Center - new Vector2(0, 14);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, 1f, 1f, 5, false, 0f, 0f, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, lightColor, false);
            return false;
        }
    }
}

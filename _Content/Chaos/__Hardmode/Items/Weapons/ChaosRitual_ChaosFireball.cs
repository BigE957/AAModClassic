using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Weapons
{
    public class ChaosRitual_ChaosFireball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Fireball");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.minion = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 120;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            int d = Projectile.ai[1] == 0 ? ModContent.DustType<Dusts.DragonflameDust>() : ModContent.DustType<Dusts.HydratoxinDust>();
            for (int num572 = 0; num572 < 5; num572++)
            {
                float num573 = Projectile.velocity.X * 0.2f * num572;
                float num574 = -(Projectile.velocity.Y * 0.2f) * num572;
                int num575 = Dust.NewDust(Vector2.Zero, Projectile.width, Projectile.height, d, 0f, 0f, 100, default, 1f);
                Main.dust[num575].noGravity = true;
                Main.dust[num575].velocity *= 0f;
                Dust expr_178B4_cp_0 = Main.dust[num575];
                expr_178B4_cp_0.position.X -= num573;
                Dust expr_178D3_cp_0 = Main.dust[num575];
                expr_178D3_cp_0.position.Y -= num574;
            }

            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            int d = Projectile.ai[1] == 0 ? ModContent.DustType<Dusts.DragonflameDust>() : ModContent.DustType<Dusts.HydratoxinDust>();
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, 1, d, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2.5f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            int shader = Projectile.ai[1] == 0 ? Terraria.Graphics.Shaders.GameShaders.Armor.GetShaderIdFromItemId(Terraria.ID.ItemID.LivingFlameDye) : Terraria.Graphics.Shaders.GameShaders.Armor.GetShaderIdFromItemId(Terraria.ID.ItemID.LivingOceanDye);

            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 4, 0);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, shader, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, Color.White, true);
            return false;
        }
    }
}

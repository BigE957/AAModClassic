using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.NPCs.Bosses.Akuma
{
    public class AkumaBoom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dayfire");     
            Main.projFrames[Projectile.type] = 7;     
        }

        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 6)
                {
                    Projectile.Kill();

                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y *= 0.00f;

        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonFire>(), 200);
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }


        public override bool PreDraw(ref Color lightColor)
        {
            int shader = Terraria.Graphics.Shaders.GameShaders.Armor.GetShaderIdFromItemId(Terraria.ID.ItemID.LivingFlameDye);
            Vector2 Drawpos = Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY);
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 7, 0, 2);

            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, shader, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 7, frame, Color.White, true);
            return false;
        }
    }
}

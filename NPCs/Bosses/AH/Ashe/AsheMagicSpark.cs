using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.AH.Ashe
{
    internal class AsheMagicSpark : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fire Bomb");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {

            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.scale = 1.1f;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            Projectile.velocity *= 1f + Math.Abs(Projectile.ai[0]);

            Vector2 acceleration = Projectile.velocity.RotatedBy(Math.PI / 2);
            acceleration *= Projectile.ai[1];
            Projectile.velocity += acceleration;

            if (Projectile.timeLeft < 60)
            {
                Projectile.alpha+= 5;
            }
            if (Projectile.timeLeft == 0)
            {
                Projectile.Kill();
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            if (Projectile.frameCounter++ > 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }


        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonFire_Buff>(), 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 4, 0, 0);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 4, frame, Projectile.GetAlpha(Color.White), true);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item124);
        }
    }
}
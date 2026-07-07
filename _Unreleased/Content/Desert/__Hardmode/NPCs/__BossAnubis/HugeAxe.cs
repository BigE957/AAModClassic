using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Desert.__Hardmode.NPCs.__BossAnubis
{
    public class HugeAxe : ModProjectile
    {
        public ref float BounceCount => ref Projectile.ai[0];
        public ref float a2 => ref Projectile.ai[1];
        public ref float a3 => ref Projectile.ai[2];
        public ref float a4 => ref Projectile.ai[3];

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Axe");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (Projectile.velocity.X < 0)
                Projectile.direction = -1;
            else
                Projectile.direction = 1;

            Projectile.velocity.Y += 0.2f;
            // i moved the values around until the hammer always looked upright at its apex. if theres a better way to do that, do it 
            Projectile.rotation += .05f * ((Math.Abs(Projectile.velocity.Y) * 0.6f) + 0.5f) * Projectile.direction;

            //TODO: at the apex of its jump make it do a little sparkle and make it play a little twinkle sound
            //TODO: after bounce, anubis moves towards the apex before hammer gets there and slashes it, pushing it downwards 
            //TODO: first bounce does some screnshake, dust upwards from tiles, and telegraphs a few points for projs to come out of
            //TODO: second bounce breaks the hammer, more screenshake and dust, projs come out of telegraph spots
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (BounceCount == 0)
            {
                Projectile.position.Y -= 5;
                Projectile.velocity = Vector2.Zero;
                Projectile.velocity.Y -= 13;
                BounceCount++;
                return false;
            }
            else
                return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 2);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, -Projectile.direction, 1, frame, lightColor, true);
            return false;
        }
    }
}

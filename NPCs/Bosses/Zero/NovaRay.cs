using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Globals;

namespace AAModClassic.NPCs.Bosses.Zero
{
    public class NovaRay : ModProjectile
    {
        private const float MoveDistance = 70f;
        
        public float Distance;


        public NPC shooter;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nova Ray");
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = false;
            Projectile.friendly = false;
            Projectile.hostile = true;
        }
        // The AI of the projectile
        public bool runOnce=true;
        public override void AI()
        {
            float rOffset = 0;
            shooter = Main.npc[(int)Projectile.ai[0]];
            if (!shooter.active || shooter.life <=0)
            {
                Projectile.Kill();
            }

            #region Set projectile position


            Vector2 diff = new Vector2((float)Math.Cos(shooter.rotation + rOffset) * 14f, (float)Math.Sin(shooter.rotation + rOffset) * 14f);
            diff.Normalize();
            Projectile.velocity = diff;
            Projectile.netUpdate = true;

            Projectile.position = new Vector2(shooter.Center.X, shooter.Center.Y) + Projectile.velocity * MoveDistance;
            Projectile.timeLeft = 2;
            int dir = Projectile.direction;
            #endregion
            
            Vector2 start = new Vector2(shooter.Center.X, shooter.Center.Y);
            Vector2 unit = Projectile.velocity;
            unit *= -1;
            for (Distance = MoveDistance; Distance <= 2200f; Distance += 5f)
            {
                start = new Vector2(shooter.Center.X, shooter.Center.Y) + Projectile.velocity * Distance;
                if (!Collision.CanHit(new Vector2(shooter.Center.X, shooter.Center.Y), 1, 1, start, 1, 1))
                {
                    Distance -= 5f;
                    break;
                }
            }



            //Add lights
            DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - MoveDistance), 26,
                DelegateMethods.CastLight);

        }
        public int colorCounter;
        public override bool PreDraw(ref Color lightColor)
        {
                DrawLaser(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, new Vector2(shooter.Center.X, shooter.Center.Y),
                    Projectile.velocity, 10, -1.57f, 1f, (int)MoveDistance);
            
            return false;
        }

        // The core function of drawing a laser
        public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, int transDist = 50)
        {
            float r = unit.ToRotation() + rotation;

            #region Draw laser body
            for (float i = transDist; i <= Distance; i += step)
            {
                Color c = AAColor.Oblivion;
                Vector2 origin = start + i * unit;
                spriteBatch.Draw(texture, origin - Main.screenPosition,
                    new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : c, r,
                    new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
            }
            #endregion

            #region Draw laser tail
            spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
                new Rectangle(0, 0, 28, 26), AAColor.ZeroShield, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
            #endregion

            #region Draw laser head
            spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
                new Rectangle(0, 52, 28, 26), AAColor.ZeroShield, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
            #endregion
        }

        // Change the way of collision check of the projectile
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 unit = Projectile.velocity;
            float point = 0f;
            // Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
            // It will look for collisions on the given line using AABB
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), new Vector2(shooter.Center.X, shooter.Center.Y),
                new Vector2(shooter.Center.X, shooter.Center.Y) + unit * Distance, 22, ref point);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 5;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }

}

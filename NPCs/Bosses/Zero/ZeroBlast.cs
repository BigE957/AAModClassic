using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System;


namespace AAMod.NPCs.Bosses.Zero
{
    public class ZeroBlast : ModProjectile
    {

        private const int chargeTime = 200;
        private const float muzzleDist = 1f;
        private const int hitboxSize = 4;
        private const int hitboxHalfSize = hitboxSize / 2;

        private Vector2 staPos; //visual laser start position
        private Vector2 endPos; //visual laser end position


        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Ray");
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.timeLeft = 360;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 3; //THE SECRET TO GOING VERY FAST (updates per frame)

            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            //if (Main.netMode != 1) BaseUtility.Chat("time: "+ projectile.timeLeft + "ai[0]: \n" + projectile.ai[0] + " | ai[1]: \n" + projectile.ai[1]);
            if (Projectile.ai[0] < chargeTime) //spindown
            {
                AICharge();
            }
            else
            {
                AILaunch();
            }
            Projectile.ai[0]++;
        }

        private void AICharge()
        {
            Projectile.position -= Projectile.velocity; //stay still (but keep velo in case)
            if (Projectile.ai[0] == 0) //initial firing
            {
                //move forwards
                Projectile.position += Projectile.velocity * muzzleDist;
                Projectile.ai[1] = 0.1f;

                //set rotation direction scale
                if (Projectile.velocity.X > 0) Projectile.spriteDirection = 1;
                else Projectile.spriteDirection = -1;
                Projectile.scale = 1.5f;

                //get end point of laser
                staPos = Projectile.position + Projectile.velocity * muzzleDist;
                endPos = Projectile.position;
                for (int i = 0; i < Projectile.timeLeft; i++) //roughly 1024 ft.
                {
                    //custom collision to match laser size, once per frame
                    Vector2 halfVelo = Projectile.velocity * 0.5f;
                    Vector2 alteredVelo = Collision.TileCollision(new Vector2(endPos.X - hitboxHalfSize + Projectile.width / 2, endPos.Y - hitboxHalfSize + Projectile.height / 2), halfVelo, hitboxSize, hitboxSize, true, true);
                    if (halfVelo != alteredVelo)
                    {
                        endPos += alteredVelo;
                        break;
                    }
                    alteredVelo = Collision.TileCollision(new Vector2(endPos.X - hitboxHalfSize + Projectile.width / 2, endPos.Y - hitboxHalfSize + Projectile.height / 2) + halfVelo, halfVelo, hitboxSize, hitboxSize, true, true);
                    if (halfVelo != alteredVelo)
                    {
                        endPos += halfVelo + alteredVelo;
                        break;
                    }
                    endPos += Projectile.velocity;
                }

                //emit dust
                for (int i = 0; i < 5; i++)
                {
                    int d1 = Dust.NewDust(Projectile.position + new Vector2(Projectile.width / 2, Projectile.height / 2) + Projectile.velocity * muzzleDist, 1, 1, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 100, Color.White, 1.5f);
                    Main.dust[d1].noGravity = true;
                    Main.dust[d1].velocity *= 0.4f;
                    d1 = Dust.NewDust(endPos + new Vector2(Projectile.width / 2, Projectile.height / 2), 0, 0, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 100, Color.White, 1f);
                    Main.dust[d1].noGravity = true;
                    Main.dust[d1].velocity *= 0.4f;
                }
            }

            Projectile.scale -= 0.007f; //shrink
            Projectile.ai[1] *= 1.004f; //grow laser
            Projectile.rotation += 0.05f * Projectile.spriteDirection * (Projectile.ai[0] * 0.2f); //spin faster and faster
        }
        private void AILaunch()
        {
            //don't update actually, causes weird desync issues clientside...
            //if (Main.myPlayer == projectile.owner) projectile.netUpdate = true;

            if (Projectile.ai[0] == chargeTime)
            {
                //move backwards
                Projectile.position -= Projectile.velocity * muzzleDist;

                //set laser size etc...
                Projectile.ai[1] = 1.7f;
                Projectile.scale = 1.2f;

                SoundEngine.PlaySound(SoundID.Item33.WithVolumeScale(0.4f).WithPitchOffset(0.2f), Projectile.position);
            }
            //damage fall off once per 2 frames
            if (Projectile.damage > 1 && Projectile.timeLeft % 2 == 0) Projectile.damage -= 1;

            //point in direction and shrink over time
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            Projectile.scale *= 0.992f;
            if (Projectile.ai[1] > 0.12f) Projectile.ai[1] *= 0.98f;

            int d1 = Dust.NewDust(Projectile.position + new Vector2(Projectile.width / 2f, Projectile.height / 2f), 1, 1, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 100, Color.White, 1f);
            Main.dust[d1].noGravity = true;
            Main.dust[d1].velocity *= 0.5f;

            //custom collision to match laser size, once per frame
            Vector2 halfVelo = Projectile.velocity * 0.5f;
            Vector2 alteredVelo = Collision.TileCollision(new Vector2(Projectile.position.X - hitboxHalfSize + Projectile.width / 2, Projectile.position.Y - hitboxHalfSize + Projectile.height / 2), halfVelo, hitboxSize, hitboxSize, true, true);
            if (halfVelo != alteredVelo)
            {
                Projectile.Kill();
            }
            alteredVelo = Collision.TileCollision(new Vector2(Projectile.position.X - hitboxHalfSize + Projectile.width / 2, Projectile.position.Y - hitboxHalfSize + Projectile.height / 2) + halfVelo, halfVelo, hitboxSize, hitboxSize, true, true);
            if (halfVelo != alteredVelo)
            {
                Projectile.Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int j = 0; j < 40; j++)
            {
                int d2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust>(), j / 90f * -Projectile.velocity.X, j / 90f * -Projectile.velocity.Y, 100, Color.White, 1.2f);
                Main.dust[d2].noGravity = true;
                Main.dust[d2].velocity *= 0.6f;
            }
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 centre = new Vector2(texture.Width / 2f, texture.Height / 2f);

            DrawLaser(spriteBatch, centre);

            spriteBatch.Draw(texture,
                Projectile.position - Main.screenPosition + centre,
                new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)),
                AAColor.ZeroShield,
                Projectile.rotation,
                centre,
                Projectile.scale,
                SpriteEffects.None,
                0
            );
            return false;
        }

        private void DrawLaser(SpriteBatch spritebatch, Vector2 centre)
        {
            Vector2 projectileCentre = Projectile.position + new Vector2(Projectile.width / 2, Projectile.height / 2);
            Vector2 start, end;
            if (Projectile.ai[0] > 0)
            {
                if (Projectile.ai[0] < chargeTime) //charge
                {
                    start = projectileCentre;
                    end = endPos + centre;
                }
                else                               //fire!
                {
                    start = staPos + centre;
                    end = projectileCentre;
                }

                Utils.DrawLaser(
                    spritebatch,
                    Mod.GetTexture("NPCs/Bosses/Zero/ZeroBlast_Beam"),
                    start - Main.screenPosition,
                    end - Main.screenPosition,
                    new Vector2(Projectile.ai[1]),
                    new Utils.LaserLineFraming(ZeroLaser)); //uses delegate (see method below)
            }
            
            
        }
        //define which frames are used in each stage (0 = start, 1 = mid, 2 = end
        private void ZeroLaser(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
        {
            color = AAColor.ZeroShield;
			if (stage == 0)
			{
				distCovered = 33f;
				frame = new Rectangle(0, 0, 16, 16);
				origin = frame.Size() / 2f;
				return;
			}
			if (stage == 1)
			{
				frame = new Rectangle(0, 16, 16, 16);
				distCovered = frame.Height;
				origin = new Vector2(frame.Width / 2, 0f);
				return;
			}
			if (stage == 2)
			{
				distCovered = 22f;
				frame = new Rectangle(0, 24, 16, 16);
				origin = new Vector2(frame.Width / 2, 1f);
				return;
			}
			distCovered = Projectile.velocity.Length() * 90;
			frame = Rectangle.Empty;
			origin = Vector2.Zero;
			color = Color.Transparent;
        }
    }
}

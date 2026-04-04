using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
    public class RealityAnchor_Proj : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reality Anchor");
		}
        public override void SetDefaults()
        {

            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
        }

        public int BoomTimer = 0;
        public bool Boom = true;

        public override void AI()
        {
            if (Projectile.velocity.Y > 0)
            {
                BoomTimer++;
            }
            else
            {
                BoomTimer = 0;
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1] += 1f;

                if (Projectile.ai[1] >= 10f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.5f;
                    if (Projectile.velocity.Y < 0f)
                    {

                        Projectile.velocity.Y = Projectile.velocity.Y + 0.35f;
                    }
                    Projectile.velocity.X = Projectile.velocity.X * 0.95f;
                    if (Projectile.velocity.Y > 16f)
                    {
                        Projectile.velocity.Y = 16f;
                    }
                    if (Vector2.Distance(Projectile.Center, Main.player[Projectile.owner].Center) > 800f)
                    {
                        Projectile.ai[0] = 1f;
                    }
                }
                else if (Projectile.ai[1] >= 30f)
                {
                    Projectile.ai[0] = 1f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
            }
            else
            {
                Projectile.tileCollide = false;
                float num41 = 16f;
                float num42 = 4f;
                Vector2 vector2 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                float num43 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector2.X;
                float num44 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector2.Y;
                float num45 = (float)Math.Sqrt((double)(num43 * num43 + num44 * num44));
                if (num45 > 3000f)
                {
                    Projectile.Kill();
                }
                num45 = num41 / num45;
                num43 *= num45;
                num44 *= num45;
                Vector2 vector3 = new Vector2(num43, num44) - Projectile.velocity;
                if (vector3 != Vector2.Zero)
                {
                    Vector2 value = vector3;
                    value.Normalize();
                    Projectile.velocity += value * Math.Min(num42, vector3.Length());
                }
                else
                {
                    if (Projectile.velocity.X < num43)
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num42;
                        if (Projectile.velocity.X < 0f && num43 > 0f)
                        {
                            Projectile.velocity.X = Projectile.velocity.X + num42;
                        }
                    }
                    else if (Projectile.velocity.X > num43)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num42;
                        if (Projectile.velocity.X > 0f && num43 < 0f)
                        {
                            Projectile.velocity.X = Projectile.velocity.X - num42;
                        }
                    }
                    if (Projectile.velocity.Y < num44)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num42;
                        if (Projectile.velocity.Y < 0f && num44 > 0f)
                        {
                            Projectile.velocity.Y = Projectile.velocity.Y + num42;
                        }
                    }
                    else if (Projectile.velocity.Y > num44)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - num42;
                        if (Projectile.velocity.Y > 0f && num44 < 0f)
                        {
                            Projectile.velocity.Y = Projectile.velocity.Y - num42;
                        }
                    }
                }
                if (Main.myPlayer == Projectile.owner)
                {
                    Rectangle rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                    Rectangle value2 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
                    if (rectangle.Intersects(value2))
                    {
                        Projectile.Kill();
                    }
                }
            }
            if (Projectile.ai[0] == 0f)
            {
                Vector2 velocity = Projectile.velocity;
                velocity.Normalize();
                Projectile.rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 1.57f;
                return;
            }
            Vector2 vector4 = Projectile.Center - Main.player[Projectile.owner].Center;
            vector4.Normalize();
            Projectile.rotation = (float)Math.Atan2(vector4.Y, vector4.X) + 1.57f;
            return;
        }


        public override void OnHitNPC (NPC target, NPC.HitInfo hit, int damageDone)
		{
            //target.AddBuff(BuffID.Daybreak, 600);
        }
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 30;
            height = 30;
            return true;
        }
		
		public override bool OnTileCollide (Vector2 oldVelocity)
		{
            if (BoomTimer > 180 && Boom)
            {
                Boom = false;

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y + 30, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<RealityBurstHuge>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Projectile.ai[0] = 1f;
                return false;
            }
            if (BoomTimer > 120 && Boom)
            {
                Boom = false;

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y + 20, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<RealityBurstLarge>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Projectile.ai[0] = 1f;
                return false;
            }
            if (BoomTimer > 60 && Boom)
            {
                Boom = false;

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y + 10, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<RealityBurstMed>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Projectile.ai[0] = 1f;
                return false;
            }
            else
            {
                Boom = false;

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y + 5, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<RealityBurstSmall>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Projectile.ai[0] = 1f;
                return false;
            }
        }
		
 
        // chain voodoo
        public override bool PreDraw(ref Color lightColor)
        {
			
            Texture2D texture = Mod.GetTexture("AAModClassic/_Unreleased/Projectiles/SoC/Anchor_Chain");
 
            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector24 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                flag = false;
            while (flag)
            {
                if (vector24.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector21 = vector24;
                    vector21.Normalize();
                    position += vector21 * num1;
                    vector24 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}
using AAModClassic._Content.Inferno.Projectiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons
{
    public class Daycrusher_Holdout : FlailHoldout
    {
        public override string ChainTexturePath => Texture + "_Chain";

        public override float DrawRotationOffset => MathHelper.PiOver2;

        public override float LaunchSpeed => 24;

        public override int LaunchTimeLimit => 18;

        public override float RetractAcceleration => base.RetractAcceleration;

        public override float MaxRetractSpeed => base.MaxRetractSpeed;

        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Daycrusher");
            base.SetStaticDefaults();
		}
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 30;
            base.SetDefaults();
        }
		
		public override void AI()
		{
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position = Projectile.position;
                dust1 = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0, 0, 0, default, 1f)];
                dust2 = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0, 0, 0, default, 1f)];
                dust1.noGravity = true;
                dust2.noGravity = true;
            }

            base.AI();

            /*
            if (Projectile.timeLeft == 120)
            {
                Projectile.ai[0] = 1f;
            }

            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
                return;
            }

            Main.player[Projectile.owner].itemAnimation = 5;
            Main.player[Projectile.owner].itemTime = 5;

            if (Projectile.alpha == 0)
            {
                if (Projectile.position.X + Projectile.width / 2 > Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2)
                {
                    Main.player[Projectile.owner].ChangeDir(1);
                }
                else
                {
                    Main.player[Projectile.owner].ChangeDir(-1);
                }
            }
            Vector2 vector14 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            float num166 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector14.X;
            float num167 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector14.Y;
            float num168 = (float)Math.Sqrt(num166 * num166 + num167 * num167);
            if (Projectile.ai[0] == 0f)
            {
                if (num168 > 700f)
                {
                    Projectile.ai[0] = 1f;
                }
                else if (num168 > 500f)
                {
                    Projectile.ai[0] = 1f;
                }
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] > 5f)
                {
                    Projectile.alpha = 0;
                }
                if (Projectile.ai[1] > 8f)
                {
                    Projectile.ai[1] = 8f;
                }
                if (Projectile.ai[1] >= 10f)
                {
                    Projectile.ai[1] = 15f;
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.3f;
                }
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = -1;
                }
                else
                {
                    Projectile.spriteDirection = 1;
                }
            }
            else if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
                Projectile.rotation = (float)Math.Atan2(num167, num166) - 1.57f;
                float num169 = 30f;

                if (num168 < 50f)
                {
                    Projectile.Kill();
                }
                num168 = num169 / num168;
                num166 *= num168;
                num167 *= num168;
                Projectile.velocity.X = num166;
                Projectile.velocity.Y = num167;
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = 1;
                }
                else
                {
                    Projectile.spriteDirection = -1;
                }

            }
            //Spew eyes
            if ((int)Projectile.ai[1] % 8 == 0 && Projectile.owner == Main.myPlayer && Main.rand.NextBool(50)) //higher # means later on in the attack
            {
                Vector2 vector54 = Main.player[Projectile.owner].Center - Projectile.Center;
                Vector2 vector55 = vector54 * -1f;
                vector55.Normalize();
                vector55 *= Main.rand.Next(45, 65) * 0.1f;
                vector55 = vector55.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866);
                //Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector55.X, vector55.Y, mod.ProjectileType("EyeProjectile2"), projectile.damage, projectile.knockBack, projectile.owner, -10f);
            }
            */
        }
		
		public override void OnHitNPC (NPC target, NPC.HitInfo hit, int damageDone)
		{
            target.AddBuff(BuffID.Daybreak, 600);
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			int p = Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<AkumaExplosionBlue>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
			Main.projectile[p].DamageType = DamageClass.Melee;
			Main.projectile[p].friendly = true;
			Main.projectile[p].hostile = false;
			Main.projectile[p].usesLocalNPCImmunity = true;
			Main.projectile[p].localNPCHitCooldown = 4;
        }
		
        // chain voodoo
        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
			/*
            Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Chain").Value;
 
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
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
            */
        }
    }
}